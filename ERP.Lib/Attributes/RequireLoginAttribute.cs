using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.BaseLib.Attributes
{
    /// <summary>
    /// This Attribute is needed for enabling Usercheck.
    /// <para />
    /// Can be used on command only.
    /// </summary>
    public sealed class RequireLoginAttribute : Attribute
    {
        /// <summary>
        /// The Permissionlevel wich user needs for getting access to the command.
        /// </summary>
        public int PermissionLevel { get; }

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="PermissionLevel">The Permissionlevel wich user needs for getting access to the command.</param>
        public RequireLoginAttribute(int PermissionLevel) 
        {
            this.PermissionLevel = PermissionLevel;
        }
    }
}
