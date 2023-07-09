using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShadows : MonoBehaviour
{

    public Vector3 targetPos;
    [SerializeField] private float lifetime = 4f;
    private Vector3 calculatedVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("I'm Alive?");
        calculatedVelocity = (targetPos - transform.position) / lifetime;
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += calculatedVelocity * Time.deltaTime;

        this.transform.rotation = Quaternion.LookRotation(calculatedVelocity);

        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
