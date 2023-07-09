using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    [SerializeField]
    public GameObject arrow;

    [SerializeField]
    private string targetTag;
    public GameObject target = null;

    [SerializeField]
    private float _attackRange = 1;
    private float _AIinterval = 1f; // Time to retarget Enemies

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AITargeting", 0f, _AIinterval);
    }

    // Update is called once per frame
    void Update() { }

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

    void FixedUpdate()
    {
        //if no target go back to gaurding pos
        if (target == null) { }
        //should we move closer?
        else if (
            (target.transform.position - transform.position).magnitude < _attackRange
            && GameObject.Find("Goblin King").GetComponent<PlayerController>().command != 0.5
        )
        {
            StartCoroutine(Attack());
        }
    }

    void AITargeting()
    {
        GameObject[] humans = GameObject.FindGameObjectsWithTag("Human");
        GameObject closestHuman = null;
        float closestDistance = _attackRange;
        foreach (GameObject g in humans)
        {
            float gobDistance = (g.transform.position - transform.position).magnitude;
            if (gobDistance < closestDistance)
            {
                closestHuman = g;
                closestDistance = gobDistance;
            }
        }
        target = closestHuman;
        if (target != null)
        {
            Debug.DrawLine(target.transform.position, transform.position, Color.cyan, .5f);
        }
    }
}
