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
        /// <param name="ID"></param>
        /// <param name="Data"></param>
        /// <exception cref="Exception"></exception>
        public void Change(Guid ID, BusinessObject Data);

        /// <summary>
        /// Creates a new object on the server.
        /// </summary>
        /// <param name="Data"></param>
        /// <exception cref="Exception"></exception>
        public void Create(BusinessObject Data);

        /// <summary>
        /// Deletes the object on server.
        /// </summary>
        /// <param name="ID"></param>
        /// <exception cref="Exception"></exception>
        public void Delete(Guid ID);

        /// <summary>
        /// Gets the data from server.
        /// </summary>
        /// <param name="ID"></param>
        /// <exception cref="Exception"></exception>
        public void GetData(Guid ID);

        /// <summary>
        /// Returns true if an object with the given ID exists.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool GetExistence(Guid ID);

        /// <summary>
        /// Gets a list of all objects from the server.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<BusinessObjectIdentifier> GetList();

        /// <summary>
        /// Gets Data of given Objects.
        /// </summary>
        /// <param name="IDs">IDs</param>
        /// <returns></returns>
        public List<BusinessObject> GetObjects(Guid[] IDs);
    }
}