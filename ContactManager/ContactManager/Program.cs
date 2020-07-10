using ContactManager.Services;
using System;

namespace ContactManager
{
    class Program
    {
        static UI ui = new UI();

        static void Main(string[] args)
        {
            StartMenu();
        }

        public static void StartMenu()
        {           
            MenuOptions();
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out int input))
                {
                    input = -1;
                    Console.WriteLine("Value must be numeric!");
                }
                switch (input)
                {
                    case 0: //exit
                        return;
                    case 1: //list
                        ui.ListAll();
                        break;
                    case 2: //create
                        ui.Create();
                        break;
                    case 3: //edit                     
                        ui.ListAll();
                        Console.WriteLine("Enter index of contact you want to edit!");

                        if (!int.TryParse(Console.ReadLine(), out int id))
                            Console.WriteLine("Value must be numeric!");

                        ui.Update(id - 1);                   
                        break;
                    case 4: //delete
                        ui.ListAll();
                        Console.WriteLine("Enter index of contact you want to delete!");

                        if (!int.TryParse(Console.ReadLine(), out id))
                            Console.WriteLine("Value must be numeric!");

                        ui.Delete(id-1);
                        break;
                    default: //error
                        Console.WriteLine("You can enter only numbers between 0 and 4 [0;4]");
                        break;
                }
            }
        }


        public static void MenuOptions()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("0 - Close");
            Console.WriteLine("1. List all contacts");
            Console.WriteLine("2. Create new contact");
            Console.WriteLine("3. Edit contact");
            Console.WriteLine("4. Delete contact");
        }
    }
}
