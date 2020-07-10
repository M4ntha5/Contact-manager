using ContactManager.Contracts;
using ContactManager.Models;
using ContactManager.Services;
using System;
using System.Linq;

namespace ContactManager
{
    public class UI
    {
        private readonly IContactService ContactService = new ContactService()
        {
            FileService = new FileService()
        };

        public void Create()
        {
            var contact = new Contact();
            Console.WriteLine("Enter first name (required):");
            contact.FirstName = Console.ReadLine();

            Console.WriteLine("Enter last name (required):");
            contact.LastName = Console.ReadLine();

            Console.WriteLine("Enter phones separated by comma(,) (required):");
            var phones = Console.ReadLine().Split(',').ToList();
            contact.Phones = phones;

            Console.WriteLine("Enter address (optional):");
            contact.Address = Console.ReadLine();


            var createdContact = ContactService.CreateContact(contact);
            if (createdContact != null)
                Console.WriteLine("Contact created successfully!");
        }

        public void Delete(int ind)
        {
            var success = ContactService.DeleteContact(ind);
            if(success)
                Console.WriteLine("Contact deleted successfully!");          
        }

        public void ListAll()
        {
            var contactsList = ContactService.GetAllContacts();
            if (contactsList == null || contactsList.Count == 0)
                Console.WriteLine("Contacts list is empty!");
            else
            {
                Console.WriteLine("{0,3} {1,10} {2,10} {3,15} {4,10}", 
                    "Ind", "First Name", "Last Name", "Address", "Phones");

                for(int i =0; i< contactsList.Count;i++)
                {
                    Console.Write("{0,3} {1,10} {2,10} {3,15}", (i+1).ToString(), contactsList[i].FirstName, 
                        contactsList[i].LastName, contactsList[i].Address);

                    for(int j =0;j < contactsList[i].Phones.Count; j++)
                    {
                        var format = j == 0 ? "{0,10}" : "{0,51}";
                        Console.WriteLine(format, contactsList[i].Phones[j]);
                    }
                }
            }
        }

        public void Update(int ind)
        {
            //getting old contact info
            var contactsList = ContactService.GetAllContacts();

            if (contactsList.Count < ind || ind < 0)
            {
                Console.WriteLine("Error! Bad index entered!");
                return;
            }

            var contact = contactsList[ind];

            Console.WriteLine($"Enter first name (required) (hit <Enter> for { contact.FirstName }): ");
            var line = Console.ReadLine();
            contact.FirstName = line.Trim() != "" ? line : contact.FirstName;

            Console.WriteLine($"Enter last name (required) (hit <Enter> for { contact.LastName }): ");
            line = Console.ReadLine();
            contact.LastName = line.Trim() != "" ? line : contact.LastName;

            Console.WriteLine($"Enter phones separated by comma(,) (required) " +
                $"(hit <Enter> for { String.Join(",", contact.Phones) }): ");
            var phones = Console.ReadLine().Split(',').ToList();
            contact.Phones = phones.Count() > 0 && !string.IsNullOrEmpty(phones[0]) ? phones : contact.Phones;

            Console.WriteLine($"Enter address (optional) (hit <Enter> for { contact.Address }):");
            line = Console.ReadLine();
            contact.Address = line.Trim() != "" ? line : contact.Address;

            var message = ContactService.UpdateContact(ind, contact);
            Console.WriteLine(message);
        }
    }
}
