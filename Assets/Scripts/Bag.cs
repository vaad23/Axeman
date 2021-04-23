using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    [SerializeField] private int _mass;
    [SerializeField] private BagFirewood _bagFirewood;

    private HashSet<Firewood> _firewoodsInMoved = new HashSet<Firewood>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Firewood firewood))
        {
            if (_firewoodsInMoved.Contains(firewood) == false)
            {
                firewood.EndedMove += AddMass;
                _firewoodsInMoved.Add(firewood);
                StartCoroutine(firewood.Move(transform));
            }
        }
    }

    private void AddMass(Firewood firewood)
    {
        firewood.EndedMove -= AddMass;
        _firewoodsInMoved.Remove(firewood);

        _mass += firewood.Mass;
        _bagFirewood.SetMass(_mass);

        firewood.gameObject.SetActive(false);
        firewood.transform.SetParent(transform);
    }
}
