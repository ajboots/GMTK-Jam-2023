using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private string targetTag;

    [SerializeField]
    private float damagePerAttack = 10.0f;

    public void TriggerAttack()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);
        Debug.Log("attacking " + enemies.Length);

        foreach (GameObject e in enemies)
        {
            if (e.GetComponent<CircleCollider2D>().IsTouching(GetComponent<BoxCollider2D>()))
            {
                e.GetComponent<UnitHealth>().TakeDamage(damagePerAttack);
            }
        }
    }
}
