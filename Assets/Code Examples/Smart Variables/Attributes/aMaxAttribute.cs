//----------------------------------------
// 	     Abony Int. - aMaxAttribute
// Copyright © 2020 'OOO EBONI INTERAKTIV'
//---------------------------------------

using UnityEngine;
using System;

namespace AbonyInt.SmartVariables
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class aMaxAttribute : PropertyAttribute
    {
        public readonly double max;

        public aMaxAttribute(byte max)
        {
            this.max = max;
        }

        public aMaxAttribute(sbyte max)
        {
            this.max = max;
        }

        public aMaxAttribute(int max)
        {
            this.max = max;
        }
        
        public aMaxAttribute(uint max)
        {
            this.max = max;
        }

        public aMaxAttribute(long max)
        {
            this.max = max;
        }

        public aMaxAttribute(ulong max)
        {
            this.max = max;
        }

        public aMaxAttribute(float max)
        {
            this.max = max;
        }

        public aMaxAttribute(double max)
        {
            this.max = max;
        }
    }
}