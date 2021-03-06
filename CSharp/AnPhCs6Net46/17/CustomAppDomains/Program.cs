﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//c Get a current hosting application default AppDomain object. Get assemblies which have been loaded in this AppDomain. Print informations of each assembly. // Create a new AppDomain in the current process and give it a friendly name SecondAppDomain. And invoke ListAllAssembliesInAppDomain() method with passing this AppDomain object.
//c Create a new AppDomain in the current process, assigning to the object. Load a custom library, CarLibrary.dll, into the newly created AppDomain.
//c Create a new AppDomain. Load a assembly into the new AppDomain. Unload the newly create AppDomain from the process. Naturally, all assemblies loaded in the AppDomain are also torn down along with parent AppDomain.

namespace CustomAppDomains
{
    class Program
    {

        private static void MakeNewAppDomain()
        {
            // Make a new AppDomain in the current process.
            AppDomain newAD = AppDomain.CreateDomain("SecondAppDomain");
            newAD.DomainUnload += (o, s) =>
            {
                Console.WriteLine("The second AppDomain has been unloaded!");
            };

            try
            {
                // Now load CarLibrary.dll into this new domain.
                newAD.Load("CarLibrary");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

            // List all assemblies.
            ListAllAssembliesInAppDomain(newAD);

            // Now tear down this AppDomain.
            AppDomain.Unload(newAD);


        }

        static void ListAllAssembliesInAppDomain(AppDomain ad)
        {
            // Now get all loaded assemblies in the default AppDomain.
            var loadedAssemblies = from a in ad.GetAssemblies()
                                   orderby a.GetName().Name
                                   select a;
            Console.WriteLine("***** Here are the assemblies loaded in {0} *****\n", ad.FriendlyName);
            foreach (var a in loadedAssemblies)
            {
                Console.WriteLine("-> Name: {0}", a.GetName().Name);
                Console.WriteLine("-> Version: {0}\n", a.GetName().Version);
            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Custom AppDomains *****\n");

            // Show all loaded assemblies in default AppDomain.
            AppDomain defaultAD = AppDomain.CurrentDomain;
            defaultAD.ProcessExit += (o, s) =>
            {
                Console.WriteLine("Default AD unloaded!");
            };

            ListAllAssembliesInAppDomain(defaultAD);

            MakeNewAppDomain();



            Console.ReadLine();
        }
    }
}
