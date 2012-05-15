using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace LabOne.Cinema.DataAccess.Database
{
    public class XmlDatabase : Database
    {
        public XmlDatabase(string basePath)
            : base(basePath)
        {
            FileExtension = "xml";
        }

        public override string Write<T>(T row, string id)
        {
            string filename = CreateFilename(typeof(T), id);
            var serializer = new XmlSerializer(typeof(T));

            //Stream writer = new FileStream(filename, FileMode.Append);
            TextWriter writer = new StreamWriter(filename);
            serializer.Serialize(writer, row);
            writer.Close();

            return filename;
        }

        public override T Read<T>(string id)
        {
            string filename = CreateFilename(typeof(T), id);
            return InternalRead<T>(filename);
        }

        protected override T InternalRead<T>(string filename)
        {
            if (File.Exists(filename))
            {
                var serializer = new XmlSerializer(typeof(T));
                //Stream reader = new FileStream(filename, FileMode.Open);
                TextReader reader = new StreamReader(filename);
                var value = (T)serializer.Deserialize(reader);
                reader.Close();

                return value;
            }
            return default(T);
        }

        public override T[] Read<T>()
        {
            string filePattern = string.Format("{0}-*.{1}", typeof(T).Name, FileExtension);
            string[] files = Directory.GetFiles(BasePath, filePattern, SearchOption.TopDirectoryOnly);
            return files.Select(InternalRead<T>).ToArray();
        }

        public override void Delete<T>(string id)
        {
            Delete(typeof(T), id);
        }

        public override void Delete(Type type, string id)
        {
            string filename = CreateFilename(type, id);
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }
    }
}