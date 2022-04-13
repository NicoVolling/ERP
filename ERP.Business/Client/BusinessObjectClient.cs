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
    public abstract class BusinessObjectClient<T_CommandCollection, T_BusinessObject>
        where T_CommandCollection : BusinessObjectServer<T_BusinessObject>, new()
        where T_BusinessObject : BusinessObject, new()
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BusinessObjectClient()
        {
        }

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
        public void Change(Guid ID, T_BusinessObject Data)
        {
            if (BeforeChange())
            {
                Result Result = CommandCollection.GetInstance<T_CommandCollection>().Change(Data);
                if (!Result.Error)
                {
                    Data.Deserialize(Result.ReturnValue);
                    this.Data = Data;
                }
                else
                {
                    throw new ErpException($"{Result.ErrorType}:{Result.ErrorMessage}");
                }
            }
        }

        /// <summary>
        /// Creates a new object on the server.
        /// </summary>
        /// <param name="Data"></param>
        /// <exception cref="Exception"></exception>
        public void Create(T_BusinessObject Data)
        {
            if (BeforeCreate())
            {
                Result Result = CommandCollection.GetInstance<T_CommandCollection>().Create(Data);
                if (!Result.Error)
                {
                    Data.Deserialize(Result.ReturnValue);
                    this.Data = Data;
                }
                else
                {
                    throw new ErpException($"{Result.ErrorType}:{Result.ErrorMessage}");
                }
            }
        }

        /// <summary>
        /// Deletes the object on server.
        /// </summary>
        /// <param name="ID"></param>
        /// <exception cref="Exception"></exception>
        public void Delete(Guid ID)
        {
            if (BeforeDelete())
            {
                Result Result = CommandCollection.GetInstance<T_CommandCollection>().Delete(ID);
                if (!Result.Error)
                {
                    Data = new T_BusinessObject();
                }
                else
                {
                    throw new ErpException($"{Result.ErrorType}:{Result.ErrorMessage}");
                }
            }
        }

        /// <summary>
        /// Gets the data from server.
        /// </summary>
        /// <param name="ID"></param>
        /// <exception cref="Exception"></exception>
        public void GetData(Guid ID)
        {
            if (BeforeGetData())
            {
                Result Result = CommandCollection.GetInstance<T_CommandCollection>().GetData(ID);
                if (!Result.Error)
                {
                    Data.Deserialize(Result.ReturnValue);
                }
                else
                {
                    throw new ErpException($"{Result.ErrorType}:{Result.ErrorMessage}");
                }
            }
        }

        /// <summary>
        /// Gets a list of all objects from the server.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<BusinessObjectIdentifier> GetList()
        {
            Result Result = CommandCollection.GetInstance<T_CommandCollection>().GetList();
            if (!Result.Error)
            {
                return Json.Deserialize<List<BusinessObjectIdentifier>>(Result.ReturnValue);
            }
            else
            {
                throw new ErpException($"{Result.ErrorType}:{Result.ErrorMessage}");
            }
        }

        protected virtual bool BeforeChange()
        { return true; }

        protected virtual bool BeforeCreate()
        { return true; }

        protected virtual bool BeforeDelete()
        { return true; }

        protected virtual bool BeforeGetData()
        { return true; }
    }
}