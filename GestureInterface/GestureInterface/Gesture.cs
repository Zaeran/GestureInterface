//Authored by Nathan Beattie

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ImgTest
{
    class Gesture
    {
        private List<int> _Sequence;
        private string _Function;
        private string _Name;

        public Gesture(string name, string function)
        {
            _Sequence = new List<int>();
            _Name = name;
            _Function = function;
        }

        public void SetSequence(int[] seq)
        {
            for (int i = 0; i < seq.Length; i++)
            {
                _Sequence.Add(seq[i]);
            }
        }

        public int[] GetSequence()
        {
            int[] seq = new int[_Sequence.Count];
            _Sequence.CopyTo(seq);
            return seq;
        }

        public void SetFunction(string a)
        {
            _Function = a;
        }

        public string GetFunction()
        {
            return _Function;
        }

        public string GetName()
        {
            return _Name;
        }
    }
}