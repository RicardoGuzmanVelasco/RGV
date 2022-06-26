using System.Collections.Generic;

namespace RGV.Math.Runtime.LargeNumbers
{
    public readonly partial struct Numbig
    {
        static readonly IList<string> SingleSuffixes = new[]
        {
            "",
            "k",
            "M",
            "B",
            "T"
        };

        static string SuffixAfter(string suffix)
        {
            var nextSuffixIndex = SingleSuffixes.IndexOf(suffix ?? "") + 1;
            return SingleSuffixes[nextSuffixIndex];
        }
    }
}