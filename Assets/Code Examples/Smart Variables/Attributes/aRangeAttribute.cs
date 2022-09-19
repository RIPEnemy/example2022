//----------------------------------------
// 	    Abony Int. - aRangeAttribute
// Copyright © 2020 'OOO EBONI INTERAKTIV'
//---------------------------------------

using UnityEngine;
using System;

namespace AbonyInt.SmartVariables
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class aRangeAttribute : PropertyAttribute
    {
        public readonly double min;
        public readonly double max;

        public aRangeAttribute(byte min, byte max)
        {
            this.min = min;
            this.max = max;
        }

        public aRangeAttribute(sbyte min, sbyte max)
        {
            this.min = min;
            this.max = max;
        }

        public aRangeAttribute(int min, int max)
        {
            this.min = min;
            this.max = max;
        }
        
        public aRangeAttribute(uint min, uint max)
        {
            this.min = min;
            this.max = max;
        }

        public aRangeAttribute(long min, long max)
        {
            this.min = min;
            this.max = max;
        }

        public aRangeAttribute(ulong min, ulong max)
        {
            this.min = min;
            this.max = max;
        }

        public aRangeAttribute(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

        public aRangeAttribute(double min, double max)
        {
            this.min = min;
            this.max = max;
        }
    }
}