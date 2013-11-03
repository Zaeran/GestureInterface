﻿//Authored by Nathan Beattie

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ImgTest
{
    /// <summary>
    /// This class handles gestures
    /// </summary>
    class Gesture
    {
        //declare variables
        private List<int> _Sequence;
        private string _Function;
        private string _Name;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name to give to the gesture</param>
        /// <param name="function">The name of the method that the gesture will invoke</param>
        public Gesture(string name, string function)
        {
            _Sequence = new List<int>();
            _Name = name;
            _Function = function;
        }

        /// <summary>
        /// Sets the sequence associated with the gesture
        /// </summary>
        /// <param name="seq">a movement sequence</param>
        public void SetSequence(int[] seq)
        {
            for (int i = 0; i < seq.Length; i++)
            {
                _Sequence.Add(seq[i]);
            }
        }

        /// <summary>
        /// returns the gesture's movement sequence
        /// </summary>
        /// <returns>the movement sequence associated with the gesture</returns>
        public int[] GetSequence()
        {
            int[] seq = new int[_Sequence.Count];
            _Sequence.CopyTo(seq);
            return seq;
        }

        /// <summary>
        /// Sets the method for the gesture to invoke
        /// </summary>
        /// <param name="a">a string representing a method in Methods.cs</param>
        public void SetFunction(string a)
        {
            _Function = a;
        }

        /// <summary>
        /// returns the gesture's associated function
        /// </summary>
        /// <returns>the name of the method this gesture invokes</returns>
        public string GetFunction()
        {
            return _Function;
        }

        /// <summary>
        /// Returns the name of the gesture
        /// </summary>
        /// <returns>the name of the gesture</returns>
        public string GetName()
        {
            return _Name;
        }
    }
}