using _2022;

namespace UnitTests;

public class Day5Test
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase("""
                    [D]    
                [N] [C]    
                [Z] [M] [P]
                 1   2   3 

                move 1 from 2 to 1
                move 3 from 1 to 3
                move 2 from 2 to 1
                move 1 from 1 to 2
                """)]
    public void Day5_Test1(string input)
    {
        var result = Day5.Solve1(input);
        Assert.That(result, Is.EqualTo("CMZ"));
    }

    [TestCase("""
                    [D]    
                [N] [C]    
                [Z] [M] [P]
                 1   2   3 

                move 1 from 2 to 1
                move 3 from 1 to 3
                move 2 from 2 to 1
                move 1 from 1 to 2
                """)]
    public void Day5_Test2(string input)
    {
        var result = Day5.Solve2(input);
        Assert.That(result, Is.EqualTo("MCD"));
    }


    [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", ExpectedResult = "5")]
    [TestCase("nppdvjthqldpwncqszvftbrmjlhg", ExpectedResult = "6")]
    [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", ExpectedResult = "10")]
    [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", ExpectedResult = "11")]
    public string Day6_Test1(string input)
    {
        return Day6.Solve1(input);
    }

    [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", ExpectedResult = "19")]
    [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", ExpectedResult = "23")]
    [TestCase("nppdvjthqldpwncqszvftbrmjlhg", ExpectedResult = "23")]
    [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", ExpectedResult = "29")]
    [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", ExpectedResult = "26")]
    public string Day6_Test2(string input)
    {
        return Day6.Solve2(input);
    }

    [TestCase("""
        $ cd /
        $ ls
        dir a
        14848514 b.txt
        8504156 c.dat
        dir d
        $ cd a
        $ ls
        dir e
        29116 f
        2557 g
        62596 h.lst
        $ cd e
        $ ls
        584 i
        $ cd ..
        $ cd ..
        $ cd d
        $ ls
        4060174 j
        8033020 d.log
        5626152 d.ext
        7214296 k
        """, ExpectedResult = "95437")]
    public string Day7_Test1(string input)
    {
        return Day7.Solve1(input);
    }

    [TestCase("""
        $ cd /
        $ ls
        dir a
        14848514 b.txt
        8504156 c.dat
        dir d
        $ cd a
        $ ls
        dir e
        29116 f
        2557 g
        62596 h.lst
        $ cd e
        $ ls
        584 i
        $ cd ..
        $ cd ..
        $ cd d
        $ ls
        4060174 j
        8033020 d.log
        5626152 d.ext
        7214296 k
        """, ExpectedResult = "24933642")]
    public string Day7_Test2(string input)
    {
        return Day7.Solve2(input);
    }

    [TestCase("""
        30373
        25512
        65332
        33549
        35390
        """, ExpectedResult = "21")]
    public string Day8_Test1(string input)
    {
        return Day8.Solve1(input);
    }

    [TestCase("""
        30373
        25512
        65332
        33549
        35390
        """, ExpectedResult = "8")]
    public string Day8_Test2(string input)
    {
        return Day8.Solve2(input);
    }

    [TestCase("""
        R 4
        U 4
        L 3
        D 1
        R 4
        D 1
        L 5
        R 2
        """, ExpectedResult = "13")]
    public string Day9_Test1(string input)
    {
        return Day9.Solve1(input);
    }

    [TestCase("""
        R 5
        U 8
        L 8
        D 3
        R 17
        D 10
        L 25
        U 20
        """, ExpectedResult = "36")]
    public string Day9_Tes2(string input)
    {
        return Day9.Solve2(input);
    }
}