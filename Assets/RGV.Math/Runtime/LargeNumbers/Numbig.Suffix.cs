using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using static RGV.DesignByContract.Runtime.Precondition;

namespace RGV.Math.Runtime.LargeNumbers
{
    public readonly partial struct Numbig
    {
        record Suffix
        {
            static readonly IList<string> SingleSuffixes = new[]
            {
                "", "K", "M", "B", "T"
            };

            readonly string value;

            public Suffix([NotNull] string suffix)
            {
                Require(IsSuffix(suffix)).True();

                value = suffix.ToUpper();
            }

            public Suffix Next() => new Suffix(After(this));

            public static bool IsSuffix(string suffix)
            {
                return IsSingle(suffix) ||
                       (suffix.Length > 1 && suffix.All(char.IsLetter));
            }

            public override string ToString() => value;

            public float FactorTo(Suffix other)
            {
                float factor;
                for(factor = 1; this != other; factor *= 1000)
                    other = other.Next();

                return factor;
            }

            #region Support methods
            static string After(Suffix suffix)
            {
                return suffix.NextIsSingle()
                    ? SingleAfter(suffix)
                    : CompoundAfter(suffix.value);
            }

            static string SingleAfter(Suffix suffix)
            {
                Require(suffix.NextIsSingle()).True();

                var nextSuffixIndex = SingleSuffixes.IndexOf(suffix.value ?? "") + 1;
                return SingleSuffixes[nextSuffixIndex];
            }

            static bool IsSingle(string suffix)
            {
                return SingleSuffixes.Contains
                (
                    suffix,
                    StringComparer.InvariantCultureIgnoreCase
                );
            }

            static string CompoundAfter(string suffix)
            {
                if(suffix == SingleSuffixes.Last())
                    return new Suffix("aa").value;

                if(suffix.All(c => c.ToString().Equals("z", StringComparison.InvariantCultureIgnoreCase)))
                    return string.Concat(Enumerable.Repeat("a", suffix.Length + 1));

                var suffixChars = suffix.ToCharArray();
                suffixChars[^1]++;
                return new string(suffixChars);
            }

            bool NextIsSingle()
            {
                return IsSingle(value) &&
                       value != SingleSuffixes.Last();
            }
            #endregion
        }
    }
}