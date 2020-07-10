using ContactManager.Contracts;
using ContactManager.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace ContactManager.Services
{
    public class FileService : IFileService
    {
        public List<Contact> ReadFromFile(string filePath = @"ContactsData.json")
        {
            TextReader reader = null;
            try
            {
                if (!File.Exists(filePath))
                    return null;
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Contact>>(fileContents);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        public void WriteToFile(List<Contact> data, string filePath = @"ContactsData.json", bool append = false)
        {
            TextWriter writer = null;
            try
            {               
                var contentsToWriteToFile = JsonConvert.SerializeObject(data);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }
}
