using System;
using System.Collections.Generic;
using System.IO;

namespace LabOne.Cinema.DataAccess.Database
{
    public abstract class Database
    {
        protected Database(string basePath)
        {
            BasePath = basePath;
        }

        public string BasePath { get; private set; }

        public string FileExtension { get; set; }

        public abstract string Write<T>(T row, string id);

        public abstract T ReadFile<T>(string id);

        protected abstract T InternalRead<T>(string filename);

        public abstract IEnumerable<T> ReadAllFiles<T>();

        //public abstract void Delete<T>(string id);

        //public abstract void Delete(Type type, string id);

        //public virtual string CreateID()
        //{
        //    return Guid.NewGuid().ToString();
        //}

        protected string GenerateFilename(Type type, string id)
        {
            var fileName = type.Name.Remove(type.Name.IndexOf('[')) + "s";
            return Path.Combine(BasePath, string.Format("{0}.{1}", fileName, FileExtension));
        }
    }
}