using System.IO;
using System.Reflection;
using Xunit;
using AoC2020.Days;

namespace AoC2020.Tests.Days
{
    public class Day21Tests
    {
        [Fact]
        public void Day21Test1()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day21_1.txt")[0];
            var day21 = new Day21(inputFile);

            // act
            var result1 = day21.PartOne();
            var result2 = day21.PartTwo();

            // assert
            Assert.Equal("5", result1);
            Assert.Equal("mxmxvkd,sqjhc,fvjkl", result2);
        }
    }
}