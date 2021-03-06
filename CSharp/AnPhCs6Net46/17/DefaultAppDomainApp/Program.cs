﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

//c Get a default AppDomain object. Get informations of the default AppDomain.
//c Run the application. CLR loads assemblies and get the process from the OS. CLR generates the default AppDomain in the process. Get the current default AppDomain. Get all loaded assemblies in the current default AppDomain. Print informations in each assembly.
//c Update ListAllAssembliesInAppDomain() method to use LINQ API. Since you use LINQ API, this application loads additional assemblies for LINQ API. You can check out by this updated method.
//c Use AssemblyLoad event which occurs when a new assembly has been loaded into a given AppDomain. I set event handler for the event by using lambda expression. This event handler using lambda expression can point any method taking a System.Object as the first parameter and an AssemblyLoadEventArgs as the second.

namespace DefaultAppDomainApp
{
    class Program
    {
        private static void DisplayDADStats()
        {

            // Get access to the AppDomain for the current thread.
            AppDomain defaultAD = AppDomain.CurrentDomain;

            // Print out various stats about this domain.
            Console.WriteLine("Name of this domain: {0}", defaultAD.FriendlyName);
            Console.WriteLine("ID of domain in this process: {0}", defaultAD.Id);
            Console.WriteLine("Is this the default domain?: {0}",
              defaultAD.IsDefaultAppDomain());
            Console.WriteLine("Base directory of this domain: {0}", defaultAD.BaseDirectory);
        }

        static void ListAllAssembliesInAppDomain()
        {
            // Get access to the AppDomain for the current thread.
            AppDomain defaultAD = AppDomain.CurrentDomain;

            // Now get all loaded assemblies in the default AppDomain.
            var loadedAssemblies = from a in defaultAD.GetAssemblies()
                                   orderby a.GetName().Name
                                   select a;
            Console.WriteLine("***** Here are the assemblies loaded in {0} *****\n",
      defaultAD.FriendlyName);
            foreach (var a in loadedAssemblies)
            {
                Console.WriteLine("-> Name: {0}", a.GetName().Name);
                Console.WriteLine("-> Version: {0}\n", a.GetName().Version);
            }
        }

        private static void InitDAD()
        {
            // This logic will print out the name of any assembly
            // loaded into the applicaion domain, after it has been
            // created.
            AppDomain defaultAD = AppDomain.CurrentDomain;
            defaultAD.AssemblyLoad += (o, s) =>
            {
                Console.WriteLine("{0} has been loaded!", s.LoadedAssembly.GetName().Name);
            };
        }

        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with the default AppDomain *****\n");
            //DisplayDADStats();
            ListAllAssembliesInAppDomain();
            InitDAD();
            Console.ReadLine();
        }
    }
}
