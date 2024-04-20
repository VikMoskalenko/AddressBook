namespace AddressBook

{
    public partial class Program
    { 
        public class Person
        {
            public string Name { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public Person(string name, string lastName, string address)
            {
                Name = name;
                LastName = lastName;
                Address = address;
            }

        }

    }

}

