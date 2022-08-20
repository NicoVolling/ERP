using ERP.Exceptions.ErpExceptions;
using System.IO.Enumeration;

namespace ERP.IO.FileSystem
{
    /// <summary>
    /// Provides functions for saving the Data withing the class.
    /// </summary>
    public class FileSaver
    {
        public FileSaver(string Namespace, string Name)
        {
            Filename = $"{Namespace}.{Name}";
        }

        protected string Filename { get; }

        public void Delete(Guid ID)
        {
            if (ObjectExists(ID))
            {
                System.IO.Directory.Delete(GetFolderName(ID), true);
            }
        }

        public string GetBasePath()
        {
            string result = System.IO.Path.Combine(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Data", Filename));
            if (!System.IO.Directory.Exists(result))
            {
                System.IO.Directory.CreateDirectory(result);
            }
            return result;
        }

        public string GetFileName(Guid ID)
        {
            return System.IO.Path.Combine(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Data", Filename, ID.ToString().ToUpper(), "Data.json"));
        }

        public string GetFolderName(Guid ID)
        {
            return System.IO.Path.Combine(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Data", Filename, ID.ToString().ToUpper()));
        }

        public IEnumerable<Guid> GetList()
        {
            foreach (string str in System.IO.Directory.GetDirectories(GetBasePath()))
            {
                if (Guid.TryParse(new DirectoryInfo(str).Name, out Guid ID))
                {
                    yield return ID;
                }
            }
        }

        public string GetMeta(Guid ID)
        {
            if (ObjectExists(ID))
            {
                return System.IO.File.ReadAllText(GetMetaFileName(ID));
            }
            throw new MissingObjectErpException(typeof(Guid), ID);
        }

        public string GetMetaFileName(Guid ID)
        {
            return System.IO.Path.Combine(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Data", Filename, ID.ToString().ToUpper(), "Meta.json"));
        }

        public string GetObject(Guid ID)
        {
            if (ObjectExists(ID))
            {
                return System.IO.File.ReadAllText(GetFileName(ID));
            }
            throw new MissingObjectErpException(typeof(Guid), ID);
        }

        public bool ObjectExists(Guid ID)
        {
            return System.IO.File.Exists(GetFileName(ID));
        }

        public void Save(Guid ID, string Data, string MetaData)
        {
            if (!System.IO.Directory.Exists(GetFolderName(ID)))
            {
                System.IO.Directory.CreateDirectory(GetFolderName(ID));
            }
            System.IO.File.WriteAllText(GetFileName(ID), Data);
            System.IO.File.WriteAllText(GetMetaFileName(ID), MetaData);
        }
    }
}