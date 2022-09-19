//----------------------------------------
// 	     Abony Int. - aMinAttribute
// Copyright © 2020 'OOO EBONI INTERAKTIV'
//---------------------------------------

using UnityEngine;
using System;

namespace AbonyInt.SmartVariables
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class aMinAttribute : PropertyAttribute
    {
        public readonly double min;

        public aMinAttribute(byte min)
        {
            this.min = min;
        }

        public aMinAttribute(sbyte min)
        {
            this.min = min;
        }

        public aMinAttribute(int min)
        {
            this.min = min;
        }
        
        public aMinAttribute(uint min)
        {
            this.min = min;
        }

        public aMinAttribute(long min)
        {
            this.min = min;
        }

        public aMinAttribute(ulong min)
        {
            this.min = min;
        }

        public aMinAttribute(float min)
        {
            this.min = min;
        }

        public aMinAttribute(double min)
        {
            this.min = min;
        }
    }
}