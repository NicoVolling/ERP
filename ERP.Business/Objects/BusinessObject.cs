﻿using ERP.BaseLib.Objecs;
using ERP.BaseLib.Serialization;
using ERP.Exceptions.ErpExceptions;
using System.Reflection;

namespace ERP.Business.Objects
{
    /// <summary>
    /// This class contains all Properties (Data).
    /// </summary>
    public abstract class BusinessObject : PropertyChangedNotifier
    {
        private int iD = -1;

        /// <summary>
        /// Identifier should be unique
        /// </summary>
        public int ID
        { get => iD; set { iD = value; NotifyPropertyChanged(); } }

        public void Deserialize(string Raw)
        {
            BusinessObject BO = (BusinessObject)Json.Deserialize(Raw, this.GetType());
            Deserialize(BO);
        }

        public void Deserialize(BusinessObject Object)
        {
            if (Object.GetType() != this.GetType())
            {
                throw new TypeMismatchErpException(this.GetType(), Object.GetType());
            }

            foreach (PropertyInfo Property in this.GetType().GetProperties())
            {
                if (Property.CanRead && Property.CanWrite && Property.GetGetMethod() is MethodInfo && Property.GetSetMethod() is MethodInfo)
                {
                    Property.SetValue(this, Property.GetValue(Object));
                }
            }
        }

        public virtual string Serialize()
        {
            return Json.Serialize(this);
        }

        public abstract override string ToString();
    }
}