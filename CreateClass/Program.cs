using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateClass
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee Kovacs = new Employee("Géza", DateTime.Now, 1000, "léhűtő");
            Kovacs.Room = new Room(111);
            Employee Kovacs2 = (Employee)Kovacs.Clone();
            Kovacs2.Room.Number = 112;
            Console.WriteLine(Kovacs.ToString());
            Console.WriteLine(Kovacs2.ToString());
            Console.ReadKey();

        }
    }

    public class Person
    {
        protected String name;
        protected DateTime birthDate;
        public Room Room;

        public Person() { }

        public Person(String name, DateTime birthDate)
        {
            this.name = name;
            this.birthDate = birthDate;
        }


        enum gender
        {
            MALE,
            FEMALE
        }

        public override string ToString()
        {
            String data = name + " " + birthDate;
            return data;
        }

    }

    public class Employee : Person, ICloneable
    {
        int salary;
        string profession;
        Room room;

        public int Salary { get => salary; set => salary = value; }
        public string Profession { get => profession; set => profession = value; }

        public override string ToString()
        {
            String data = name + " ";
            data += birthDate + " ";
            data += Salary + " ";
            data += Profession;
            return data;
        }

        public Employee (String name, DateTime birthDate, int salary, string profession)
        {
            this.name = name;
            this.birthDate = birthDate;
            this.salary = salary;
            this.profession = profession;

        }
        public object Clone()
        {
            Employee newEmployee = (Employee)this.MemberwiseClone();
            newEmployee.Room = new Room(Room.Number);
            return newEmployee;
        }

    }
    
    public class Room
    {
        public int Number;

        public Room(int number)
        {
            Number = number;
        }

        public int Number1 { get => Number; set => Number = value; }
    }



}
