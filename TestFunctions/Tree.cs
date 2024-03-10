using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Tree {
    public Node root {get; private set;}

    public Tree(Node root)
    {
        this.root = root;
    }

    public void CreateTree(Node node)
    {
        if (node.value == 1 || node.value == 2 || node.value == 3)
        {
            return;
        } 

        var child_1 = new Node(node.value - 1);
        node.SetFirstChild(child_1);

        var child_2 = new Node(node.value - 2);
        node.SetSecondChild(child_2);

        var child_3 = new Node(node.value - 3);
        node.SetThirdChild(child_3);

        CreateTree(node.firstChild);
        CreateTree(node.secondChild);
        CreateTree(node.thirdChild);

        Console.WriteLine("Create new tree successful");
    }

    public void BrowseTree(Node node)
    {
        Console.Write(node.value + ": ");

        if (node.isLeaf)
        {
            Console.WriteLine();
            return;
        }

        Console.Write(node.firstChild.value + ", ");
        Console.Write(node.secondChild.value + ", ");
        Console.WriteLine(node.thirdChild.value);

        BrowseTree(node.firstChild);
        BrowseTree(node.secondChild);
        BrowseTree(node.thirdChild);
    }
}