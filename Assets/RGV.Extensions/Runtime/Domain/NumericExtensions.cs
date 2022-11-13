using System.Collections.Generic;
using System.Linq;

namespace Assets.RGV.Extensions.Runtime.Domain
{
    public static class NumericExtensions
    {
        public static IEnumerable<int> FactorizeBase10(this int number)
        {
            var factors = new List<int>();

            for(var i = 0; i < number.ToString().Length; i++)
            {
                var factor = int.Parse(number.ToString().Reverse().ToList()[i].ToString()) *
                             (int)System.Math.Pow(10, i);
                factors.Add(factor);
            }

            factors.Remove(0);
            factors.Reverse();

            return factors;
        }

        public static int Sign(this int number)
        {
            return Sign((float)number);
        }

        public static int Sign(this bool isPositive)
        {
            return Sign(isPositive ? 1 : -1);
        }

        public static int Sign(this float number)
        {
            return number < 0 ? -1 : 1;
        }
    }
}