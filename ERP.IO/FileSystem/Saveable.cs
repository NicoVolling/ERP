﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.IO.FileSystem
{
    /// <summary>
    /// Provides functions for saving the Data withing the class.
    /// </summary>
    public abstract class Saveable
    {

        /// <summary>
        /// The relative path to the file.
        /// </summary>
        protected abstract string Filename { get; }

        /// <summary>
        /// Serializes the data in the class.
        /// </summary>
        /// <returns>Serialized Data.</returns>
        protected abstract string Serialize();

        /// <summary>
        /// Deserializes the data in the class.
        /// </summary>
        /// <param name="Raw">Serialized Data.</param>
        protected abstract void Deserialize(string Raw);

        /// <summary>
        /// Will be executed before data will be saved.
        /// </summary>
        /// <returns>If it is allowed to save data now.</returns>
        protected virtual bool CanSave() { return true; }

        /// <summary>
        /// Will be executed before data will be loaded.
        /// </summary>
        /// <returns>If it is allowed to load data now.</returns>
        protected virtual bool CanLoad() { return true; }

        /// <summary>
        /// Saves all data.
        /// </summary>
        public void Save() 
        {
            if(!CanSave()) { return; }
            try 
            {
                if (!System.IO.Directory.Exists(System.IO.Path.Combine(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Data"))))
                {
                    System.IO.Directory.CreateDirectory(System.IO.Path.Combine(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Data")));
                }
                System.IO.File.WriteAllText(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Data", Filename), Serialize());
            } 
            catch 
            { 
                throw; 
            }
        }

        /// <summary>
        /// Loads all data.
        /// </summary>
        public void Load() 
        {
            if (!CanLoad()) { return; }
            try
            {
                Deserialize(System.IO.File.ReadAllText(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Data", (Filename))));
            }
            catch
            {
                throw;
            }
        }
    }
}
