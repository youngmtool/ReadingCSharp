﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//c I make Car type derive from IComparable and implement CompareTo() from IComparable.
//c Update a class Car. I refactor CompareTo() towards simplification by using CompareTo() with Int32(structure) data type values inside of it method scope.
//c Add a read-only property (get property) SortByPetName which returns PetNameComparer(class) type instance after explicit type casting it to IComparable(interface) type.

namespace ComparableCar
{
    // The iteration of the Car can be ordered
    // based on the CarID.
    public class Car : IComparable
    {

        public int CarID { get; set; }
        public Car(string name, int currSp, int id)
        {
            CurrentSpeed = currSp;
            PetName = name;
            CarID = id;
        }

        // Property to return the PetNameComparer.
        public static IComparer SortByPetName
        {
            get
            {
                return (IComparer)new PetNameComparer();
            }
        }

        // Constant for maximum speed.
        public const int MaxSpeed = 100;

        // Car properties.
        public int CurrentSpeed { get; set; } = 0;
        public string PetName { get; set; } = "";

        // Is the car still operational?
        private bool carIsDead;

        // A car has-a radio.
        private Radio theMusicBox = new Radio();

        // Constructors.
        public Car() { }
        public Car(string name, int speed)
        {
            CurrentSpeed = speed;
            PetName = name;
        }
        public void CrankTunes(bool state)
        {
            // Delegate request to inner object.
            theMusicBox.TurnOn(state);
        }

        public void Accelerate(int delta)
        {
            if (carIsDead)
                Console.WriteLine("{0} is out of order...", PetName);
            else
            {
                CurrentSpeed += delta;
                if (CurrentSpeed >= MaxSpeed)
                {
                    carIsDead = true;
                    CurrentSpeed = 0;
                    // We need to call the HelpLink property, thus we need to
                    // create a local variable before throwing the Exception object.
                    Exception ex =
                      new Exception(string.Format("{0} has overheated!", PetName));
                    ex.HelpLink = "http://www.CarsRUs.com";
                    // Stuff in custom data regarding the error.
                    ex.Data.Add("TimeStamp", string.Format("The car exploded at {0}", DateTime.Now));
                    ex.Data.Add("Cause", "You have a lead foot.");
                    throw ex;
                }
                else
                    Console.WriteLine("=> CurrentSpeed = {0}", CurrentSpeed);
            }
        }

        // IComparable implementation.
        //int IComparable.CompareTo(object obj)
        //{
        //    Car temp = obj as Car;
        //    if (temp != null)
        //    {
        //        if (this.CarID > temp.CarID)
        //            return 1;
        //        if (this.CarID < temp.CarID)
        //            return -1;
        //        else
        //            return 0;
        //    }
        //    else
        //        throw new ArgumentException("Parameter is not a Car!");
        //}

        int IComparable.CompareTo(object obj)
        {
            Car temp = obj as Car;
            if (temp != null)
                return this.CarID.CompareTo(temp.CarID);
            else
                throw new ArgumentException("Parameter is not a Car!");
        }
    }
}
