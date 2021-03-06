﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//c Create a console application NullableTypes.
//c Updata Main() by declaring value data type local variables and assigning null to them. It's going to cause compile-time error. But when I declare string(class) data type local variables and assign null to it, that brings no issue.
//c Add a method LocalNullableVariables(). Within this, I declare value type local variables (nullable) and assign null to one of them. But I can't declare reference type as nullable by appending ? symbol because reference type is by default nullable.
//c Add a method LocalNullableVariablesUsingNullable(). Within this, I declare value type local variables (nullable) by using Nullable<T> syntax. This way is actually standard way to declare value type local variables (nullable). ? symbol for this is a shorthand for this one and it's actually converted to Nullable<T> syntax by C# compiler toward the CIL instruction in assembly.
//c Add a class DatabaseReader which contains value type (int, bool) field (nullable) and methods whose return types are value type (int, bool) (nullable).
//c Update a method Main() by using HasValue property to check local variable is assigned or not.
//c Update a method Main() by using null coalescing operator. If the value retrieved from GetIntFromDatabase() is not null, that value will be assigned to myData. And If the value retrieved from GetIntFromDatabase() is null, predefined data 100 will be assigned to myData.
//c Updata a method Main() by implementing the same functionality of null coalescing operator by using if/else statement.
//c Add a method TesterMethod() whic contains code to check if the incoming parameter is null or not. This is a traditional way for null check.
//c Update a method Main() by invoking TesterMethod() with passing null.
//c Add a method TesterMethodByUsingNullConditionalOperator() to use null conditional operator to check if the incoming parameter is null or not. If the variable is null, print whitespace.
//c Add a method TesterMethodByUsingNullConditionalAndNullCoalescingOperator() which uses null conditional operator and null coalescing operator.

namespace NullableTypes
{
    class DatabaseReader
    {
        // Nullable data field.
        public int? numericValue = null;
        public bool? boolValue = true;

        // Note the nullable return type.
        public int? GetIntFromDatabase()
        { return numericValue; }
        // Note the nullable return type.
        public bool? GetBoolFromDatabase()
        { return boolValue; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Compiler errors!
            // Value types cannot be set to null!
            //bool myBool = null;
            //int myInt = null;

            // OK! Strings are reference types.
            string myString = null;

            Console.WriteLine("***** Fun with Nullable Data *****\n");
            DatabaseReader dr = new DatabaseReader();

            // Get int from "database".
            int? i = dr.GetIntFromDatabase();
            if (i.HasValue)
                Console.WriteLine("Value of 'i' is: {0}", i.Value);
            else
                Console.WriteLine("Value of 'i' is undefined.");
            // Get bool from "database".
            bool? b = dr.GetBoolFromDatabase();
            if (b != null)
                Console.WriteLine("Value of 'b' is: {0}", b.Value);
            else
                Console.WriteLine("Value of 'b' is undefined.");

            // If the value from GetIntFromDatabase() is null,
            // assign local variable to 100.
            int myData = dr.GetIntFromDatabase() ?? 100;
            Console.WriteLine("Value of myData: {0}", myData);

            // Long-hand notation not using ?? syntax.
            int? moreData = dr.GetIntFromDatabase();
            if (!moreData.HasValue)
                moreData = 100;
            Console.WriteLine("Value of moreData: {0}", moreData);

            Console.WriteLine($"I'm passing null:\n");
            TesterMethod(null);

            Console.WriteLine($"I'm passing null:\n");
            TesterMethodByUsingNullConditionalOperator(null);

            Console.WriteLine($"I'm passing null:\n");
            TesterMethodByUsingNullConditionalAndNullCoalescingOperator(null);

            Console.ReadLine();
        }

        static void LocalNullableVariables()
        {
            // Define some local nullable variables.
            int? nullableInt = 10;
            double? nullableDouble = 3.14;
            bool? nullableBool = null;
            char? nullableChar = 'a';
            int?[] arrayOfNullableInts = new int?[10];

            // Error! Strings are reference types!
            // string? s = "oops";
        }

        static void LocalNullableVariablesUsingNullable()
        {
            // Define some local nullable types using Nullable<T>.
            Nullable<int> nullableInt = 10;
            Nullable<double> nullableDouble = 3.14;
            Nullable<bool> nullableBool = null;
            Nullable<char> nullableChar = 'a';
            Nullable<int>[] arrayOfNullableInts = new Nullable<int>[10];
        }

        static void TesterMethod(string[] args)
        {
            // We should check for null before accessing the array data!
            if (args != null)
            {
                Console.WriteLine($"You sent me {args.Length} arguments.");
            }
        }

        static void TesterMethodByUsingNullConditionalOperator(string[] args)
        {
            // We should check for null before accessing the array data!
            Console.WriteLine($"You sent me {args?.Length} arguments.");
        }

        static void TesterMethodByUsingNullConditionalAndNullCoalescingOperator(string[] args)
        {
            Console.WriteLine($"You sent me {args?.Length ?? 0} arguments.");
        }
    }
}
