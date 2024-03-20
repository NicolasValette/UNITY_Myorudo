using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Datas
{
    public class Bid
    {
        private int _value;
        private int _face;

        public int Value { get=> _value; }
        public int Face { get=> _face; }
        public Bid(int value, int face)
        {
            _value = value;
            _face = face;
        }
        public override string ToString()
        {
            return $"{_value} - {_face}";
        }
        
    }
}