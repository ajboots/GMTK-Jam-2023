using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    [SerializeField]
    private float _UnitHP = 100.0f;
    private float _MaxHP;

    [SerializeField]
    private Sprite _deadSprite;

    [SerializeField]
    private GameObject _UIHealthBar;

    public void FixedUpdate()
    {
        if (_UIHealthBar != null)
        {
            _UIHealthBar.GetComponent<AnimateHP>().ScaleHP(_MaxHP, _UnitHP);
        }
    }

    public void Start()
    {
        _MaxHP = _UnitHP;
    }

    public void TakeDamage(float damage)
    {
        _UnitHP -= damage;
        if (_UnitHP < 0)
        {
            if (_UIHealthBar != null)
            {
                _UIHealthBar.GetComponent<AnimateHP>().ScaleHP(_MaxHP, 0);
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = _deadSprite;
            GameObject
                .Find("Particle Manager")
                .GetComponent<ParticleManager>()
                .playBlood(transform.position, Quaternion.Euler(0, 0, 90), gameObject);
            MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour c in comps)
            {
                c.enabled = false;
            }
            gameObject.tag = "Dead";
            GetComponent<CapsuleCollider2D>().enabled = false;
            if (GetComponent<BoxCollider2D>())
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
