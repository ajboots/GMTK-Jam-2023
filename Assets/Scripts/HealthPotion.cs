using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    private Vector3 staringPos;

    // Start is called before the first frame update
    void Start()
    {
        staringPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = staringPos + new Vector3(0, Mathf.Sin(Time.time) * .05f, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goblin"))
        {
            GameObject[] goblins = GameObject.FindGameObjectsWithTag("Goblin");
            foreach (GameObject g in goblins)
            {
                UnitHealth h = g.GetComponent<UnitHealth>();
                if (h != null)
                {
                    h.TakeDamage(-20);
                }
            }
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
