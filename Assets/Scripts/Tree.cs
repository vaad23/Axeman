using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tree : MonoBehaviour
{
    [SerializeField] private Trunk _templateTrunk;
    [SerializeField] private Leaves _templateLeaves;
    [SerializeField] private Firewood _templateFirewood;
    [SerializeField] private Collider _collider;

    [SerializeField] private float _hpMax;
    [SerializeField] private int _firewoodCountMax;
    [SerializeField] private float _hpCurrent;
    [SerializeField] private int _firewoodCountCurrent;
    [SerializeField] private int _height;

    private List<Trunk> _trunks;
    private Leaves _leaves;

    public bool IsDestroyed { get; private set; }

    public event UnityAction<Tree> Destroyed;

    private void Awake()
    {
        Create();
    }

    public void TakeDamage(float damage)
    {
        if (IsDestroyed)
            return;

        _hpCurrent -= damage;

        if (_hpCurrent <= 0)
        {
            CreateFirewoods(_firewoodCountCurrent);

            while (_trunks.Count > 0)
            {
                Trunk trunk = _trunks[_trunks.Count - 1];
                _trunks.Remove(trunk);
                Destroy(trunk.gameObject);
            }
            Destroy(_leaves.gameObject);
            _leaves = null;

            _collider.enabled = false;
            IsDestroyed = true;
            Destroyed?.Invoke(this);
        }
        else
        {
            float hpPercent = _hpCurrent / _hpMax;

            while ( hpPercent < (_trunks.Count - 1) * 1f / _height)
            {
                Trunk trunk = _trunks[_trunks.Count - 1];
                _trunks.Remove(trunk);
                Destroy(trunk.gameObject);

                _leaves.transform.localPosition = new Vector3(0, _trunks.Count - 0.5f, 0);
            }

            if (hpPercent < _firewoodCountCurrent * 1f / _firewoodCountMax)
            {
                int count = _firewoodCountCurrent - (int)(_firewoodCountMax * hpPercent);
                if (count > 0)
                    CreateFirewoods(count);
            }
        }
    }

    private void Create()
    {
        _hpCurrent = _hpMax;
        _firewoodCountCurrent = _firewoodCountMax;

        _trunks = new List<Trunk>();
        for (int i = 0; i < _height; i++)
        {
            var trunk = Instantiate(_templateTrunk, transform);
            trunk.transform.localPosition = new Vector3(0, i, 0);
            _trunks.Add(trunk);
        }
        _leaves = Instantiate(_templateLeaves, transform);
        _leaves.transform.localPosition = new Vector3(0, _trunks.Count - 0.5f, 0);

        IsDestroyed = false;
    }

    private void CreateFirewoods(int count)
    {
        _firewoodCountCurrent -= count;
        
        int[] masses = new int[3];

        for (int i = masses.Length; i >0; i--)
        {
            int mass = count / i;
            count -= mass;
            masses[i - 1] = mass;
        }

        for (int i = 0; i < masses.Length; i++)        
            if (masses[i] > 0)
            {
                Firewood firewood = Instantiate(_templateFirewood, transform);
                firewood.Init(masses[i]);
                firewood.transform.localPosition = new Vector3(Random.Range(-1f,1f), _trunks.Count, Random.Range(-1f, 1f));
                firewood.transform.eulerAngles = new Vector3(0, Random.Range(0, 360f), 0);
            }        
    }
}
