namespace _2022;

public class Day7 : ISolve
{
    public static string Solve1(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var root = Parse(lines);

        var result = root.SearchDirectories()
            .Where(x => x.Size < 100000)
            .Sum(x => x.Size);
        return result.ToString();
    }

    static DirectoryNode Parse(string[] lines)
    {
        var root = new DirectoryNode(null, "/");
        DirectoryNode? currentDirectory = null;
        foreach (var line in lines)
        {
            // 명령어의 경우 명령 처리
            if (line.StartsWith("$") is true)
            {
                var tokens = line.Split(' ');
                if (tokens[1] is "cd")
                {
                    if (tokens[2] is "/")
                        currentDirectory = root;
                    else if (tokens[2] is "..")
                    {
                        currentDirectory = currentDirectory?.Parent;
                    }
                    else
                    {
                        var dictionaryName = tokens[2];
                        currentDirectory = currentDirectory?.Nodes.FirstOrDefault(x => x is DirectoryNode && x.Name == dictionaryName) as DirectoryNode;
                    }
                }
                else if (tokens[1] is "ls")
                    continue;
                else
                {
                    throw new InvalidOperationException();
                }
            }
            // 아닌 경우 파일 사이즈 취합
            else
            {
                var tokens = line.Split(' ');

                // 디렉토리
                if (tokens[0] is "dir")
                {
                    var newDirectory = new DirectoryNode(currentDirectory, tokens[1]);
                    currentDirectory?.AddNode(newDirectory);
                }
                // 파일
                else
                {
                    var newFile = new FileNode(tokens[1], long.Parse(tokens[0]));
                    currentDirectory?.AddNode(newFile);
                }
            }
        }

        return root;
    }


    public static string Solve2(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var root = Parse(lines);

        var usedSize = root.Size;
        var mustDeleteSize = 30000000 - (70000000 - usedSize);

        var result = root.SearchDirectories()
            .Where(x => x.Size > mustDeleteSize)
            .OrderBy(x => x.Size)
            .Select(x => x.Size)
            .First();
        return result.ToString();
    }


    abstract record Node(string Name)
    {
        public abstract long Size { get; }
    }
    record DirectoryNode(DirectoryNode? Parent, string Name) : Node(Name)
    {
        public IEnumerable<Node> Nodes { get; } = new List<Node>();

        public override long Size
        {
            get
            {
                var totals = 0L;
                foreach (var node in Nodes)
                    totals += node.Size;
                return totals;
            }
        }

        public IEnumerable<DirectoryNode> SearchDirectories()
        {
            foreach (var node in Nodes)
            {
                if (node is DirectoryNode directory)
                {
                    yield return directory;
                    foreach (var subDirectory in directory.SearchDirectories())
                        yield return subDirectory;
                }
            }
        }

        public void AddNode(Node node) => (Nodes as List<Node>)!.Add(node);
    }
    record FileNode(string Name, long FileSize) : Node(Name)
    {
        public override long Size => FileSize;
    }
}
