using ContactManager.Models;
using System.Collections.Generic;

namespace ContactManager.Contracts
{
    public interface IFileService
    {
        List<Contact> ReadFromFile(string filePath = @"ContactsData.json");
        void WriteToFile(List<Contact> data, string filePath = @"ContactsData.json", bool append = false);
    }

}
