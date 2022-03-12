using System;
using System.Collections.Generic;
using System.Linq;
using RGV.Extensions.Runtime;

namespace RGV.Math.Runtime.Roman
{
    public sealed partial class RomanNumeral
    {
        List<RomanSymbol> Symbols { get; }

        #region Constructors
        public RomanNumeral() : this("I") { }

        public RomanNumeral(string symbols)
        {
            if(!symbols.All(RomanSymbol.IsSymbol))
                throw new ArgumentOutOfRangeException(nameof(symbols));
            if(IsAdditiveNotation(symbols))
                throw new NotSupportedException("Additive notation is not supported");

            Symbols = symbols.Select(c => new RomanSymbol(c)).ToList();
        }

        public RomanNumeral(int number)
        {
            if(number <= 0)
                throw new ArgumentOutOfRangeException(nameof(number));

            var factors = new List<RomanNumeral>();
            for(var i = 0; i < number.ToString().Length; i++)
            {
                var factor = int.Parse(number.ToString().Reverse().ToList()[i].ToString()) * (int)System.Math.Pow(10, i);
                factors.Add(FromNumber(factor));
            }

            factors.Reverse();

            Symbols = new List<RomanSymbol>();
            foreach(var factor in factors)
                Symbols.AddRange(factor.Symbols);
        }
        #endregion

        #region Conversions
        public static implicit operator RomanNumeral(string symbols)
        {
            return new RomanNumeral(symbols);
        }

        public static implicit operator int(RomanNumeral source)
        {
            return source.ToNumber();
        }

        public override string ToString()
        {
            return Symbols.Aggregate(string.Empty, (s, symbol) => s + symbol);
        }

        int ToNumber()
        {
            if(Symbols.Count == 1)
                return Symbols.Single();

            int total = Symbols.Last();
            for(var i = Symbols.Count - 2; i >= 0; i--)
            {
                int addend = Symbols[i];
                if(NextSymbolIsLower(i))
                    addend *= -1;
                total += addend;
            }

            return total;

            bool NextSymbolIsLower(int index)
            {
                return Symbols[index + 1] > Symbols[index];
            }
        }

        static RomanNumeral FromNumber(int number)
        {
            return NeedsSustractiveSymbols(number)
                ? FromSubtractiveNumber(number)
                : new RomanNumeral(FactorizeCharsFromNumber(number));
        }

        static bool NeedsSustractiveSymbols(int number)
        {
            return IsAdditiveNotation(FactorizeCharsFromNumber(number));
        }

        static string FactorizeCharsFromNumber(int number)
        {
            var result = "";

            while(number > 0)
            {
                var symbol = RomanSymbol.ClosestLowerOrEqualTo(number);
                result += symbol;
                number -= symbol;
            }

            return result;
        }

        static RomanNumeral FromSubtractiveNumber(int number)
        {
            var subtrahend = RomanSymbol.ClosestHigherTo(number);
            var minuend = new RomanNumeral(subtrahend - number);

            var symbols = minuend + subtrahend.ToString();
            return new RomanNumeral(symbols);
        }
        #endregion

        #region Equality
        public override bool Equals(object obj)
        {
            return obj is RomanNumeral other && Equals(other);
        }

        bool Equals(RomanNumeral other)
        {
            return Symbols.SequenceEqual(other.Symbols);
        }

        public override int GetHashCode()
        {
            return Symbols.GetHashCode();
        }
        #endregion
        
        static bool IsAdditiveNotation(string symbols)
        {
            return symbols.LongestContiguous() >= 4;
        }
    }
}