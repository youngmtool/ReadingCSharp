﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//c Create a console application FunWithEnums to examine the enumeration type.
//c Add enum EmpType holding 4 items.
//c Update EmpType. I set 102 numerical value for Manager named constant. 
//c Update EmpType. In enum, named constant should be unique. But numerical value for the named constant doesn't need to be unique. And also numerical value for the named constant doesn't need to follow sequential ordering.
//c Update EmpType. I can change the underlying data type (byte, short, int, or long) for numerical value mapped to the corresponding named constant.
//c When I set underlying data type for numerical value in the way I like, I must follow the range of each data type. For example, When I use byte data type, I can't store 999 as a numerical value for the named constant VicePresident because 999 is out of range for the byte data type.
//c Add AskForBonus() which uses EmpType enum type.
//c Assign a specific value for Contractor of EmpType enum into EmpType type local variable. And I pass it into AskForBonus() and within it, I put that enum type value to the switch statement.
//c Add ThisMethodWillNotCompile(). This method can't be compiled because when I use enum type I can't assign a value which is not existing in the enum. In this case, the compile-time error happens. And when I don't specify a scope for the enum, I can't access any enum type so I get the compile-time error.
//c Updata Main() to use Enum.GetUnderlyingType() which returns the underlying data type for the enum storage. In this case, we're now using byte data type.
//c Use typeof operator to get the type of the assembly. I can get the type from the assembly in 3 ways. 1st is to use Type class. 2nd is to use typeof operator. Both are using strongly typed name for the type what I want to know. 3rd is to use reflection. By 3rd way, I can get the metadata(ex. type information) at runtime, not compiletime by specifying just liter string for the type name what I want to know.
//c Updata Main() to retrieve a specific name from the enum by using Enum.ToString().
//c Update Main() to retrieve a specific value from the enum by using explicit data type case syntax on enum variable.
//c Add EvaluateEnum(). This method will print out the details of any enum. Especially note the Enum.GetValues(). This method creates the corresponding array from the specific enum type.
//c Updata Main(). I create EmpType enum type local variable. I assign the value for the EmpType.Contractor to the e2 local variable. And I pass it into the EvaluateEnum() as an argument.

namespace FunWithEnums
{

    // Compile-time error! 999 is too big for a byte!
    enum EmpType : byte
    {
        Manager = 10,
        Grunt = 1,
        Contractor = 100,
        VicePresident = 9
        //VicePresident = 999
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**** Fun with Enums *****");
            // Make an EmpType variable.
            EmpType emp = EmpType.Contractor;
            AskForBonus(emp);


            // Print storage for the enum.
            Console.WriteLine("EmpType uses a {0} for storage",
              Enum.GetUnderlyingType(emp.GetType()));

            // This time use typeof to extract a Type.
            Console.WriteLine("EmpType uses a {0} for storage",
                Enum.GetUnderlyingType(typeof(EmpType)));

            // Prints out "emp is a Contractor".
            Console.WriteLine("emp is a {0}.", emp.ToString());


            // Prints out "Contractor = 100".
            Console.WriteLine("{0} name's value = {1}", emp.ToString(), (byte)emp);


            EmpType e2 = EmpType.Contractor;

            // These types are enums in the System namespace.
            DayOfWeek day = DayOfWeek.Monday;
            ConsoleColor cc = ConsoleColor.Gray;

            EvaluateEnum(e2);
            EvaluateEnum(day);
            EvaluateEnum(cc);

            Console.ReadLine();
        }

        // Enums as parameters.
        static void AskForBonus(EmpType e)
        {
            switch (e)
            {
                case EmpType.Manager:
                    Console.WriteLine("How about stock options instead?");
                    break;
                case EmpType.Grunt:
                    Console.WriteLine("You have got to be kidding...");
                    break;
                case EmpType.Contractor:
                    Console.WriteLine("You already get enough cash...");
                    break;
                case EmpType.VicePresident:
                    Console.WriteLine("VERY GOOD, Sir!");
                    break;
            }
        }


        //static void ThisMethodWillNotCompile()
        //{
        //    // Error! SalesManager is not in the EmpType enum!
        //    EmpType emp = EmpType.SalesManager;

        //    // Error! Forgot to scope Grunt value to EmpType enum!
        //    emp = Grunt;
        //}


        static void EvaluateEnum(System.Enum e)
        {
            Console.WriteLine("=> Information about {0}", e.GetType().Name);

            Console.WriteLine("Underlying storage type: {0}",
              Enum.GetUnderlyingType(e.GetType()));

            // Get all name/value pairs for incoming parameter.
            Array enumData = Enum.GetValues(e.GetType());
            Console.WriteLine("This enum has {0} members.", enumData.Length);

            // Now show the string name and associated value, using the D format
            // flag (see Chapter 3).
            for (int i = 0; i < enumData.Length; i++)
            {
                Console.WriteLine("Name: {0}, Value: {0:D}",
                  enumData.GetValue(i));
            }
            Console.WriteLine();
        }
    }
}

