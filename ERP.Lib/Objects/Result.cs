using ERP.BaseLib.Serialization;

namespace ERP.BaseLib.Objects
{
    /// <summary>
    /// A result contains all needed information when the server answers the client.
    /// </summary>
    public sealed class Result
    {
        /// <summary>
        /// Wether the request was successful.
        /// </summary>
        public bool Error;

        /// <summary>
        /// If request has failed, further informations are here.
        /// </summary>
        public string ErrorMessage;

        /// <summary>
        /// The Type the Error. Example: ErpException
        /// </summary>
        public string ErrorType;

        /// <summary>
        /// All data wich server sends to client.
        /// </summary>
        public string ReturnValue;

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
        public Result(string ReturnValue)
        {
            this.ReturnValue = ReturnValue;
            this.ErrorMessage = String.Empty;
        }

        /// <summary>
        /// Constructor for answer on a successful request
        /// </summary>
        /// <param name="ReturnValue">All data wich server sends to client.</param>
        public Result(Object ReturnValue)
        {
            this.ReturnValue = Json.Serialize(ReturnValue);
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

        /// <summary>
        /// Result for False-Answer
        /// </summary>
        public static Result False { get => new("False"); }

        /// <summary>
        /// Everything worked well.
        /// </summary>
        public static Result OK { get => new("OK"); }

        /// <summary>
        /// Result for True-Answer
        /// </summary>
        public static Result True { get => new("True"); }

        public static implicit operator Result(string String)
        {
            try
            {
                return Json.Deserialize<Result>(String);
            }
            catch
            {
                throw;
            }
        }

        public static implicit operator string(Result Result)
        {
            return Result.ToString();
        }

        public static bool operator !=(Result Result1, Result Result2)
        {
            return !(Result1 == Result2);
        }

        public static bool operator ==(Result Result1, Result Result2)
        {
            return
                Result1.Error == Result2.Error &&
                Result1.ErrorMessage == Result2.ErrorMessage &&
                Result1.ReturnValue == Result2.ReturnValue;
        }

        public override bool Equals(object obj)
        {
            if (obj is Result Result1)
            {
                return this == Result1;
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return Json.Serialize(this);
        }
    }
}