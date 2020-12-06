using System.IO;
using System.Reflection;
using Xunit;
using AoC2020.Days;

namespace AoC2020.Tests.Days
{
    public class Day05Tests
    {
        [Fact]
        public void Day05Test1()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day05_1.txt")[0];
            var day05 = new Day05(inputFile);

            // act
            var result1 = day05.PartOne();

            // assert
            Assert.Equal("357", result1);
        }
        [Fact]
        public void Day05Test2()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day05_2.txt")[0];
            var day05 = new Day05(inputFile);

            // act
            var result1 = day05.PartOne();

            // assert
            Assert.Equal("567", result1);
        }
        [Fact]
        public void Day05Test3()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day05_3.txt")[0];
            var day05 = new Day05(inputFile);

            // act
            var result1 = day05.PartOne();

            // assert
            Assert.Equal("119", result1);
        }
        [Fact]
        public void Day05Test4()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day05_4.txt")[0];
            var day05 = new Day05(inputFile);

            // act
            var result1 = day05.PartOne();

            // assert
            Assert.Equal("820", result1);
        }
    }
}