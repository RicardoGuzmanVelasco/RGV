using System;
using System.Linq;
using static RGV.DesignByContract.Runtime.Precondition;

namespace RGV.Math.Runtime.LargeNumbers
{
    public readonly partial struct Numbig
    {
        readonly float number;
        readonly string suffix;

        public Numbig(float number, string suffix = "")
        {
            Require<ArgumentException>(suffix.All(char.IsLetter)).True();
            Require<ArgumentException>(suffix.Length <= 1 == IsSingle(suffix)).True();

            (this.number, this.suffix) = Factorize(number, suffix);
        }

        static (float, string) Factorize(float number, string suffix = null)
        {
            suffix = ToSuffix(suffix);
            return number < 1000
                ? (number, suffix)
                : Factorize(number / 1000, SuffixAfter(suffix));
        }

        public override string ToString()
        {
            return $"{number}{suffix}";
        }
    }
}