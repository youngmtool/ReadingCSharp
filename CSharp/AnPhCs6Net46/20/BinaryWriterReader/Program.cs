﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//c Create a project BinaryWriterReader. Set a file named BinFile.dat in the destination place. I instantiate BinaryWriter object with passing FileStream object retrieved by invoking FileInfo.OpenWrite() method. And I write various types of data into the stream by using BinaryWriter.Write() method.
//c Read a BinFile.dat file by using BinaryReader object instantiated by being passed into FileInfo object retrieved from FileInfo.OpenRead() method. And with this BinaryReader object, I call each Read() method such as for double type, int type, or string type.

namespace BinaryWriterReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Binary Writers / Readers *****\n");

            // Open a binary writer for a file.
            FileInfo f = new FileInfo("BinFile.dat");
            using (BinaryWriter bw = new BinaryWriter(f.OpenWrite()))
            {
                // Print out the type of BaseStream.
                // (System.IO.FileStream in this case).
                Console.WriteLine("Base stream is: {0}", bw.BaseStream);

                // Create some data to save in the file.
                double aDouble = 1234.67;
                int anInt = 34567;
                string aString = "A, B, C";

                // Write the data.
                bw.Write(aDouble);
                bw.Write(anInt);
                bw.Write(aString);
            }
            Console.WriteLine("Done!");



            // Read the binary data from the stream.
            using (BinaryReader br = new BinaryReader(f.OpenRead()))
            {
                Console.WriteLine(br.ReadDouble());
                Console.WriteLine(br.ReadInt32());
                Console.WriteLine(br.ReadString());
            }
            Console.ReadLine();
        }
    }
}
