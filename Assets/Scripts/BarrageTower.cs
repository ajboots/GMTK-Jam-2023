using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrageTower : MonoBehaviour
{
    private float _AIinterval = 5f;

    [SerializeField]
    GameObject barrage;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AITargeting", 0f, _AIinterval);
    }

    // Update is called once per frame
    void Update() { }

    void AITargeting()
    {
        GameObject[] goblins = GameObject.FindGameObjectsWithTag("Goblin");
        Vector3 chosenTarget = goblins[Random.Range(0, goblins.Length)]
            .gameObject
            .transform
            .position;
    }

    IEnumerator BarrageAttack()
    {
        yield return new WaitForSeconds(1f);
    }
}
