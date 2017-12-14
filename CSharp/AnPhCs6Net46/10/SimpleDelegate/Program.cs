﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//c Create a console application SimpleDelegate. It contains BinaryOp(delegate) type being able to point to any method taking 2 parameters in Int32(struct) data type, returning Int32 data type value. It also contains SimpleMath(class) type containing two method matched to what BinaryOp(delegate) type points to.
//c Add a method SquareNumber(). I make BinaryOp(delegate) type instance point to SquareNumber(). But it fails with showing compile time error because the method signature BinaryOp(delegate) type object pointing to is not compatable to the method signature of SquareNumber().

namespace SimpleDelegate
{
    // This delegate can point to any method,
    // taking two integers and returning an integer.
    public delegate int BinaryOp(int x, int y);

    // This class contains methods BinaryOp will
    // point to.
    public class SimpleMath
    {
        public static int Add(int x, int y)
        { return x + y; }
        public static int Subtract(int x, int y)
        { return x - y; }
        public static int SquareNumber(int a)
        { return a * a; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Simple Delegate Example *****\n");

            // Create a BinaryOp delegate object that
            // "points to" SimpleMath.Add().
            BinaryOp b = new BinaryOp(SimpleMath.Add);
            // Invoke Add() method indirectly using delegate object.
            Console.WriteLine("10 + 10 is {0}", b(10, 10));

            // Compiler error! Method does not match delegate pattern!
            //BinaryOp b2 = new BinaryOp(SimpleMath.SquareNumber);

            Console.ReadLine();
        }
    }
}