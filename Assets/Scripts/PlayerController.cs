using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D body;

    [SerializeField]
    private float speed = 10;

    private bool sprinting = false;

    private float horizontal, vertical;

    public GameObject Target;

    public enum Command
    {
        GroupUp, ActFreely, FocusTarget
    }
    
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        sprinting = Input.GetKey(KeyCode.LeftShift);
    }

    private void FixedUpdate()
    {
        // Move Around body
        body.velocity = new Vector2(horizontal * speed * (sprinting ? 2 : 1), vertical * speed * (sprinting ? 2 : 1));

        // Move around target
        Target.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Target.transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, Camera.main.nearClipPlane);
    }
}
