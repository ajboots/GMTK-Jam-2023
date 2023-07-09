using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D body;

    [SerializeField]
    private float speed = 1;

    private bool sprinting = false;
    private float horizontal;
    private float vertical;
    public float command;
    public GameObject Target;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            command = 2f;
        }
        else if (Input.GetMouseButton(1))
        {
            command = .5f;
        }
        else
        {
            command = 1f;
        }
        horizontal = Input.GetAxisRaw("Horizontal");

        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // Move Around body
        body.velocity = new Vector2(horizontal * speed, vertical * speed);

        // Move around target
        Target.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Target.transform.position = new Vector3(
            Target.transform.position.x,
            Target.transform.position.y,
            Camera.main.nearClipPlane
        );
    }
}
