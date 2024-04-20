namespace AddressBook

{
    public partial class Program
    {
        public class AddressBook
        {
            private WriteAndReadToFile wtf;
            private List<Person> addressBookList = new List<Person>();
            public List<Person> AddressBookList
            {
                get { return addressBookList; }
                set { addressBookList = value; }
            }
            public AddressBook()
            {
                AddressBookList = new List<Person>();
                wtf = new WriteAndReadToFile("user.txt");
            }
            public void CreateUser()
            {
                Console.WriteLine("Enter first name: ");
                //var name = Console.ReadLine();
                string name = GetValidInputInParams();
                Console.WriteLine("Enter last name : ");
                //var lastName = Console.ReadLine();
                string lastName = GetValidInputInParams();
                Console.WriteLine("Enter your address : ");
                string address = GetValidInputInParams();
                Person person = new Person(name, lastName, address);
                AddPersonToList(person);
                wtf.WriteUserToFile(person);
            }

            private string GetValidInputInParams()
            {
                string input = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(input) || input.Any(char.IsDigit) || input.Any(char.IsPunctuation))
                {
                    Console.WriteLine("Invalid input. Please enter again: ");
                    input = Console.ReadLine();
                }
                return input;
            }
            private void AddPersonToList(Person person)
            {
                AddressBookList.Add(person);
            }
            public void DeleteUser()
            {
                Console.WriteLine("Enter a name of the user you would like to delete ");
                var name = Console.ReadLine();
                Console.WriteLine("Enter last name of the person: ");
                var lastName = Console.ReadLine();
                AddressBookList.RemoveAll(x => x.Name == name && x.LastName == lastName);
                wtf.UpdateUserOnFile(AddressBookList);
            }
            public void ShowAll()
            {
                foreach (var person in AddressBookList)
                {
                    Console.WriteLine($"Name : {person.Name}, Last Name: {person.LastName}, Address: {person.Address}");
                }

            }
            public void UpdateUserInfo()
            {
                Console.WriteLine("Enter the first name of the user you want to update: ");
                var oldFirstName = Console.ReadLine();
                Console.WriteLine("Which info do you want to update? ");
                Console.WriteLine(" 1. Name, 2. Last Name, 3. Address");
                var userOption = Console.ReadLine();
                UpdateUserFunction(userOption, oldFirstName);
            }
            private void UpdateUserFunction(string userOption, string oldFirstName)
            {
                var personsWithMatchingFirstName = AddressBookList.Where(person => person.Name == oldFirstName);

                string newValue;

                if (userOption == "1")
                {
                    Console.WriteLine("Enter a new first name : ");
                    newValue = Console.ReadLine();
                    foreach (var person in personsWithMatchingFirstName)
                    {
                        person.Name = newValue;
                    }
                }
                else if (userOption == "2")
                {
                    Console.WriteLine("Enter a new last name for the user: ");
                    newValue = Console.ReadLine();
                    foreach (var person in personsWithMatchingFirstName)
                    {
                        person.LastName = newValue;
                    }
                }
                else if (userOption == "3")
                {
                    Console.WriteLine("Enter a new address for the user: ");
                    newValue = Console.ReadLine();
                    foreach (var person in personsWithMatchingFirstName)
                    {
                        person.Address = newValue;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");
                }
                wtf.UpdateUserOnFile(AddressBookList);
            }
        }
    }
}

