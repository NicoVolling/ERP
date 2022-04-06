using ERP.BaseLib.Helpers;
using ERP.BaseLib.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using ERP.Exceptions.ErpExceptions;

namespace ERP.Business.Objects
{
    /// <summary>
    /// This class contains all Properties (Data).
    /// </summary>
    public abstract class BusinessObject
    {

        /// <summary>
        /// Identifier should be unique
        /// </summary>
        public int ID { get; set; } = -1;

        public virtual string Serialize() 
        {
            return Json.Serialize(this);
        }

        public void Deserialize(string Raw) 
        {
            BusinessObject BO = (BusinessObject)Json.Deserialize(Raw, this.GetType());
            Deserialize(BO);
        }

        public void Deserialize(BusinessObject Object) 
        {
            if(Object.GetType() != this.GetType()) 
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

        public override abstract string ToString();
    }
}
