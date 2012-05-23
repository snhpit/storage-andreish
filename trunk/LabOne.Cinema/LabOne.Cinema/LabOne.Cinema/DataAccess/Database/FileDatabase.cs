using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using LabOne.Cinema.Entities;

namespace LabOne.Cinema.DataAccess.Database
{
    public class FileDataBase : DataBase
    {
        public FileDataBase(string basePath)
            : base(basePath)
        {
            FileExtension = "txt";
        }

        internal override bool WriteData<T>(T data)
        {
            string filename = GenerateFileName(((dynamic)data).ToArray().GetType());
            var formatter = new BinaryFormatter();

            using (var writer = File.OpenWrite(filename))
            {
                formatter.Serialize(writer, data);
            }

            return true;
        }

        internal override IEnumerable<T> ReadFile<T>()
        {
            string filename = GenerateFileName(typeof(T));
            var readData = InternalRead<T>(filename);
            if (readData == null)
            {
                throw new FileNotFoundException(string.Format("The file on this path {0} was not found", filename));
            }
            return readData;
        }

        protected override IEnumerable<T> InternalRead<T>(string filename)
        {
            if (File.Exists(filename))
            {
                List<T> data;
                var formatter = new BinaryFormatter();
                using (var reader = File.OpenRead(filename))
                {
                    data = (List<T>)formatter.Deserialize(reader);
                }
                if (data[0] == null)
                {
                    throw new ItemNotFoundException(string.Format("Item {0} not load.", typeof(T).Name));
                }
                return data;
            }
            return null;
        }
    }
}