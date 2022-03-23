﻿using ERP.BaseLib.Objects;
using ERP.BaseLib.Serialization;
using ERP.Business.Objects;
using ERP.Business.Server;
using ERP.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// The user that is currently using the client.
        /// </summary>
        protected User CurrentUser { get; }

        protected virtual bool BeforeGetData() { return true; }
        
        protected virtual bool BeforeCreate() { return true; }
        
        protected virtual bool BeforeDelete() { return true; }
        
        protected virtual bool BeforeChange() { return true; }

        /// <summary>
        /// Current context. This is the BusinessObject containing all the Data.
        /// </summary>
        public T_BusinessObject Data { get; private set; } = new T_BusinessObject();

        /// <summary>
        /// Gets the data from server.
        /// </summary>
        /// <param name="ID"></param>
        /// <exception cref="Exception"></exception>
        public void GetData(int ID)
        {
            if (BeforeGetData())
            {
                Result Result = CommandCollection.GetInstance<T_CommandCollection>().GetData(CurrentUser, ID);
                if (!Result.Error)
                {
                    Data.Deserialize(Result.ReturnValue);
                }
                else 
                {
                    throw new Exception(Result.ErrorMessage);
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
                Result Result = CommandCollection.GetInstance<T_CommandCollection>().Create(CurrentUser, Data);
                if (!Result.Error)
                {
                    Data.Deserialize(Result.ReturnValue);
                    this.Data = Data;
                }
                else
                {
                    throw new Exception(Result.ErrorMessage);
                }
            }
        }

        /// <summary>
        /// Deletes the object on server.
        /// </summary>
        /// <param name="ID"></param>
        /// <exception cref="Exception"></exception>
        public void Delete(int ID)
        {
            if (BeforeDelete())
            {
                Result Result = CommandCollection.GetInstance<T_CommandCollection>().Delete(CurrentUser, ID);
                if (!Result.Error)
                {
                    Data = new T_BusinessObject();
                }
                else
                {
                    throw new Exception(Result.ErrorMessage);
                }
            }
        }

        /// <summary>
        /// Updates data on the server.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Data"></param>
        /// <exception cref="Exception"></exception>
        public void Change(int ID, T_BusinessObject Data)
        {
            if (BeforeChange())
            {
                Result Result = CommandCollection.GetInstance<T_CommandCollection>().Change(CurrentUser, ID, Data);
                if (!Result.Error)
                {
                    Data.Deserialize(Result.ReturnValue);
                    this.Data = Data;
                }
                else
                {
                    throw new Exception(Result.ErrorMessage);
                }
            }
        }

        /// <summary>
        /// Gets a list of all objects from the server.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<T_BusinessObject> GetList()
        {
            Result Result = CommandCollection.GetInstance<T_CommandCollection>().GetList(CurrentUser);
            if (!Result.Error)
            {
                return Json.Deserialize<List<T_BusinessObject>>(Result.ReturnValue);
            }
            else
            {
                throw new Exception(Result.ErrorMessage);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="CurrentUser">The user that is currently using the client.</param>
        public BusinessObjectClient(User CurrentUser) 
        {
            this.CurrentUser = CurrentUser;
        }
    }
}
