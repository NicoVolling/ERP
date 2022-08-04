using ERP.BaseLib.Objects;
using ERP.BaseLib.Serialization;
using ERP.Business.Objects;
using ERP.Business.Server;
using ERP.Commands.Base;
using ERP.Exceptions.ErpExceptions;

namespace ERP.Business.Client
{
    /// <summary>
    /// This class contains all ClientFunctionalities
    /// </summary>
    public abstract class BusinessObjectClient<T_CommandCollection, T_BusinessObject> : IBusinessObjectClient
        where T_CommandCollection : BusinessObjectServer<T_BusinessObject>, new()
        where T_BusinessObject : BusinessObject, new()
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BusinessObjectClient()
        {
        }

        BusinessObject IBusinessObjectClient.BO_Data { get => Data; set => Data = value as T_BusinessObject; }

        /// <summary>
        /// Current context. This is the BusinessObject containing all the Data.
        /// </summary>
        public T_BusinessObject Data { get; private set; } = new T_BusinessObject();

        /// <summary>
        /// Updates data on the server.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Data"></param>
        /// <exception cref="Exception"></exception>
        public void Change(Guid SECURITY_CODE, Guid ID, T_BusinessObject Data)
        {
            Result Result = CommandCollection.GetInstance<T_CommandCollection>().Change(SECURITY_CODE, Data);
            if (!Result.Error)
            {
                Data.DeserializeAndApplyChanges(Result.ReturnValue);
                this.Data = Data;
            }
            else
            {
                throw new ErpException($"{Result.ErrorType}:{Result.ErrorMessage}");
            }
        }

        public void Change(Guid SECURITY_CODE, Guid ID, BusinessObject Data)
        {
            Change(SECURITY_CODE, ID, Data as T_BusinessObject);
        }

        /// <summary>
        /// Creates a new object on the server.
        /// </summary>
        /// <param name="Data"></param>
        /// <exception cref="Exception"></exception>
        public void Create(Guid SECURITY_CODE, T_BusinessObject Data)
        {
            Result Result = CommandCollection.GetInstance<T_CommandCollection>().Create(SECURITY_CODE, Data);
            if (!Result.Error)
            {
                Data.DeserializeAndApplyChanges(Result.ReturnValue);
                this.Data = Data;
            }
            else
            {
                throw new ErpException($"{Result.ErrorType}:{Result.ErrorMessage}");
            }
        }

        public void Create(Guid SECURITY_CODE, BusinessObject Data)
        {
            Create(SECURITY_CODE, Data as T_BusinessObject);
        }

        /// <summary>
        /// Deletes the object on server.
        /// </summary>
        /// <param name="ID"></param>
        /// <exception cref="Exception"></exception>
        public void Delete(Guid SECURITY_CODE, Guid ID)
        {
            Result Result = CommandCollection.GetInstance<T_CommandCollection>().Delete(SECURITY_CODE, ID);
            if (!Result.Error)
            {
                Data = new T_BusinessObject();
            }
            else
            {
                throw new ErpException($"{Result.ErrorType}:{Result.ErrorMessage}");
            }
        }

        /// <summary>
        /// Gets the data from server.
        /// </summary>
        /// <param name="ID"></param>
        /// <exception cref="Exception"></exception>
        public void GetData(Guid SECURITY_CODE, Guid ID)
        {
            Result Result = CommandCollection.GetInstance<T_CommandCollection>().GetData(SECURITY_CODE, ID);
            if (!Result.Error)
            {
                Data.DeserializeAndApplyChanges(Result.ReturnValue);
            }
            else
            {
                throw new ErpException($"{Result.ErrorType}:{Result.ErrorMessage}");
            }
        }

        /// <summary>
        /// Returns true if an object with the given ID exists.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool GetExistence(Guid SECURITY_CODE, Guid ID)
        {
            Result Result = CommandCollection.GetInstance<T_CommandCollection>().GetExistence(SECURITY_CODE, ID);
            if (!Result.Error)
            {
                return Json.Deserialize<bool>(Result.ReturnValue);
            }
            else
            {
                throw new ErpException($"{Result.ErrorType}:{Result.ErrorMessage}");
            }
        }

        /// <summary>
        /// Gets a list of all objects from the server.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<BusinessObjectIdentifier> GetList(Guid SECURITY_CODE)
        {
            Result Result = CommandCollection.GetInstance<T_CommandCollection>().GetList(SECURITY_CODE);
            if (!Result.Error)
            {
                return Json.Deserialize<List<BusinessObjectIdentifier>>(Result.ReturnValue);
            }
            else
            {
                throw new ErpException($"{Result.ErrorType}:{Result.ErrorMessage}");
            }
        }

        /// <summary>
        /// Gets Data of given Objects.
        /// </summary>
        /// <param name="IDs">IDs</param>
        /// <returns></returns>
        public List<T_BusinessObject> GetObjects(Guid SECURITY_CODE, Guid[] IDs)
        {
            Result Result = CommandCollection.GetInstance<T_CommandCollection>().GetObjects(SECURITY_CODE, IDs);
            if (!Result.Error)
            {
                return Json.Deserialize<List<T_BusinessObject>>(Result.ReturnValue);
            }
            else
            {
                throw new ErpException($"{Result.ErrorType}:{Result.ErrorMessage}");
            }
        }

        List<BusinessObject> IBusinessObjectClient.GetObjects(Guid SECURITY_CODE, Guid[] IDs)
        {
            return this.GetObjects(SECURITY_CODE, IDs).Select(o => o as BusinessObject).ToList();
        }
    }
}