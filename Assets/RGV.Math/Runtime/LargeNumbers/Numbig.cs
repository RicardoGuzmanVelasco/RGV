namespace RGV.Math.Runtime.LargeNumbers
{
    public readonly partial struct Numbig
    {
        readonly float number;
        readonly string suffix;

        public Numbig(float number, string suffix = null)
        {
            (this.number, this.suffix) = Factorize(number, suffix);
        }

        static (float, string) Factorize(float number, string suffix = null)
        {
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