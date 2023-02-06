using System;
using static RGV.DesignByContract.Runtime.Contract;

namespace RGV.Math.Runtime.LargeNumbers
{
    public readonly partial struct Numbig
    {
        readonly float number;
        readonly Suffix suffix;

        #region Ctors
        public Numbig(float number, string suffix = "")
        {
            Require(number).Not.Negative();
            Require(Suffix.IsSuffix(suffix)).True();

            (this.number, this.suffix) = Factorize(number, new Suffix(suffix));
        }

        public Numbig(string toBeParsed)
        {
            var parsed = Parse(toBeParsed);

            number = parsed.number;
            suffix = parsed.suffix;
        }
        #endregion

        static (float, Suffix) Factorize(float number, Suffix suffix)
        {
            return number switch
            {
                0 => (0, new Suffix("")),
                >= 1000 => Factorize(number / 1000, suffix.Next()),
                < 1000 and >= 1 => (number, suffix),
                > 0 and < 1 => Factorize(number * 1000, suffix.Prev()),
                _ => throw new InvalidOperationException()
            };
        }

        public Numbig Plus(Numbig other)
        {
            return new Numbig
            (
                (number + other.number) * other.suffix.FactorTo(suffix),
                suffix.value
            );
        }

        public Numbig ConvertTo(string otherSuffix)
        {
            var factor = suffix.FactorTo(new Suffix(otherSuffix));
            return new Numbig(number / factor, suffix.value);
        }

        #region Formatting
        public static Numbig Parse(string from)
        {
            var (n, suffix) = SplitNumberAndSuffix(from);
            Require(Suffix.IsSuffix(suffix)).True();
            Require(float.TryParse(n, out var number)).True();

            return new Numbig(number, suffix);
        }

        public override string ToString()
        {
            return $"{number}{suffix}";
        }
        #endregion

        #region Support  methods
        static (string, string) SplitNumberAndSuffix(string from)
        {
            int i;
            for(i = from.Length - 1; i >= 0 && char.IsLetter(from[i]); i--)
            {
                /*Doesn't need a body*/
            }

            return (from[..(i + 1)], from[(i + 1)..]);
        }
        #endregion
    }
}