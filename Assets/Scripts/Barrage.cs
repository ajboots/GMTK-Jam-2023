using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrage : MonoBehaviour
{
    private float _AIinterval = 5f;
    private bool waiting = true;

    [SerializeField]
    private float _roationSpeed = 10;

    [SerializeField]
    Sprite[] barrageAnimation;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AITargeting", 0f, _AIinterval);
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        if (waiting)
        {
            gameObject.transform.rotation =
                gameObject.transform.rotation
                * Quaternion.Euler(0, 0, _roationSpeed * Time.deltaTime);
        }
    }

    IEnumerator Attack()
    {
        float animationLength = .2f;
        SpriteRenderer r = GetComponent<SpriteRenderer>();
        int index = 0;
        while (index < barrageAnimation.Length)
        {
            yield return new WaitForSeconds(animationLength);
            if (index == 17)
            {
                GetComponent<ParticleSystem>().Play();
                animationLength = .1f;
            }
            if (index == 18)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Goblin");

                foreach (GameObject e in enemies)
                {
                    if (
                        e.GetComponent<CapsuleCollider2D>()
                            .IsTouching(GetComponent<BoxCollider2D>())
                    )
                    {
                        e.GetComponent<UnitHealth>().TakeDamage(2000);

                        Vector3 effectLocation = e.transform.position;

                        GameObject
                            .Find("Particle Manager")
                            .GetComponent<ParticleManager>()
                            .playBlood(
                                effectLocation,
                                Quaternion.LookRotation(Vector3.forward, Vector3.up),
                                gameObject
                            );
                    }
                }
            }
            GetComponent<SpriteRenderer>().sprite = barrageAnimation[index];
            index++;
        }

        _roationSpeed = 0;
        GameObject.Destroy(gameObject, 3f);
    }
}
