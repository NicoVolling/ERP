using ERP.Business.Objects;

namespace ERP.Business.Client
{
    public interface IBusinessObjectClient
    {
        /// <summary>
        /// Current context. This is the BusinessObject containing all the Data.
        /// </summary>
        public BusinessObject BO_Data { get; protected set; }

        /// <summary>
        /// Updates data on the server.
        /// </summary>
        /// <param name="SECURITY_CODE"></param>
        /// <param name="ID"></param>
        /// <param name="Data"></param>
        /// <exception cref="Exception"></exception>
        public void Change(Guid SECURITY_CODE, Guid ID, BusinessObject Data);

        /// <summary>
        /// Creates a new object on the server.
        /// </summary>
        /// <param name="SECURITY_CODE"></param>
        /// <param name="Data"></param>
        /// <exception cref="Exception"></exception>
        public void Create(Guid SECURITY_CODE, BusinessObject Data);

        /// <summary>
        /// Deletes the object on server.
        /// </summary>
        /// <param name="SECURITY_CODE"></param>
        /// <param name="ID"></param>
        /// <exception cref="Exception"></exception>
        public void Delete(Guid SECURITY_CODE, Guid ID);

        /// <summary>
        /// Gets the data from server.
        /// </summary>
        /// <param name="SECURITY_CODE"></param>
        /// <param name="ID"></param>
        /// <exception cref="Exception"></exception>
        public void GetData(Guid SECURITY_CODE, Guid ID);

        /// <summary>
        /// Returns true if an object with the given ID exists.
        /// </summary>
        /// <param name="SECURITY_CODE"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool GetExistence(Guid SECURITY_CODE, Guid ID);

        /// <summary>
        /// Gets a list of all objects from the server.
        /// </summary>
        /// <param name="SECURITY_CODE"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<Guid> GetList(Guid SECURITY_CODE);

        /// <summary>
        /// Gets a list of all objects metas from the server.
        /// </summary>
        /// <param name="SECURITY_CODE"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<BusinessObjectMeta> GetMetaList(Guid SECURITY_CODE);

        /// <summary>
        /// Gets Data of given Objects.
        /// </summary>
        /// <param name="SECURITY_CODE"></param>
        /// <param name="IDs">IDs</param>
        /// <returns></returns>
        public List<BusinessObject> GetObjects(Guid SECURITY_CODE, Guid[] IDs);
    }
}