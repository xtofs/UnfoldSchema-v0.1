

static class NodeDisplay
{
    // https://andrewlock.net/creating-an-ascii-art-tree-in-csharp/
    public static void WriteTo(this Node node, TextWriter writer)
    {
        var numberOfChildren = node.Nodes.Count;
        for (var i = 0; i < numberOfChildren; i++)
        {
            var child = node.Nodes[i];
            var isLast = (i == (numberOfChildren - 1));
            WriteTo(child, indent: "", isLast: isLast, writer);
        }
    }

    private static void WriteTo(Node node, string indent, bool isLast, TextWriter writer)
    {
        // Print the provided pipes/spaces indent
        Console.Write(indent);

        // Depending if this node is a last child, print the
        // corner or cross, and calculate the indent that will
        // be passed to its children
        if (isLast)
        {
            writer.Write(CONFIG.Corner);
            indent += CONFIG.Space;
        }
        else
        {
            writer.Write(CONFIG.Cross);
            indent += CONFIG.Vertical;
        }

        writer.WriteLine(node.Name);
        // Loop through the children recursively, passing in the
        // indent, and the isLast parameter
        var numberOfChildren = node.Nodes.Count;
        for (var i = 0; i < numberOfChildren; i++)
        {
            var child = node.Nodes[i];
            var isLastChild = (i == (numberOfChildren - 1));
            WriteTo(child, indent, isLastChild, writer);
        }
    }

    record struct Config(
        string Cross, string Corner, string Vertical, string Space)
    { }

    // Constants for indentation
    static Config[] CONFIGS = new[]{
        new Config(" ├─"," └─"," │ ", "   "),
        new Config(" ╠═"," ╚═"," ║ ", "   "),
        new Config(" +-"," +-"," | ", "   "),
    };

    static Config CONFIG = CONFIGS[0];
}