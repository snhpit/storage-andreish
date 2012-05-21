using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using LabOne.Cinema.Entities;

namespace LabOne.Cinema.DataAccess.Database
{
    internal class FileDataBase : DataBase
    {
        public FileDataBase(string basePath)
            : base(basePath)
        {
            FileExtension = "txt";
        }

        public override bool WriteData<T>(T data)
        {
            if (data.GetType().IsValueType)
            {
                throw new IsValueTypeException();
            }

            string filename = GenerateFileName(((dynamic)data).ToArray().GetType());
            var formatter = new BinaryFormatter();

            using (var writer = File.OpenWrite(filename))
            {
                formatter.Serialize(writer, data);
            }

            return true;
        }

        public override IEnumerable<T> ReadFile<T>()
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
                var formatter = new BinaryFormatter();
                using (var reader = File.OpenRead(filename))
                {
                    return (List<T>)formatter.Deserialize(reader);
                }
            }
            return null;
        }
    }
}