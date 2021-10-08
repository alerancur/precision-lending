using System;
using System.Text;
using Xunit;

namespace Question1
{
    public class StringOperationTests
    {
        /// <summary>
        /// This function receives two strings and interveles them in one
        /// </summary>
        /// <remarks>
        /// If one of the strings is null then the other string is returned
        /// If both are null then empty string is returned
        /// </remarks>
        /// <param name="string1"></param>
        /// <param name="string2"></param>
        /// <returns></returns>
        public static string InterleavedStrings(string string1, string string2)
        {
            var result = new StringBuilder();

            if (string1 == null || string2 == null)
                return string1 ?? string2 ?? result.ToString();

            for (int i = 0; i < Math.Max(string1.Length, string2.Length); i++)
            {
                result.Append($"{(i < string1.Length ? string1[i] : string.Empty)}{(i < string2.Length ? string2[i] : string.Empty)}");
            }

            return result.ToString();
        }

        [Fact]
        public void TestInterleavedStrings()
        {
            var result = InterleavedStrings("abc", "123");
            Assert.Equal("a1b2c3", result);

            result = InterleavedStrings("abcde", "123");
            Assert.Equal("a1b2c3de", result);

            result = InterleavedStrings("abc", "12345");
            Assert.Equal("a1b2c345", result);

            result = InterleavedStrings(null, "12345");
            Assert.Equal("12345", result);

            result = InterleavedStrings("abc", null);
            Assert.Equal("abc", result);
        }
    }
}
