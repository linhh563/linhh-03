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

        DetermineNode(root, 1);

        Console.WriteLine("Create new tree successful");
    }

    public void BrowseTree(Node node)
    {
        Console.WriteLine(node.value + "(" + node.determinedValue + ")");

        if (node.isLeaf)
        {
            return;
        }

        BrowseTree(node.firstChild);
        BrowseTree(node.secondChild);
        BrowseTree(node.thirdChild);
    }

    // ham dinh tri cho cay tro choi
    // player turn = 1 thi lay max
    // player turn = 2 thi lay min
    private int DetermineNode(Node node, int playerTurn)
    {
        if (node.isLeaf)
        {
            return node.determinedValue;
        }

        Node _node;

        int value = (playerTurn == 1) ? DefinedValue.Min : DefinedValue.Max;

        // dinh tri nut con 1
        _node = node.firstChild;
        value = DetermineChild(playerTurn, value, _node);

        // dinh tri nut con 2
        _node = node.secondChild;
        value = DetermineChild(playerTurn, value, _node);

        // dinh tri nut con 3
        _node = node.thirdChild;
        value = DetermineChild(playerTurn, value, _node);

        node.SetDeterminedValue(value);`
        return value;
    }

    private int DetermineChild(int playerTurn, int value, Node node)
    {
        int result;

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