using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    [SerializeField]
    private string targetTag;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    // IEnumerator Attack()
    // {
    //     if ((target.transform.position - transform.position).x < 0)
    //     {
    //         transform.localScale = new Vector3(-1, 1, 1);
    //     }
    //     else if ((target.transform.position - transform.position).x > 0)
    //     {
    //         transform.localScale = new Vector3(1, 1, 1);
    //     }

    //     GetComponent<SpriteAnimator>().StartAttack();
    //     yield return new WaitForSeconds(1f);
    // }
}
