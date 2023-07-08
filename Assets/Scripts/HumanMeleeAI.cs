using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMeleeAI : MonoBehaviour
{
    [SerializeField]
    private float _targetingRange = 2;

    [SerializeField]
    private float _speed = 2;

    [SerializeField]
    private float _attackRange = 2;
    private float _AIinterval = 1f; // Time to retarget Enemies
    private GameObject target;
    private Vector3 staringPos;
    private bool _attacking = false;

    void Start()
    {
        // Call the function with a custom interval
        InvokeRepeating("AITargeting", 0f, _AIinterval);
    }

    void FixedUpdate()
    {
        //if attacking let the coroutine work
        if (_attacking) { }
        //if no target go back to gaurding pos
        else if (target == null)
        {
            transform.position += (transform.position - staringPos).normalized * _speed;
        }
        //should we attack?
        else if ((target.transform.position - transform.position).magnitude < _attackRange)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        GetComponent<SpriteAnimator>().StartAttack();
        yield return new WaitForSeconds(1f);
    }

    void AITargeting()
    {
        GameObject[] goblins = GameObject.FindGameObjectsWithTag("Goblin");
        GameObject closestGoblin = null;
        float closestDistance = _targetingRange;
        foreach (GameObject g in goblins)
        {
            float gobDistance = (g.transform.position - transform.position).magnitude;
            if (gobDistance < closestDistance)
            {
                closestGoblin = g;
                closestDistance = gobDistance;
            }
        }
        target = closestGoblin;
    }

    void KillThisUnit()
    {
        CancelInvoke("AITargeting");
        GameObject.Destroy(gameObject);
    }
}
