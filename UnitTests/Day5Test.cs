using _2022;

namespace UnitTests
{
    public class Day5Test
    {
        private string _input;

        [SetUp]
        public void Setup()
        {
            _input = """
                    [D]    
                [N] [C]    
                [Z] [M] [P]
                 1   2   3 

                move 1 from 2 to 1
                move 3 from 1 to 3
                move 2 from 2 to 1
                move 1 from 1 to 2
                """;
        }

        [Test]
        public void Test1()
        {
            var result = Day5.Solve1(_input);
            Assert.That(result, Is.EqualTo("CMZ"));
        }

        [Test]
        public void Test2()
        {
            var result = Day5.Solve2(_input);
            Assert.That(result, Is.EqualTo("MCD"));
        }
    }
}