using ContactManager.Contracts;
using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactManager.Services
{
    public class ContactService : IContactService
    {
        public IFileService FileService { get; set; }

        public List<Contact> GetAllContacts()
        {
            var contacts = FileService.ReadFromFile();
            return contacts;
        }

        public Contact CreateContact(Contact contact)
        {
            //check if all required values entered
            if (contact == null || string.IsNullOrEmpty(contact.FirstName) || 
                string.IsNullOrEmpty(contact.LastName) || contact.Phones == null)
            {
                //at this point i would throw exception, but it will look ugly in console so just WriteLine
                Console.WriteLine("Error! All required fields must be filed with data!");
                return null;
            }
            
            var allContacts = GetAllContacts();

            //if contacts file empty
            if (allContacts == null)
                allContacts = new List<Contact>() { contact };
            else
            {
                //if same number enterd multiple times - remove it
                contact.Phones = contact.Phones.Distinct().ToList();
                if (!CheckForUniquePhones(allContacts, contact))
                    return null;

                allContacts.Add(contact);
            }

            //inserting to a file
            FileService.WriteToFile(allContacts);

            return contact;
        }

        public bool DeleteContact(int ind)
        {
            //fetch all existing contacts from file
            var contactsList = GetAllContacts();
            if (contactsList.Count < ind || ind < 0)
            {
                Console.WriteLine("Error! Bad index entered!");
                return false;
            }
            //remove selected from list
            contactsList.RemoveAt(ind);
            //update file with deleted contact
            FileService.WriteToFile(contactsList);

            return true;
        }

        public string UpdateContact(int ind, Contact contact)
        {
            var contactsList = GetAllContacts();

            //nothing changed return
            if (contactsList[ind].Equals(contact))
                return "Nothing to change!";

            //check if entered phone numbers unique 
            if (!CheckForUniquePhones(contactsList, contact))
                return "";
       
            //replace
            contactsList[ind] = contact;
            //update in a file
            FileService.WriteToFile(contactsList);
            return "Contact updated successfully!";
        }

        private bool CheckForUniquePhones(List<Contact> allContacts, Contact newContact)
        {
            //getting all registered phone numbers
            List<string> allPhones = new List<string>();
            foreach (var con in allContacts)
                allPhones = allPhones.Concat(con.Phones).ToList();

            //check if phone number unique
            if (allPhones.Intersect(newContact.Phones).Any())
            {
                //at this point i would throw exception, but it will look ugly in console so just WriteLine
                Console.WriteLine("Error! Such phone number already taken!");
                return false;
            }
            return true;
        }
    }
}
