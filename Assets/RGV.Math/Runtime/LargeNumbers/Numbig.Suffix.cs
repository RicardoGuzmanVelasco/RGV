using System;
using System.Collections.Generic;
using System.Linq;

namespace RGV.Math.Runtime.LargeNumbers
{
    public readonly partial struct Numbig
    {
        static readonly IList<string> SingleSuffixes = new[]
        {
            "", "K", "M", "B", "T"
        };

        public static string ToSuffix(string suffix)
        {
            return suffix?.ToUpper();
        }

        static string SuffixAfter(string suffix)
        {
            suffix ??= "";
            return !NextSuffixIsSingle(suffix)
                ? CompoundSuffixAfter(suffix)
                : SingleSuffixAfter(suffix);
        }

        static string SingleSuffixAfter(string suffix)
        {
            var nextSuffixIndex = SingleSuffixes.IndexOf(suffix ?? "") + 1;
            return SingleSuffixes[nextSuffixIndex];
        }

        static bool NextSuffixIsSingle(string suffix)
        {
            return IsSingle(suffix) &&
                   SingleSuffixes.IndexOf(suffix) < SingleSuffixes.Count - 1;
        }

        static bool IsSingle(string suffix)
        {
            return SingleSuffixes.Contains(suffix, StringComparer.InvariantCultureIgnoreCase);
        }

        static string CompoundSuffixAfter(string suffix)
        {
            if(suffix == SingleSuffixes.Last())
                return ToSuffix("aa");

            if(suffix.All(c => c.ToString().Equals("z", StringComparison.InvariantCultureIgnoreCase)))
                return string.Concat(Enumerable.Repeat("a", suffix.Length + 1));

            var suffixChars = suffix.ToCharArray();
            suffixChars[^1]++;
            return new string(suffixChars);
        }
    }
}