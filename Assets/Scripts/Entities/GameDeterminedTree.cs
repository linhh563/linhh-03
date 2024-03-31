using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameDeterminedTree
{
    public Node root {get; private set;}
    public GameDeterminedTree(Node root)
    {
        this.root = root;
        this.root.SetPlayerTurn(1);
    }

    public GameDeterminedTree(int value)
    {
        this.root = new Node(value);
        this.root.SetPlayerTurn(1);
    }

    public void CreateTree(Node root)
    {
        if (root.value == 1 ||  root.value == 2 || root.value == 3)
        {
            return;
        }

        var child_1 = new Node(root.value - 1);
        root.AddFirstChild(child_1);

        var child_2 = new Node(root.value - 2);
        root.AddSecondChild(child_2);

        var child_3 = new Node(root.value - 3);
        root.AddThirdChild(child_3);

        CreateTree(root.firstChild);
        CreateTree(root.secondChild);
        CreateTree(root.thirdChild);

        DetermineNode(root, 1);
        Debug.Log("Create tree successful");
    }

    // determine the treatment for node (parameter)
    public int DetermineNode(Node node, int playerTurn)
    {
        if (node.isLeaf)
        {
            return node .determinedValue;
        }

        int value = (playerTurn == 1) ? DefinedValue.Min : DefinedValue.Max;

        var _node = node.firstChild;
        value = DetermineChild(playerTurn, value, _node);

        _node = node.secondChild;
        value = DetermineChild(playerTurn, value, _node);

        _node = node.thirdChild;
        value = DetermineChild(playerTurn, value, _node);

        node.SetDeterminedValue(value);
        return value;
    }

    public int DetermineChild(int playerTurn, int value, Node node)
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
