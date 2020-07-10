using ContactManager.Models;
using System.Collections.Generic;

namespace ContactManager.Contracts
{
    public interface IContactService
    {
        Contact CreateContact(Contact contact);
        List<Contact> GetAllContacts();
        bool DeleteContact(int ind);
        string UpdateContact(int ind, Contact contact);
    }
}
