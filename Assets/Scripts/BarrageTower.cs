using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrageTower : MonoBehaviour
{
    private float _AIinterval = 8f;

    [SerializeField]
    float range = 100f;

    [SerializeField]
    GameObject barrage;

    [SerializeField]
    GameObject shadows;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AITargeting", 0f, _AIinterval);
    }

    // Update is called once per frame
    void Update() { }

    void AITargeting()
    {
        //Target Goblins
        GameObject[] goblins = GameObject.FindGameObjectsWithTag("Goblin");
        Vector3 chosenTarget = goblins[Random.Range(0, goblins.Length - 1)].transform.position;

        //Check if Goblins are in range
        float dist = (chosenTarget - this.transform.position).magnitude;

        Debug.Log(dist);

        if (dist <= range)
        {
            //Spawn Barrage and Arrow Shadows
            GameObject b = Instantiate(barrage, chosenTarget, Quaternion.identity);
            //Debug.Log(b.name);
            GameObject s = Instantiate(
                shadows,
                new Vector3(
                    this.transform.position.x,
                    this.transform.position.y,
                    this.transform.position.z + 5
                ),
                Quaternion.identity
            );
            s.GetComponent<ArrowShadows>().targetPos = chosenTarget;

            BarrageAttack();
        }
    }

    IEnumerator BarrageAttack()
    {
        yield return new WaitForSeconds(2f);
    }
}
