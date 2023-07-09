using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    [SerializeField]
    private float _UnitHP = 100.0f;

    [SerializeField]
    private Sprite _deadSprite;

    [SerializeField]
    bool isBarricade = false;

    public void TakeDamage(float damage)
    {
        _UnitHP -= damage;

        if (_UnitHP < 0)
        {
            if (isBarricade)
            {
                GetComponent<Barricade>().Destruct();
            }
            else 
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = _deadSprite;
                GameObject
                    .Find("Particle Manager")
                    .GetComponent<ParticleManager>()
                    .playBlood(transform.position, Quaternion.Euler(0, 0, 90), gameObject);
            }
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
