using ERP.BaseLib.Objects;
using ERP.BaseLib.Serialization;
using ERP.Business.Objects.Attributes;
using ERP.Exceptions.ErpExceptions;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ERP.Business.Objects
{
    /// <summary>
    /// This class contains all Properties (Data).
    /// </summary>
    public abstract class BusinessObject : PropertyChangedNotifier
    {
        private Guid iD = Guid.Empty;

        public static BusinessObject Empty { get => new BusinessObjectEmpty(); }

        /// <summary>
        /// Identifier should be unique
        /// </summary>
        public Guid ID
        { get => iD; set { iD = value; NotifyPropertyChanged(); } }

        public bool IsEmpty()
        {
            return ID == Empty.ID;
        }

        public abstract string OnToString();

        public override string ToString()
        {
            return $"{OnToString()}";
        }

        private class BusinessObjectEmpty : BusinessObject
        {
            public BusinessObjectEmpty()
            {
                ID = Guid.Empty;
            }

            public override string OnToString()
            {
                return $"No Selection";
            }
        }
    }
}