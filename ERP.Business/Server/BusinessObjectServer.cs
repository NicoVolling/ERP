using ERP.BaseLib.Objects;
using ERP.BaseLib.Serialization;
using ERP.Business.Objects;
using ERP.Commands.Base;
using ERP.IO.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Business.Server
{
    /// <summary>
    /// A version of <see cref="CommandCollection"/> wich is more specified for storing data.
    /// </summary>
    /// <typeparam name="T_BusinessObject">Type of BusinessObject</typeparam>
    public abstract class BusinessObjectServer<T_BusinessObject> : CommandCollection, IFileSaver 
        where T_BusinessObject : BusinessObject, new()
    {

        /// <summary>
        /// Contains all objects that will be saved.
        /// </summary>
        protected List<T_BusinessObject> ObjectList { get; private set; } = new List<T_BusinessObject>();

        public string Filename { get; }

        /// <summary>
        /// Creates the Object.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        protected virtual T_BusinessObject OnCreate(User User, T_BusinessObject Data) 
        {
            try 
            { 
                AddObject(Data);
                return Data;
            }
            catch 
            { 
                throw; 
            }
        }

        /// <summary>
        /// Creates the Object.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public Result Create(User User, T_BusinessObject Data) 
        {
            if(!ServerSide) { return GetClientResult(User, Data); }
            return new Result(OnCreate(User, Data));
        }

        /// <summary>
        /// Changes the Object.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ID"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        protected virtual T_BusinessObject OnChange(User User, T_BusinessObject Data) 
        {
            try 
            { 
                T_BusinessObject Object = GetObjectByID(Data.ID);
                Object.Deserialize(Data);
                return Object;
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Changes the Object.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ID"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public Result Change(User User, T_BusinessObject Data) 
        {
            if(!ServerSide) { return GetClientResult(User, Data); }
            return new Result(OnChange(User, Data));
        }

        /// <summary>
        /// Delete Object.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public virtual bool OnDelete(User User, int ID) 
        {
            try
            {
                T_BusinessObject Object = GetObjectByID(ID);
                RemoveObject(ID);
                return true;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete Object.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Result Delete(User User, int ID)
        {
            if (!ServerSide) { return GetClientResult(User, ID); }
            return new Result(OnDelete(User, ID));
        }

        /// <summary>
        /// Gets Object by ID.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public virtual T_BusinessObject OnGetData(User User, int ID) 
        {
            try
            {
                return GetObjectByID(ID);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Gets Object by ID.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Result GetData(User User, int ID)
        {
            if (!ServerSide) { return GetClientResult(User, ID); }
            return new Result(OnGetData(User, ID));
        }

        /// <summary>
        /// Gets a list of all Objects.
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public virtual List<T_BusinessObject> OnGetList(User User) 
        {
            try
            {
                Load();
                return ObjectList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Gets a list of all Objects.
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public Result GetList(User User)
        {
            if (!ServerSide) { return GetClientResult(User); }
            return new Result(OnGetList(User));
        }

        public bool DataLoaded { get; private set; } = false;

        /// <summary>
        /// Gets Object by ID or throws Exception if not found.
        /// </summary>
        /// <param name="ObjectList"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected T_BusinessObject GetObjectByID(int ID) 
        {
            Load();
            if(ObjectList.FirstOrDefault(o => o.ID == ID) is T_BusinessObject Object) { return Object; }
            throw new Exception($"Cound not find ID:{ID} of object {typeof(T_BusinessObject).Name}");
        }

        string IFileSaver.Serialize()
        {
            return Json.Serialize(ObjectList);
        }

        void IFileSaver.Deserialize(string Raw)
        {
            List<T_BusinessObject>? ObjectList = Json.Deserialize<List<T_BusinessObject>>(Raw);
            if (ObjectList != null)
            {
                this.ObjectList = ObjectList;
            }
            else 
            {
                this.ObjectList = new List<T_BusinessObject>();
            }
        }

        public void Save()
        {
            IFileSaver.Save(this);
        }

        public void Load()
        {
            IFileSaver.Load(this);
            DataLoaded = true;
        }

        /// <summary>
        /// Adds the given object to the objectlist and returns the ID
        /// </summary>
        /// <param name="Object">Will be added to the list</param>
        /// <returns>ID that the object will retrieve</returns>
        protected int AddObject(T_BusinessObject Object) 
        {
            int ID = -1;
            if (ObjectList.Any())
            {
                int previous = -1;
                foreach(T_BusinessObject obj in ObjectList.OrderBy(o => o.ID))
                {
                    if (previous != -1)
                    {
                        if (obj.ID - previous > 1)
                        {
                            ID = previous + 1;
                            break;
                        }
                    }
                    previous = obj.ID;
                }
                if(ID < 0) 
                {
                    throw new Exception("ID-Generation failed.");
                }
            }
            else 
            {
                ID = 0;
            }

            ObjectList.Add(Object);

            return ID;
        }

        public void RemoveObject(int ID) 
        {
            ObjectList.RemoveAll(o => o.ID == ID);
        }

        bool IFileSaver.CanSave()
        {
            return DataLoaded;
        }

        bool IFileSaver.CanLoad()
        {
            return !DataLoaded;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public BusinessObjectServer() 
        {
            Filename = System.IO.Path.Combine(Namespace.Replace(".", System.IO.Path.DirectorySeparatorChar.ToString()), this.GetType().Name);
        }
    }
}
