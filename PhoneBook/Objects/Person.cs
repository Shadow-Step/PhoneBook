using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    [Serializable]
    class Country
    {
        public Country() { }
        public Country(string name)
        {
            this.name = name;
        }

        string name = "Unknown";
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public List<Operator> operators = new List<Operator>();

        //
        public void addOperator(string name)
        {
            operators.Add(new Operator(name));
        }
    }
    [Serializable]
    class Operator
    {
        string name;
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public List<Person> persons = new List<Person>();

        public Operator() { }
        public Operator(string name)
        {
            this.name = name;
        }

        public void addPerson(string name = "Unknown",string phone = "Unknown",string adress = "Unknown")
        {
            persons.Add(new Person(name, phone, adress));
        }

    }
    [Serializable]
    public class Person
    {
        public Person() { }
        public Person(string name,string phone,string address)
        {
            this.Name = name;
            this.Phone = phone;
            this.Address = address;
        }

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
