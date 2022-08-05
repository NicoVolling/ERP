using ERP.BaseLib.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.BaseLib.Objects
{
    public class Result<T> : Result
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Result()
        {
            this.ReturnValue = String.Empty;
            this.ErrorMessage = String.Empty;
        }

        /// <summary>
        /// Constructor for answer on a successful request
        /// </summary>
        /// <param name="ReturnValue">All data wich server sends to client.</param>
        public Result(T ReturnValue)
        {
            this.ReturnValue = ReturnValue.Serialize();
            this.ErrorMessage = String.Empty;
        }

        /// <summary>
        /// Constructor for answer on a failed request
        /// </summary>
        /// <param name="Exception">The Exception that has been thrown</param>
        public Result(Exception Exception)
        {
            this.ReturnValue = String.Empty;
            this.Error = true;
            this.ErrorType = Exception.GetType().Name;
            this.ErrorMessage = Exception.Message;
        }

        public override string ReturnValueType { get => GetReturnTypeFormatted(typeof(T)); }

        private static string GetReturnTypeFormatted(Type type)
        {
            string returntype = "";
            if (type.GenericTypeArguments.Length < 1)
            {
                returntype = type.Name;
            }
            else
            {
                returntype = string.Concat(type.Name.TakeWhile(o => o != '`'));
                returntype += "<";
                foreach (Type tp in type.GenericTypeArguments)
                {
                    if (!returntype.EndsWith("<")) { returntype += ", "; }
                    returntype += tp.Name;
                }
                returntype += ">";
            }
            return returntype;
        }
    }
}