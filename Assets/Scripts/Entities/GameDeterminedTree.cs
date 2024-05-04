using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameDeterminedTree
{
    public Node root;
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

        for (int i = 1; i <= 3; i++)
        {
            var child = new Node(root.value - i);
            root.AddChild(child);
        }

        foreach (var child in root.children)
        {
            CreateTree(child);
        }

        DetermineNode(root);
    }

    // determine the treatment for node (parameter)
    public int DetermineNode(Node node)
    {
        if (node.isLeaf)
        {
            return node .determinedValue;
        }

        int value = node.determinedValue;

        foreach (var child in node.children)
        {
            if (node.playerTurn == 1)
            {
                value = DefinedValue.FindMax(value, DetermineNode(child));
                if (value >= node.determinedValue)
                {
                    node.SetDeterminedValue(value);
                    return value;
                }
            }
            else
            {
                value = DefinedValue.FindMin(value, DetermineNode(child));
                if (value <= node.determinedValue)
                {
                    node.SetDeterminedValue(value);
                    return value;
                }
            }
        }

        node.SetDeterminedValue(value);
        return value;
    }
}