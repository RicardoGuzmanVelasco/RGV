using System;
using System.Collections.Generic;
using System.Linq;
using RGV.Extensions.Runtime;

namespace RGV.Math.Runtime.Roman
{
    public record RomanNumeral
    {
        readonly string symbols;

        #region Constructors
        public RomanNumeral() : this("I") { }

        public RomanNumeral(string symbols)
        {
            if(symbols.Contains('j'))
                throw new ArgumentOutOfRangeException(nameof(symbols));
            if(IsAdditiveNotation(symbols))
                throw new NotSupportedException("Additive notation is not supported");

            this.symbols = symbols;
        }

        public RomanNumeral(int number)
        {
            if(number <= 0)
                throw new ArgumentOutOfRangeException(nameof(number));
        }
        #endregion

        public static implicit operator RomanNumeral(string symbols)
        {
            return new RomanNumeral(symbols);
        }

        public static implicit operator int(RomanNumeral source)
        {
            return source.ToInt();
        }

        public override string ToString()
        {
            return symbols;
        }

        static bool IsAdditiveNotation(string symbols)
        {
            return symbols.LongestContiguous() >= 4;
        }

        int ToInt()
        {
            var sum = symbols.
                Where(s => Symbols.ContainsKey(s))
                .Sum(s => Symbols[s]);
            if(sum > 0)
                return sum; 

            return symbols switch
            {
                "IV" => 4,
                "IX" => 9,
                _ => 0
            };
        }
        
        static readonly Dictionary<char, int> Symbols = new Dictionary<char, int>
        {
            ['I'] = 1,
            ['V'] = 5,
            ['X'] = 10,
            ['L'] = 50,
            ['C'] = 100,
            ['D'] = 500,
            ['M'] = 1000,
        };
    }
}