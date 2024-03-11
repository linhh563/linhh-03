using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

public class Tree {
    public Node root {get; private set;}

    public Tree(Node root)
    {
        this.root = root;
        this.root.SetPlayerTurn(1);
        DetermineNode(root, 1);
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
        Console.Write(node.value + "(" + node.determinedValue + "): ");

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

    private int DetermineNode(Node node, int playerTurn)
    {
        Node _node;
        int value;

        if (node.isLeaf)
        {
            return node.determinedValue;
        }

        value = (playerTurn == 1) ? DefinedValue.Max : DefinedValue.Min;

        _node = node.firstChild;
        value = DetermineChild(playerTurn, value, _node);

        _node = node.secondChild;
        value = DetermineChild(playerTurn, value, _node);

        _node = node.thirdChild;
        value = DetermineChild(playerTurn, value, _node);

        node.SetPlayerTurn(value);
        return value;
    }

    private int DetermineChild(int playerTurn, int value, Node node)
    {
        int result = value;

        if (playerTurn == 1)
        {
            result = DefinedValue.FindMax(value, DetermineNode(node, 2));
        }
        else 
        {
            result = DefinedValue.FindMin(value, DetermineNode(node, 1));
        }

        return result;
    }
}