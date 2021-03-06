﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//c Create a console application ProcessMultipleExceptions.
//c Add a method Accelerate(). I add new if conditional statement to check if delta is less than 0. If so, I throw an exception with predefined ArgumentOutOfRangeException(class) type object.
//c Update a method Main(). I pass -10 to Accelerate(). -10 means delta is less than 0 so it's supposed to trigger an exception with ArgumentOutOfRangeException(class) type object. And this exception is caught by catch clause which is using ArgumentOutOfRangeException(class) type as a parameter e.
//c Add a method Main(). I put code which is supposed to trigger an exception with ArgumentOutOfRangeException(class) type object. But this kind of exception also can be caught by an exception with System.Exception(class) type object so that below 2 catch codes become unreachable and it means this gets a compile time error.
//c Update a method Main(). This time, I put specific catch clauses which contain specific kind of exception (as opposed to general kind of exception like System.Exception) in the top positions. So triggered exception by -10 will be caught catch clause containing exception with ArgumentOutOfRangeException(class) type object.
//c Update a method Main(). I use throw keyword inside of CarIsDeadException catch block to rethrow exception with CarIsDeadException(class) type object to the Main() and Main() throws this exception to the CLR and CLR manages this exception by showing a system-supplied error message box.
//c Update a method Main(). I use inner exception technique.
//c Update a method Main(). I use finally block which is always executed after catch block.
//c Update a class CarIsDeadException. I add a custom constructor which takes 3 parameters and 2 passed arguments are stored to properties' backing fields and 1 passed argument (message) is passed to the custom constructor of base class (ApplicationException(class) type).
//c Update a method Main(). I use an exception filter by using when keyword with making an optional when clause. Only if when clause returns true catch block logic is executed.

namespace ProcessMultipleExceptions
{
    class Radio
    {
        public void TurnOn(bool on)
        {
            if (on)
                Console.WriteLine("Jamming...");
            else
                Console.WriteLine("Quiet time...");
        }
    }

    class Car
    {
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
            if (delta < 0)
                throw new
                  ArgumentOutOfRangeException("delta", "Speed must be greater than zero!");

            if (carIsDead)
                Console.WriteLine("{0} is out of order...", PetName);
            else
            {
                CurrentSpeed += delta;
                if (CurrentSpeed >= MaxSpeed)
                {
                    carIsDead = true;
                    CurrentSpeed = 0;

                    CarIsDeadException ex = new CarIsDeadException(
                        string.Format("{0} has overheated!", PetName),
                         "You have a lead foot",
                         DateTime.Now);

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
    }

    [Serializable]
    public class CarIsDeadException : ApplicationException
    {
        // Custom members for our exception.
        public DateTime ErrorTimeStamp { get; set; }
        public string CauseOfError { get; set; }

        public CarIsDeadException() { }
        public CarIsDeadException(string message) : base(message) { }
        public CarIsDeadException(string message, string cause, DateTime time) : base(message)
        {
            CauseOfError = cause;
            ErrorTimeStamp = time;
        }
        public CarIsDeadException(string message,
                                  System.Exception inner)
          : base(message, inner) { }
        protected CarIsDeadException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
          : base(info, context) { }
        // Any additional custom properties, constructors and data members...
    }

    class Program
    {
        // This code compiles just fine.
        static void Main(string[] args)
        {
            Console.WriteLine("***** Handling Multiple Exceptions *****\n");
            Car myCar = new Car("Rusty", 90);
            try
            {
                // Trigger an argument out of range exception.
                myCar.Accelerate(-10);
                FileStream fs = File.Open(@"C:\carErrors.txt", FileMode.Open);
            }

            catch (CarIsDeadException e) when (e.ErrorTimeStamp.DayOfWeek != DayOfWeek.Friday)
            {
                // This new line will only print if the when clause evaluates to true.
                Console.WriteLine("Catching car is dead!");

                Console.WriteLine(e.Message);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
            // This will catch any other exception
            // beyond CarIsDeadException or
            // ArgumentOutOfRangeException.
            catch (Exception e2)
            {
                // Throw an exception that records the new exception,
                // as well as the message of the first exception.
                throw new CarIsDeadException(e.Message, e2);
            }
            finally
            {
                // This will always occur. Exception or not.
                myCar.CrankTunes(false);
            }
            Console.ReadLine();
        }
    }
}
