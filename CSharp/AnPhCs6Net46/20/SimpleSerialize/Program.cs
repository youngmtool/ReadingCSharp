﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;

//c Create a project SimpleSerialize. And add a Radio class which is marked with [Serializable] and one field, radioID, in the Radio class is marked with [NonSerialized].
//c Add a Car class which is decorated by [Serializable].
//c Add a JamesBondCar class which is decorated by [Serializable] and derived from Car base class.
//c Add a Person class to examine the exception for XML format serialization. When I serialize object graph, this ignores members which have a private access. When I need to include private member to serialization, I should use the public property which sets private fields.
//c Manipulate JamesBondCar instance and serialize object graph and store it into a file named CarData.dat by using FileStream.
//c Serialize the object graph into a SOAP format file by using SoapFormatter object and FileStream object.

namespace SimpleSerialize
{
    [Serializable]
    public class Radio
    {
        public bool hasTweeters;
        public bool hasSubWoofers;
        public double[] stationPresets;

        [NonSerialized]
        public string radioID = "XF-552RR6";
    }


    [Serializable]
    public class Car
    {
        public Radio theRadio = new Radio();
        public bool isHatchBack;
    }



    [Serializable]
    public class JamesBondCar : Car
    {
        public bool canFly;
        public bool canSubmerge;
    }


    [Serializable]
    public class Person
    {
        // A public field.
        public bool isAlive = true;

        // A private field.
        private int personAge = 21;

        // Public property/private data.
        private string fName = string.Empty;
        public string FirstName
        {
            get { return fName; }
            set { fName = value; }
        }
    }

    class Program
    {
        // Be sure to import the System.Runtime.Serialization.Formatters.Binary
        // and System.IO namespaces.
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Object Serialization *****\n");

            // Make a JamesBondCar and set state.
            JamesBondCar jbc = new JamesBondCar();
            jbc.canFly = true;
            jbc.canSubmerge = false;
            jbc.theRadio.stationPresets = new double[] { 89.3, 105.1, 97.1 };
            jbc.theRadio.hasTweeters = true;

            // Now save the car to a specific file in a binary format.
            SaveAsBinaryFormat(jbc, "CarData.dat");
            Console.ReadLine();
        }

        static void SaveAsBinaryFormat(object objGraph, string fileName)
        {
            // Save object to a file named CarData.dat in binary.
            BinaryFormatter binFormat = new BinaryFormatter();

            using (Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, objGraph);
            }
            Console.WriteLine("=> Saved car in binary format!");
        }

        static void LoadFromBinaryFile(string fileName)
        {
            BinaryFormatter binFormat = new BinaryFormatter();

            // Read the JamesBondCar from the binary file.
            using (Stream fStream = File.OpenRead(fileName))
            {
                JamesBondCar carFromDisk =
                  (JamesBondCar)binFormat.Deserialize(fStream);
                Console.WriteLine("Can this car fly? : {0}", carFromDisk.canFly);
            }
        }

        // Be sure to import System.Runtime.Serialization.Formatters.Soap
        // and reference System.Runtime.Serialization.Formatters.Soap.dll.
        static void SaveAsSoapFormat(object objGraph, string fileName)
        {
            // Save object to a file named CarData.soap in SOAP format.
            SoapFormatter soapFormat = new SoapFormatter();

            using (Stream fStream = new FileStream(fileName,
              FileMode.Create, FileAccess.Write, FileShare.None))
            {
                soapFormat.Serialize(fStream, objGraph);
            }
            Console.WriteLine("=> Saved car in SOAP format!");
        }
    }


}
