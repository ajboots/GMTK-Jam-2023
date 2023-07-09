using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private string targetTag;

    [SerializeField]
    private float damagePerAttack = 10.0f;

    private void ShootArrow()
    {
        if (GetComponent<Archer>().target == null)
        {
            return;
        }
        Vector3 arrowPath = transform.position - GetComponent<Archer>().target.transform.position;
        GameObject.Instantiate(
            GetComponent<Archer>().arrow,
            gameObject.transform.position,
            Quaternion.Euler(0, 0, Mathf.Atan2(arrowPath.y, arrowPath.x) * Mathf.Rad2Deg + 180)
        );
    }

    public void TriggerAttack()
    {
        if (GetComponent<Archer>() != null)
        {
            ShootArrow();
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);

        foreach (GameObject e in enemies)
        {
            if (e.GetComponent<CapsuleCollider2D>().IsTouching(GetComponent<BoxCollider2D>()))
            {
                e.GetComponent<UnitHealth>().TakeDamage(damagePerAttack);

                Vector3 effectLocation = GetComponent<BoxCollider2D>().bounds.max;
                if (gameObject.transform.localScale.x < 0)
                {
                    effectLocation = GetComponent<BoxCollider2D>().bounds.min;
                }
                GameObject
                    .Find("Particle Manager")
                    .GetComponent<ParticleManager>()
                    .playBlood(
                        effectLocation,
                        Quaternion.LookRotation(transform.position - effectLocation, Vector3.up),
                        gameObject
                    );
            }
        }
    }
}
