using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looter : MonoBehaviour
{
    [SerializeField] bool _isRandomLootAmount = true;
    
    [SerializeField] GameObject[] loot;
    [SerializeField] int[] _maxLoot;
    [SerializeField] float _lootRange;
    private int _randomLootAmount;
    private Vector3 _instantiatepoint ;

    public void Loot()
    {
        for (int i = 0; i < loot.Length; i++)
        {
            if (_isRandomLootAmount)
            {
                _randomLootAmount = Random.Range(0, _maxLoot[i]);
            }
            else
            {
                _randomLootAmount = _maxLoot[i];
            }

            for (int x = 0; x < _randomLootAmount; x++)
            {
                _instantiatepoint = new Vector3(Random.Range(-_lootRange, _lootRange), 0, Random.Range(-1f, 1f)) + transform.position;
                Object.Instantiate(loot[i], _instantiatepoint, Quaternion.identity);   // ca fait pop 
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) { Loot(); }    // Debug
    }
}
