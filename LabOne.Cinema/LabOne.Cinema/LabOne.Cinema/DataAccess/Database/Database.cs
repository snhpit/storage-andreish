using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace LabOne.Cinema.DataAccess.Database
{
    public abstract class DataBase : IDataBase
    {
        protected DataBase() { }

        protected DataBase(string basePath)
        {
            BasePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\", basePath));
        }

        public string BasePath { get; protected set; }

        public string FileExtension { get; protected set; }

        public abstract bool WriteData<T>(T data);

        public abstract IEnumerable<T> ReadFile<T>();

        protected abstract IEnumerable<T> InternalRead<T>(string filename);

        protected string GenerateFileName(Type type)
        {
            string fileName = type.IsArray ? type.Name.Remove(type.Name.IndexOf('[')) + "s" : type.Name + "s";

            return Path.Combine(BasePath, string.Format("{0}.{1}", fileName, FileExtension));
        }

        public bool Delete<T>()
        {
            return Delete(typeof(T));
        }

        protected bool Delete(Type type)
        {
            string filename = GenerateFileName(type);
            if (File.Exists(filename))
            {
                File.Delete(filename);
                return true;
            }
            return false;
        }
    }
}