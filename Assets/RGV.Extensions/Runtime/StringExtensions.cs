using System.Collections.Generic;
using System.Linq;

namespace RGV.Extensions.Runtime
{
    public static class StringExtensions
    {
        public static int LongestContiguous(this string source)
        {
            if(string.IsNullOrEmpty(source))
                return 0;

            var combos = new List<string>();
            while(source.Length > 0)
            {
                var combo = NextCombo(source);
                combos.Add(combo);
                source = source[combo.Length..];
            }

            return combos.Max(s => s.Length);

            string NextCombo(string s)
            {
                var combo = s.First().ToString();
                for(var i = 1; i < s.Length; i++)
                    if(s[i] == combo.Last())
                        combo += s[i];
                    else
                        break;

                return combo;
            }
        }
    }
}