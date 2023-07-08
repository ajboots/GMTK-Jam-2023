using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    [SerializeField]
    private float _UnitHP = 100.0f;

    public void TakeDamage(float damage)
    {
        _UnitHP -= damage;
        if (_UnitHP < 0)
        {
            GameObject.Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}
