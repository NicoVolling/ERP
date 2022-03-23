using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.BaseLib.Objects
{
    /// <summary>
    /// The User is needed for getting Access to protected commands.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Username.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// Determines the accesslevel.
        /// </summary>
        public int PermissionLevel { get; private set; }

        /// <summary>
        /// If the user was loggedin successfully.
        /// </summary>
        public bool LoggedIn { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Name">Username.</param>
        /// <param name="Password">Password.</param>
        public User(string Name, string Password) 
        {
            this.Name = Name;
            this.Password = Password;
        }

        /// <summary>
        /// Trys to log in the user
        /// </summary>
        /// <param name="Name">Username.</param>
        /// <param name="Password">Password.</param>
        /// <returns>A User</returns>
        public static User Login(string Name, string Password) 
        {
            return new User(Name, Password) { LoggedIn = true, PermissionLevel = int.MaxValue };
            //TODO: Login
        }
    }
}
