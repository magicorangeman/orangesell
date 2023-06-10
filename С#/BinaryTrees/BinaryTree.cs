using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTrees;

public class BinaryTree<TKey> : IEnumerable<TKey>
    where TKey : IComparable
{
    public TKey Value { get; set; }
    public int Size;
    public BinaryTree<TKey> Left, Right;
    private bool IsInit;

    public BinaryTree()
    {
        Value = default;
        Size = 0;
        IsInit = false;
    }

    public TKey this[int index]
    {
        get
        {
            if ( index < 0 && index > Size - 1)
                throw new IndexOutOfRangeException();

            if (index < Size - 1)
            {
                if (Left != null)
                {
                    if (Left.Size > index)
                        return Left[index];
                    if (Left.Size + 1 <= index)
                        return Right[index - Left.Size - 1];
                }
                else if (index != 0 && Right != null)
                    return Right[index - 1];
            }
            else if (index == Size - 1 && Right != null)
                return Left != null ? Right[index - Left.Size - 1] : Right[index - 1];
            return Value;
        }

        set { this[index] = value; }
    }

    public void Add(TKey key)
    {
        var tree = this;
        if (key is null) return;

        while (true)
        {
            tree.Size++;
            if (!tree.IsInit)
            {
                tree.Value = key;
                tree.IsInit = true;
                break;
            }

            tree = key.CompareTo(tree.Value) < 0 ? GrowLeft(tree) : GrowRight(tree);
        }
    }

    public bool Contains(TKey key)
    {
        if (!IsInit) return false;
        var tree = this;
        while (tree.IsInit)
        {
            if (key.Equals(tree.Value)) return true;
            tree = key.CompareTo(tree.Value) < 0 ? GrowLeft(tree) : GrowRight(tree);
        }
        return false;
    }

    public IEnumerator<TKey> GetEnumerator()
    {
        if (IsInit)
        {
            if (Left != null)
                foreach (var left in Left) yield return left;
            if (Right != null)
            {
                yield return Value;
                foreach (var right in Right) yield return right;
            }
            else yield return Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private static BinaryTree<TKey> GrowLeft(BinaryTree<TKey> tree)
    {
        tree.Left ??= new BinaryTree<TKey>();
        return tree.Left;
    }

    private static BinaryTree<TKey> GrowRight(BinaryTree<TKey> tree)
    {
        tree.Right ??= new BinaryTree<TKey>();
        return tree.Right;
    }
}