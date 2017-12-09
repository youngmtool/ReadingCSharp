﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//c Create a console app Employees.
//c It's still impossible for object user (from outside) to access to the protected field directly. protected fields can be accessed by the class defining it or derived class from the defining class.
//c Update a method Main(). Via Manager(class) type object derived from Employee, I invoke GetBenefitCost() inherited from its base class Employee with using functionality of BenefitPackage class GetBenefitCost().
//c Update a method Main() to use a nested BenefitPackageLevel(enum) type inside of a nested BenefitPackage(class) type of Employee(class) type.
//c Update a method Main(). Via Manager(class) type object, I invoke a derived method GiveBonus() of Employee(class) type. I do same thing via SalesPerson(class) type object. Now the problem is I'm using same bonus logic for all employees such as Manager, SalesPerson, Part-time SalesPerson.

namespace Employees
{
    class Program
    {
        // Create a subclass object and access base class functionality.
        static void Main(string[] args)
        {
            Console.WriteLine("***** The Employee Class Hierarchy *****\n");
            SalesPerson fred = new SalesPerson();
            fred.Age = 31;
            fred.Name = "Fred";
            fred.SalesNumber = 50;

            // Assume Manager has a constructor matching this signature:
            // (string fullName, int age, int empID, float currPay, string ssn, int numbOfOpts)
            Manager chucky = new Manager("Chucky", 50, 92, 100000, "333-23-2322", 9000);
            Console.WriteLine(chucky.Name);

            // Error! Can't access protected data from client code.
            //Employee emp = new Employee();
            //emp.empName = "Fred";

            double cost = chucky.GetBenefitCost();
            Console.WriteLine($"cost: {cost}");

            // Define my benefit level.
            Employee.BenefitPackage.BenefitPackageLevel myBenefitLevel =
              Employee.BenefitPackage.BenefitPackageLevel.Platinum;
            Console.WriteLine($"myBenefitLevel: {myBenefitLevel}");

            // Give each employee a bonus?
            // chucky = new Manager("Chucky", 50, 92, 100000, "333-23-2322", 9000);
            chucky.GiveBonus(300);
            chucky.DisplayStats();
            Console.WriteLine();
            SalesPerson fran = new SalesPerson("Fran", 43, 93, 3000, "932-32-3232", 31);
            fran.GiveBonus(200);
            fran.DisplayStats();

            Console.ReadLine();
        }
    }
}