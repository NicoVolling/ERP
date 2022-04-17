namespace ERP.IO.FileSystem
{
    /// <summary>
    /// Provides functions for saving the Data withing the class.
    /// </summary>
    public interface IFileSaver
    {
        /// <summary>
        /// The relative path to the file.
        /// </summary>
        protected internal string Filename { get; }

        /// <summary>
        /// Loads all data.
        /// </summary>
        public static void Load(IFileSaver Target)
        {
            if (!Target.CanLoad()) { return; }
            try
            {
                if (System.IO.File.Exists(System.IO.Path.Combine(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Data", Target.Filename + ".json"))))
                {
                    Target.Deserialize(System.IO.File.ReadAllText(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Data", Target.Filename + ".json")));
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Loads all data.
        /// </summary>
        public static void Save(IFileSaver Target)
        {
            if (!Target.CanSave()) { return; }
            try
            {
                if (!System.IO.Directory.Exists(System.IO.Path.Combine(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Data"))))
                {
                    System.IO.Directory.CreateDirectory(System.IO.Path.Combine(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Data")));
                }
                System.IO.File.WriteAllText(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Data", Target.Filename + ".json"), Target.Serialize());
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Loads all data.
        /// </summary>
        public void Load();

        /// <summary>
        /// Loads all data.
        /// </summary>
        public void Save();

        /// <summary>
        /// Will be executed before data will be loaded.
        /// </summary>
        /// <returns>If it is allowed to load data now.</returns>
        protected internal bool CanLoad();

        /// <summary>
        /// Will be executed before data will be saved.
        /// </summary>
        /// <returns>If it is allowed to save data now.</returns>
        protected internal bool CanSave();

        /// <summary>
        /// Deserializes the data in the class.
        /// </summary>
        /// <param name="Raw">Serialized Data.</param>
        protected internal void Deserialize(string Raw);

        /// <summary>
        /// Serializes the data in the class.
        /// </summary>
        /// <returns>Serialized Data.</returns>
        protected internal string Serialize();
    }
}