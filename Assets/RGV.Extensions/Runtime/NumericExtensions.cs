using System.Collections.Generic;
using System.Linq;

namespace RGV.Extensions.Runtime
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
    }
}