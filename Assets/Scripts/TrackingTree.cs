using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrackingTree : MonoBehaviour
{
    private HashSet<Tree> _trees;

    public bool IsFound => _trees.Count > 0;

    public event UnityAction Founded;

    private void OnEnable()
    {
        _trees = new HashSet<Tree>();
    }

    private void OnDisable()
    {
        foreach (var tree in _trees)
        {
            tree.Destroyed -= Remove;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Tree tree))
            if (tree.IsDestroyed == false)
            {
                tree.Destroyed += Remove;
                _trees.Add(tree);
                Founded?.Invoke();
            }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent(out Tree tree))
            if (_trees.Contains(tree))
            {
                tree.Destroyed -= Remove;
                _trees.Remove(tree);
            }
    }

    private void Remove(Tree tree)
    {
        _trees.Remove(tree);
    }

}
