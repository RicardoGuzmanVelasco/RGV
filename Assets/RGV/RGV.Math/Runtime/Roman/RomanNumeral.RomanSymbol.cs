using System;
using System.Collections.Generic;
using System.Linq;

namespace RGV.Math.Runtime.Roman
{
    public sealed partial class RomanNumeral
    {
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

            #region Constructors
            public RomanSymbol(char source)
            {
                if(!IsSymbol(source))
                    throw new ArgumentOutOfRangeException();

                symbol = source;
            }
            #endregion

            #region IComparable implementation
            public int CompareTo(RomanSymbol other)
            {
                return ToInt().CompareTo(other.ToInt());
            }
            #endregion

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

            #region Conversions
            public static implicit operator int(RomanSymbol r)
            {
                return r.ToInt();
            }

            int ToInt()
            {
                return Values[symbol];
            }

            public override string ToString()
            {
                return symbol.ToString();
            }
            #endregion
        }
    }
}