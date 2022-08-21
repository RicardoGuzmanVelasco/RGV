using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using static RGV.DesignByContract.Runtime.Contract;

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

            internal readonly string value;

            public Suffix([NotNull] string suffix)
            {
                Require(IsSuffix(suffix)).True();

                value = suffix.ToUpper();
            }

            public Suffix Next() => new Suffix(After(this));
            public Suffix Prev() => new Suffix(Before(this));

            public static bool IsSuffix(string suffix)
            {
                return IsSingle(suffix) ||
                       (suffix.Length > 1 && suffix.All(char.IsLetter));
            }

            public override string ToString() => value;

            internal float FactorTo(Suffix other)
            {
                if(this < other)
                    return 1f / other.FactorTo(this);

                float factor;
                for(factor = 1; this != other; factor *= 1000)
                    other = other.Next();

                return factor;
            }

            public static bool operator >(Suffix left, Suffix right)
            {
                if(IsSingle(left.value) && IsSingle(right.value))
                    return SingleSuffixes.IndexOf(left.value) > SingleSuffixes.IndexOf(right.value);

                if(IsSingle(right.value))
                    return true;

                if(IsSingle(left.value))
                    return false;

                if(left.value.Length != right.value.Length)
                    return left.value.Length > right.value.Length;

                return string.Compare
                (
                    left.value, right.value,
                    StringComparison.OrdinalIgnoreCase
                ) > 0;
            }

            public static bool operator <(Suffix left, Suffix right)
            {
                return !(left > right || left == right);
            }

            #region Support methods
            static string Before(Suffix suffix)
            {
                Require(suffix > new Suffix("")).True();

                if(IsSingle(suffix.value))
                    return SingleSuffixes[SingleSuffixes.IndexOf(suffix.value) - 1];

                throw new NotImplementedException();
            }

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