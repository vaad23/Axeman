using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagFirewood : MonoBehaviour
{
    [SerializeField] private FirewoodInBag _template;
    [SerializeField] private int _massInFirst;
    [SerializeField] private float _offset;
    [SerializeField] private int _height;

    private List<FirewoodInBag> _firewoods = new List<FirewoodInBag>();

    public void SetMass(int mass)
    {
        while (mass > GetMass(_firewoods.Count))
        {
            FirewoodInBag firewood = Instantiate(_template, transform);
            firewood.transform.localPosition = new Vector3(_firewoods.Count / _height * _offset, _firewoods.Count % _height * _offset, 0);
            _firewoods.Add(firewood);
        }
    }

    private int GetMass(int count)
    {
        return _massInFirst * count * (count + 1) / 2;
    }
}
