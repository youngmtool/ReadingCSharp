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
    }
}
}
