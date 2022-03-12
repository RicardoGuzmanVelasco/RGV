namespace RGV.Extensions.Runtime
{
    public static class StringExtensions
    {
        public static int LongestContiguous(this string source)
        {
            if(string.IsNullOrEmpty(source))
                return 0;
            
            var maxRepeatedFollowingChars = 1;
            for(var i = 0; i < source.Length - 1; i++)
                if(source[i] == source[i + 1])
                    maxRepeatedFollowingChars++;
                else
                    maxRepeatedFollowingChars = 1;

            return maxRepeatedFollowingChars;
        } 
    }
}