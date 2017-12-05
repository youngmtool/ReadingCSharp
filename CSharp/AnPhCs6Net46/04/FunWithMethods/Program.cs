﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//c Create a console application FunWithMethods to examine parameter modifiers such none, out, ref, and param.
//c Add a Add() and this method's parameters have no parameter modifiers. So when you invoke this method with passing arguments, the value of arguments is copied and goes into this method.
//c Add an overriden Add() whose parameter modifier is out. With this feature I can change the variable which is located in the place where this method is invoked. Note that this method's return type is void.
//c Add FillTheseValues(). By using out parameter modifier, I can get the similar effect that I can get multiple return values from the single method.
//c Add ThisWontCompile(). When I use out parameter modifier, I must assign a value to a variable which will manipulate variable values located in method calling place.
//c Add SwapStrings() which swaps 2 variable values by using ref parameter modifier, located in the place that this method is invoked.
//c Update Main() to invoke SwapStrings().


namespace FunWithMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Methods *****\n");

            // Pass two variables in by value.
            int x = 9, y = 10;
            Console.WriteLine("Before call: X: {0}, Y: {1}", x, y);
            Console.WriteLine("Answer is: {0}", Add(x, y));
            Console.WriteLine("After call: X: {0}, Y: {1}", x, y);

            // No need to assign initial value to local variables
            // used as output parameters, provided the first time
            // you use them is as output arguments.
            int ans;
            Add(90, 90, out ans);
            Console.WriteLine("90 + 90 = {0}", ans);

            int i; string str; bool b;
            FillTheseValues(out i, out str, out b);

            Console.WriteLine("Int is: {0}", i);
            Console.WriteLine("String is: {0}", str);
            Console.WriteLine("Boolean is: {0}", b);

            //int var;
            //ThisWontCompile(var);

            string str1 = "Flip";
            string str2 = "Flop";
            Console.WriteLine("Before: {0}, {1} ", str1, str2);
            SwapStrings(ref str1, ref str2);
            Console.WriteLine("After: {0}, {1} ", str1, str2);

            Console.ReadLine();
        }

        // Arguments are passed by value by default.
        static int Add(int x, int y)
        {
            int ans = x + y;
            // Caller will not see these changes
            // as you are modifying a copy of the
            // original data.
            x = 10000;
            y = 88888;
            return ans;
        }


        // Output parameters must be assigned by the called method.
        static void Add(int x, int y, out int ans)
        {
            ans = x + y;
        }

        // Returning multiple output parameters.
        static void FillTheseValues(out int a, out string b, out bool c)
        {
            a = 9;
            b = "Enjoy your string.";
            c = true;
        }

        //static void ThisWontCompile(out int a)
        //{
        //    Console.WriteLine("Error! Forgot to assign output arg!");
        //}


        // Reference parameters.
        public static void SwapStrings(ref string s1, ref string s2)
        {
            string tempStr = s1;
            s1 = s2;
            s2 = tempStr;
        }
    }
}
