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
    private float _attackRange = 1;
    private float _AIinterval = 1f; // Time to retarget Enemies
    private GameObject target = null;
    private Vector3 staringPos;
    private bool _attacking = false;
    private float _epsilon = 0.05f;

    void Start()
    {
        // Call the function with a custom interval
        InvokeRepeating("AITargeting", 0f, _AIinterval);
        staringPos = transform.position;
    }

    void FixedUpdate()
    {
        //if attacking let the coroutine work
        if (_attacking) { }
        //if no target go back to gaurding pos
        else if (target == null)
        {
            Vector3 backToStart =
                (transform.position - staringPos).normalized * _speed * Time.deltaTime;
            Debug.DrawRay(transform.position, backToStart * 20f);
            if ((transform.position - staringPos).magnitude > _epsilon)
            {
                transform.position -= backToStart;
            }
        }
        //should we move closer?
        else if (
            (target.transform.position - transform.position).magnitude < _targetingRange
            && (target.transform.position - transform.position).magnitude > _attackRange
        )
        {
            Vector3 vecToGob =
                (transform.position - target.transform.position).normalized
                * _speed
                * Time.deltaTime;
            transform.position -= vecToGob;
            Debug.DrawRay(transform.position, vecToGob * 20f, Color.red);
        }
        else if ((target.transform.position - transform.position).magnitude < _attackRange)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        if ((target.transform.position - transform.position).x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if ((target.transform.position - transform.position).x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
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
        if (target != null)
        {
            Debug.DrawLine(target.transform.position, transform.position, Color.blue);
        }
    }

    void KillThisUnit()
    {
        CancelInvoke("AITargeting");
        GameObject.Destroy(gameObject);
    }
}
