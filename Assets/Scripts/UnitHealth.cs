using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    [SerializeField]
    private float _UnitHP = 100.0f;

    [SerializeField]
    private Sprite _deadSprite;

    public void TakeDamage(float damage)
    {
        _UnitHP -= damage;
        if (_UnitHP < 0)
        {
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
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}
