using static RGV.DesignByContract.Runtime.Precondition;

namespace RGV.Math.Runtime.LargeNumbers
{
    public readonly partial struct Numbig
    {
        readonly float number;
        readonly Suffix suffix;

        public Numbig(float number, string suffix = "")
        {
            Require(Suffix.IsSuffix(suffix)).True();

            (this.number, this.suffix) = Factorize(number, new Suffix(suffix));
        }

        public Numbig(string toBeParsed)
        {
            var parsed = Parse(toBeParsed);

            number = parsed.number;
            suffix = parsed.suffix;
        }

        static (float, Suffix) Factorize(float number, Suffix suffix)
        {
            return number < 1000
                ? (number, suffix)
                : Factorize(number / 1000, suffix.Next());
        }

        public override string ToString()
        {
            return $"{number}{suffix}";
        }

        public Numbig Plus(Numbig other)
        {
            return default;
        }

        public static Numbig Parse(string from)
        {
            var (n, suffix) = SplitNumberAndSuffix(from);
            Require(Suffix.IsSuffix(suffix)).True();
            Require(float.TryParse(n, out var number)).True();

            return new Numbig(number, suffix);
        }

        #region Support  methods
        static (string, string) SplitNumberAndSuffix(string from)
        {
            int i;
            for(i = from.Length - 1; i >= 0 && char.IsLetter(from[i]); i--) ;
            return (from[..(i + 1)], from[(i + 1)..]);
        }
        #endregion
    }
}