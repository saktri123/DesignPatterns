using System;

namespace ProxyCodingExcercise
{
    public class Person : IPerson
    {
        public int Age { get; set; }

        public string Drink()
        {
            return "drinking";
        }

        public string Drive()
        {
            return "driving";
        }

        public string DrinkAndDrive()
        {
            return "driving while drunk";
        }
    }

    public class ResponsiblePerson : IPerson
    {
        private readonly Person person;

        public ResponsiblePerson(Person person)
        {
            this.person = person;
        }

        public int Age
        {
            get => person.Age;
            set { person.Age = value; }
        }

        public string Drink()
        {
            if (person.Age < 18) return "too young";
            return person.Drink();
        }

        public string DrinkAndDrive()
        {
            return "dead";
        }

        public string Drive()
        {

            if (person.Age < 16) return "too young";
            return person.Drive();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
