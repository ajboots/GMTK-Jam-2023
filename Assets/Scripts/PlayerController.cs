using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;

    public GameObject Target;

    public enum Command
    {
        GroupUp, ActFreely, FocusTarget
    }
    
    // Update is called once per frame
    void Update()
    {
        doMovement();
    }

    void doMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position.Set(transform.position.x, transform.position.y + speed, transform.position.z);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position.Set(transform.position.x - speed, transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position.Set(transform.position.x, transform.position.y - speed, transform.position.z);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position.Set(transform.position.x + speed, transform.position.y, transform.position.z);
        }
    }
}
