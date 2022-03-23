using ERP.BaseLib.Objects;
using ERP.Business.Objects;
using ERP.Commands.Base;
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
    public abstract class BusinessObjectServer<T_BusinessObject> : CommandCollection 
        where T_BusinessObject : BusinessObject, new()
    {
        /// <summary>
        /// Creates the Object.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public abstract Result Create(User User, T_BusinessObject Data);

        /// <summary>
        /// Changes the Object.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ID"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public abstract Result Change(User User, int ID, T_BusinessObject Data);

        /// <summary>
        /// Delete Object.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public abstract Result Delete(User User, int ID);

        /// <summary>
        /// Gets Object by ID.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public abstract Result GetData(User User, int ID);

        /// <summary>
        /// Gets a list of all Objects.
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public abstract Result GetList(User User);

        /// <summary>
        /// Gets Object by ID or throws Exception if not found.
        /// </summary>
        /// <param name="ObjectList"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected T_BusinessObject GetObjectByID(IEnumerable<T_BusinessObject> ObjectList, int ID) 
        {
            if(ObjectList.FirstOrDefault(o => o.ID == ID) is T_BusinessObject Object) { return Object; }
            throw new Exception($"Cound not find ID:{ID} of object {typeof(T_BusinessObject).Name}");
        }
    }
}
