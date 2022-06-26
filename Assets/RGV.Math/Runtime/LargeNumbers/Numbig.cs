namespace RGV.Math.Runtime.LargeNumbers
{
    public readonly partial struct Numbig
    {
        readonly float number;
        readonly Suffix suffix;

        public Numbig(float number, string suffix = "")
        {
            (this.number, this.suffix) = Factorize(number, new Suffix(suffix));
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
    }
}