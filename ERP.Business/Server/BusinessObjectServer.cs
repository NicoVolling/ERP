using ERP.BaseLib.Objects;
using ERP.BaseLib.Serialization;
using ERP.Business.Objects;
using ERP.Commands.Base;
using ERP.Exceptions.ErpExceptions;
using ERP.IO.FileSystem;

namespace ERP.Business.Server
{
    /// <summary>
    /// A version of <see cref="CommandCollection"/> wich is more specified for storing data.
    /// </summary>
    /// <typeparam name="T_BusinessObject">Type of BusinessObject</typeparam>
    public abstract class BusinessObjectServer<T_BusinessObject> : CommandCollection, IFileSaver
        where T_BusinessObject : BusinessObject, new()
    {
        private bool DataChanged = false;

        /// <summary>
        /// Constructor
        /// </summary>
        public BusinessObjectServer()
        {
            Filename = $"{Namespace}.{this.GetType().Name}";
        }

        public bool DataLoaded { get; private set; } = false;

        public string Filename { get; }

        /// <summary>
        /// Contains all objects that will be saved.
        /// </summary>
        protected List<T_BusinessObject> ObjectList { get; private set; } = new List<T_BusinessObject>();

        bool IFileSaver.CanLoad()
        {
            return !DataLoaded;
        }

        bool IFileSaver.CanSave()
        {
            return DataLoaded && DataChanged;
        }

        /// <summary>
        /// Changes the Object.
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public Result<T_BusinessObject> Change(Guid SECURITY_CODE, T_BusinessObject Data)
        {
            if (!ServerSide) { return GetClientResult(SECURITY_CODE, Data).ToGenericResult<T_BusinessObject>(); }
            T_BusinessObject res = OnChange(SECURITY_CODE, Data);
            DataChanged = true;
            return new(res);
        }

        /// <summary>
        /// Creates the Object.
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public Result<T_BusinessObject> Create(Guid SECURITY_CODE, T_BusinessObject Data)
        {
            if (!ServerSide) { return GetClientResult(SECURITY_CODE, Data).ToGenericResult<T_BusinessObject>(); }
            T_BusinessObject res = OnCreate(SECURITY_CODE, Data);
            DataChanged = true;
            return new(res);
        }

        /// <summary>
        /// Delete Object.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Result<bool> Delete(Guid SECURITY_CODE, Guid ID)
        {
            if (!ServerSide) { return GetClientResult(SECURITY_CODE, ID).ToGenericResult<bool>(); }
            bool res = OnDelete(SECURITY_CODE, ID);
            DataChanged = true;
            return new(res);
        }

        void IFileSaver.DeserializeData(string Raw)
        {
            List<T_BusinessObject> ObjectList = Json.Deserialize<List<T_BusinessObject>>(Raw);
            if (ObjectList != null)
            {
                if (ObjectList.Where(o => o.ID == Guid.Empty) is IEnumerable<T_BusinessObject> EmptyList && EmptyList.Any())
                {
                    ObjectList = ObjectList.Select(o => { if (o.ID == Guid.Empty) { o.ID = Guid.NewGuid(); } return o; }).ToList();
                    DataChanged = true;
                }
                this.ObjectList = ObjectList;
            }
            else
            {
                this.ObjectList = new List<T_BusinessObject>();
            }
        }

        /// <summary>
        /// Gets Object by ID.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Result<T_BusinessObject> GetData(Guid SECURITY_CODE, Guid ID)
        {
            if (!ServerSide) { return GetClientResult(SECURITY_CODE, ID).ToGenericResult<T_BusinessObject>(); }
            return new(OnGetData(SECURITY_CODE, ID));
        }

        /// <summary>
        /// Returns true if an object with the given ID exists.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Result<bool> GetExistence(Guid SECURITY_CODE, Guid ID)
        {
            if (!ServerSide) { return GetClientResult(SECURITY_CODE, ID).ToGenericResult<bool>(); }
            return new(OnGetExistence(SECURITY_CODE, ID));
        }

        /// <summary>
        /// Gets a list of all Objects.
        /// </summary>
        /// <returns></returns>
        public Result<IEnumerable<BusinessObjectIdentifier>> GetList(Guid SECURITY_CODE)
        {
            if (!ServerSide) { return GetClientResult(SECURITY_CODE).ToGenericResult<IEnumerable<BusinessObjectIdentifier>>(); }
            return new(OnGetList(SECURITY_CODE).Select(o => new BusinessObjectIdentifier(o.ID, o.ToString())));
        }

        /// <summary>
        /// Gets Data of given Objects.
        /// </summary>
        /// <param name="IDs">IDs</param>
        /// <returns></returns>
        public Result<IEnumerable<T_BusinessObject>> GetObjects(Guid SECURITY_CODE, Guid[] IDs)
        {
            if (!ServerSide) { return GetClientResult(SECURITY_CODE, IDs).ToGenericResult<IEnumerable<T_BusinessObject>>(); }
            return new(OnGetList(SECURITY_CODE).Where(o => IDs.Any(p => p == o.ID)).ToArray());
        }

        public void Load()
        {
            IFileSaver.Load(this);
            DataLoaded = true;
        }

        public void Save()
        {
            IFileSaver.Save(this);
            DataChanged = false;
        }

        string IFileSaver.SerializeData()
        {
            return ObjectList.Serialize();
        }

        /// <summary>
        /// Adds the given object to the objectlist and returns the ID
        /// </summary>
        /// <param name="Object">Will be added to the list</param>
        /// <returns>ID that the object will retrieve</returns>
        protected Guid AddObject(T_BusinessObject Object)
        {
            Object.ID = Guid.NewGuid();
            ObjectList.Add(Object);
            DataChanged = true;
            return Object.ID;
        }

        protected IEnumerable<T_BusinessObject> GenerateSampleData()
        {
            IEnumerable<T_BusinessObject> list = OnGenerateSampleData();
            foreach (T_BusinessObject item in list)
            {
                if (item.ID == Guid.Empty) { item.ID = Guid.NewGuid(); }
            }
            return list;
        }

        /// <summary>
        /// Gets Object by ID or throws Exception if not found.
        /// </summary>
        /// <param name="ObjectList"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected T_BusinessObject GetObjectByID(Guid ID)
        {
            Load();
            if (GetObjectList().FirstOrDefault(o => o.ID == ID) is T_BusinessObject Object) { return Object; }
            throw new MissingObjectErpException(typeof(T_BusinessObject), ID);
        }

        protected List<T_BusinessObject> GetObjectList()
        {
            try
            {
                Load();
                if (!(ObjectList != null && ObjectList.Any()))
                {
                    ObjectList = GenerateSampleData().ToList();
                }
                return ObjectList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Changes the Object.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        protected virtual T_BusinessObject OnChange(Guid SECURITY_CODE, T_BusinessObject Data)
        {
            try
            {
                Load();
                T_BusinessObject Object = GetObjectByID(Data.ID);
                Object.ApplyChanges(Data);
                return Object;
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
        protected virtual T_BusinessObject OnCreate(Guid SECURITY_CODE, T_BusinessObject Data)
        {
            try
            {
                Load();
                AddObject(Data);
                return Data;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete Object.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        protected virtual bool OnDelete(Guid SECURITY_CODE, Guid ID)
        {
            try
            {
                Load();
                T_BusinessObject Object = GetObjectByID(ID);
                RemoveObject(ID);
                return true;
            }
            catch
            {
                throw;
            }
        }

        protected virtual IEnumerable<T_BusinessObject> OnGenerateSampleData()
        {
            return new List<T_BusinessObject>();
        }

        /// <summary>
        /// Gets Object by ID.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        protected virtual T_BusinessObject OnGetData(Guid SECURITY_CODE, Guid ID)
        {
            try
            {
                Load();
                return GetObjectByID(ID);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Returns true if an object with the given ID exists.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        protected virtual bool OnGetExistence(Guid SECURITY_CODE, Guid ID)
        {
            return GetObjectList().Any(o => o.ID == ID);
        }

        /// <summary>
        /// Gets a list of all Objects.
        /// </summary>
        /// <returns></returns>
        protected virtual List<T_BusinessObject> OnGetList(Guid SECURITY_CODE)
        {
            return GetObjectList();
        }

        protected void RemoveObject(Guid ID)
        {
            ObjectList.RemoveAll(o => o.ID == ID);
        }
    }
}