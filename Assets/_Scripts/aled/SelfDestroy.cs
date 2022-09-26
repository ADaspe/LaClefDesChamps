using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] float lifeTime;
    private GameObject _object;
    private void Awake()
    {
        _object = GetComponent<GameObject>();
        Destroy(gameObject, lifeTime);   
    }
}