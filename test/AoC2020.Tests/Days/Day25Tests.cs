using System.IO;
using System.Reflection;
using Xunit;
using AoC2020.Days;

namespace AoC2020.Tests.Days
{
    public class Day25Tests
    {
        [Fact]
        public void Day25Test1()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day25_1.txt")[0];
            var day25 = new Day25(inputFile);

            // act
            var result1 = day25.PartOne();

            // assert
            Assert.Equal("14897079", result1);
        }
    }
}