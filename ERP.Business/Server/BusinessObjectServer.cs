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
    public abstract class BusinessObjectServer<T_BusinessObject> : CommandCollection
        where T_BusinessObject : BusinessObject, new()
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BusinessObjectServer()
        {
            FileSaver = new(Namespace, this.GetType().Name);
        }

        protected FileSaver FileSaver { get; }

        /// <summary>
        /// Changes the Object.
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public Result<T_BusinessObject> Change(Guid SECURITY_CODE, T_BusinessObject Data)
        {
            if (!ServerSide) { return GetClientResult(SECURITY_CODE, Data).ToGenericResult<T_BusinessObject>(); }
            T_BusinessObject res = OnChange(SECURITY_CODE, Data);
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
            return new(res);
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
        public Result<IEnumerable<Guid>> GetList(Guid SECURITY_CODE)
        {
            if (!ServerSide) { return GetClientResult(SECURITY_CODE).ToGenericResult<IEnumerable<Guid>>(); }
            return new(FileSaver.GetList());
        }

        /// <summary>
        /// Gets a list of all Objects Metas.
        /// </summary>
        /// <returns></returns>
        public Result<IEnumerable<BusinessObjectMeta>> GetMetaList(Guid SECURITY_CODE)
        {
            if (!ServerSide) { return GetClientResult(SECURITY_CODE).ToGenericResult<IEnumerable<BusinessObjectMeta>>(); }
            return new(FileSaver.GetList().Select(o => Json.Deserialize<BusinessObjectMeta>(FileSaver.GetMeta(o))));
        }

        /// <summary>
        /// Gets Data of given Objects.
        /// </summary>
        /// <param name="IDs">IDs</param>
        /// <returns></returns>
        public Result<IEnumerable<T_BusinessObject>> GetObjects(Guid SECURITY_CODE, Guid[] IDs)
        {
            if (!ServerSide) { return GetClientResult(SECURITY_CODE, IDs).ToGenericResult<IEnumerable<T_BusinessObject>>(); }
            return new(FileSaver.GetList().Where(o => IDs.Contains(o)).Select(o => Json.Deserialize<T_BusinessObject>(FileSaver.GetObject(o))));
        }

        /// <summary>
        /// Adds the given object to the objectlist and returns the ID
        /// </summary>
        /// <param name="Object">Will be added to the list</param>
        /// <returns>ID that the object will retrieve</returns>
        protected Guid AddObject(T_BusinessObject Object)
        {
            Object.ID = Guid.NewGuid();
            FileSaver.Save(Object.ID, Object.Serialize(), new BusinessObjectMeta() { ID = Object.ID, Name = Object.ToStringValue, LastChanged = DateTime.Now }.Serialize());
            return Object.ID;
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
            return Json.Deserialize<T_BusinessObject>(FileSaver.GetObject(ID));
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
                T_BusinessObject Object = GetObjectByID(Data.ID);
                FileSaver.Save(Data.ID, Data.Serialize(), new BusinessObjectMeta() { ID = Data.ID, Name = Data.ToStringValue, LastChanged = DateTime.Now }.Serialize());
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
        /// Gets Object by ID.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        protected virtual T_BusinessObject OnGetData(Guid SECURITY_CODE, Guid ID)
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
        /// Returns true if an object with the given ID exists.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        protected virtual bool OnGetExistence(Guid SECURITY_CODE, Guid ID)
        {
            return FileSaver.ObjectExists(ID);
        }

        protected void RemoveObject(Guid ID)
        {
            FileSaver.Delete(ID);
        }
    }
}