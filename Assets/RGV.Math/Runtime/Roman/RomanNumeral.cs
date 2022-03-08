using System;
using System.Collections.Generic;
using System.Linq;
using RGV.Extensions.Runtime;

namespace Assets.RGV.Math.Runtime.Roman
{
    public record RomanNumeral
    {
        readonly string symbols;

        #region Constructors
        public RomanNumeral() : this("I") { }

        public RomanNumeral(string symbols)
        {
            if(!symbols.All(Symbols.ContainsKey))
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

        static bool IsAdditiveNotation(string symbols)
        {
            return symbols.LongestContiguous() >= 4;
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