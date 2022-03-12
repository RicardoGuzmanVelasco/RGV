using System;
using System.Collections.Generic;
using System.Linq;
using RGV.Extensions.Runtime;

namespace RGV.Math.Runtime.Roman
{
    public sealed partial class RomanNumeral
    {
        IList<RomanSymbol> Symbols { get; }

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

            Symbols = FromNumber(number).Symbols;
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
                ? FromSubstractiveNumber(number)
                : new RomanNumeral(CharsFromNumber(number));
        }

        static bool NeedsSustractiveSymbols(int number)
        {
            return IsAdditiveNotation(CharsFromNumber(number));
        }

        static string CharsFromNumber(int number)
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

        static RomanNumeral FromSubstractiveNumber(int number)
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
            if(Symbols.Count != other.Symbols.Count)
                return false;

            for(int i = 0; i < Symbols.Count; i++)
            {
                if(!Symbols[i].Equals(other.Symbols[i]))
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return Symbols != null ? Symbols.GetHashCode() : 0;
        }
        #endregion
        
        static bool IsAdditiveNotation(string symbols)
        {
            return symbols.LongestContiguous() >= 4;
        }
    }
}