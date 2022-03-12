using System;
using System.Collections.Generic;
using System.Linq;
using RGV.Extensions.Runtime;

namespace RGV.Math.Runtime.Roman
{
    public record RomanNumeral
    {
        readonly string chars;

        IList<RomanSymbol> Symbols => chars.Select(c => new RomanSymbol(c)).ToList();

        #region Constructors
        public RomanNumeral() : this("I") { }

        public RomanNumeral(string symbols)
        {
            if(!symbols.All(RomanSymbol.IsSymbol))
                throw new ArgumentOutOfRangeException(nameof(symbols));
            if(IsAdditiveNotation(symbols))
                throw new NotSupportedException("Additive notation is not supported");

            chars = symbols;
        }

        public RomanNumeral(int number)
        {
            if(number <= 0)
                throw new ArgumentOutOfRangeException(nameof(number));

            chars = FromNumber(number).chars;
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
            return chars;
        }
        
        int ToNumber()
        {
            if(chars.Length == 1)
                return Symbols.Single();

            int total = Symbols.Last();
            for(var i = chars.Length - 2; i >= 0; i--)
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
            var result = "";

            if(NeedsSustractiveSymbols(number))
                return FromSubstractiveNumber(number);
            
            while(number > 0)
            {
                var symbol = RomanSymbol.ClosestLowerOrEqualTo(number);
                result += symbol;
                number -= symbol;
            }

            return new RomanNumeral(result);
        }

        static bool NeedsSustractiveSymbols(int number)
        {
            var result = "";
            while(number > 0)
            {
                var symbol = RomanSymbol.ClosestLowerOrEqualTo(number);
                result += symbol;
                number -= symbol;
            }

            return IsAdditiveNotation(result);
        }

        static RomanNumeral FromSubstractiveNumber(int number)
        {
            return new RomanNumeral("I" + RomanSymbol.ClosestHigherTo(number));
        }
        #endregion

        static bool IsAdditiveNotation(string symbols)
        {
            return symbols.LongestContiguous() >= 4;
        }

        #region Nested
        record RomanSymbol : IComparable<RomanSymbol>
        {
            #region Static members
            static readonly Dictionary<char, int> Values = new Dictionary<char, int>
            {
                ['I'] = 1,
                ['V'] = 5,
                ['X'] = 10,
                ['L'] = 50,
                ['C'] = 100,
                ['D'] = 500,
                ['M'] = 1000
            };
            #endregion
            
            readonly char symbol;

            public RomanSymbol(char source)
            {
                if(!IsSymbol(source))
                    throw new ArgumentOutOfRangeException();
                
                symbol = source;
            }

            public static bool IsSymbol(char c)
            {
                return Values.ContainsKey(c);
            }

            public static RomanSymbol ClosestHigherTo(int number)
            {
                return new RomanSymbol(Values.First(rs => rs.Value > number).Key);
            }
            public static RomanSymbol ClosestLowerOrEqualTo(int number)
            {
                return new RomanSymbol(Values.Last(rs => rs.Value <= number).Key);
            }

            public static implicit operator int(RomanSymbol r)
            {
                return r.ToInt();
            }

            int ToInt()
            {
                return Values[symbol];
            }

            public int CompareTo(RomanSymbol other)
            {
                return ToInt().CompareTo(other.ToInt());
            }

            public override string ToString()
            {
                return symbol.ToString();
            }
        }
        #endregion
    }
}