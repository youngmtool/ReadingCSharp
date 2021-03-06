﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//c Add a class ThreeDCircle with pretending I get this class from third-party.
//c Inherit Circle class from ThreeDCircle.
//c Add a new keyword to explicitly state Draw() in ThreeDCircle hides Draw() in base class Circle, Shape.
//c Add a new keyword to string data type field PetName which hides PetName in base class..
//c Update a class Hexagon by implementing an IDraw3D(interface) type and also implementing Draw3D() from IDraw3D.

namespace CustomInterface
{
    // Hexagon now implements IPointy.
    class Fork : IPointy
    {
        public Fork() { }
        public Fork(string name) { }
        

        // IPointy implementation.
        public byte Points
        {
            get { return 33; }
        }
    }
}
