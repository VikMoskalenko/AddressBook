using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Configuration;

namespace AddressBook

{
    public partial class Program
    {
        static void Main(string[] args)
        {

            AddressBook abc = StartProgram();

            bool ProgramIsRunning = true;

            Console.WriteLine("___________Address book______________");

            while (ProgramIsRunning)
            {
                PrintUserOptions();

                var userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        abc.CreateUser();
                        break;
                    case "2":
                        abc.UpdateUserInfo();
                        break;
                    case "3":
                        abc.DeleteUser();
                        break;
                    case "4":
                        abc.ShowAll();
                        break;
                    case "x":
                        ProgramIsRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

            }

        }
        private static void PrintUserOptions()
        {
            Console.WriteLine("Choose one of the following options: ");
            Console.WriteLine(" 1. Create a new user");
            Console.WriteLine(" 2. Edit user information");
            Console.WriteLine(" 3. Delete existing user");
            Console.WriteLine(" 4. Show all users in the book");
            Console.WriteLine(" x. Exit");
        }
        private static AddressBook StartProgram()
        {
            AddressBook abc = new AddressBook();
            WriteAndReadToFile writer = new WriteAndReadToFile("user.txt");
            writer.ReadFromFile(abc);
            return abc;
        }

        public class WriteAndReadToFile
        {
            private readonly string UserTextFile;
            public WriteAndReadToFile(string fileName)
            {
                UserTextFile = "user.txt";
            }
            public void WriteUserToFile(Person person)
            {
                using (StreamWriter sw = new StreamWriter(UserTextFile, true))
                {
                    sw.WriteLine($"{person.Name}, {person.LastName}, {person.Address}");
                }

            }
            public void ReadFromFile(AddressBook abc)
            {
                string textLine;
                try
                {
                    using (StreamReader sr = new StreamReader(UserTextFile))
                    {
                        while ((textLine = sr.ReadLine()) != null)
                        {
                            string[] userInformation = textLine.Split(", ");
                            if (userInformation.Length >= 3)
                            {
                                Person p = new Person(userInformation[0], userInformation[1], userInformation[2]);
                                abc.AddressBookList.Add(p);
                            }

                        }

                    }

                }
                catch (FileNotFoundException fnf)
                {
                    Console.WriteLine("File does not exist: " + fnf);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong: " + e);
                }

            }

            public void UpdateUserOnFile(List<Person> addressBookList)
            {
                // Remove old row
                File.WriteAllText(UserTextFile, String.Empty);
                using (StreamWriter sw = new StreamWriter(UserTextFile))
                {
                    foreach (var person in addressBookList)
                    {
                        sw.WriteLine($"{person.Name}, {person.LastName}, {person.Address}");
                    }

                }

            }

        }

    }

}

