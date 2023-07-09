using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed;
    private GameObject king;
    private Vector3 _startingKingOffset;
    private float _epsilon = .05f;

    [SerializeField]
    void Start()
    {
        king = GameObject.Find("Goblin King");
        _startingKingOffset = king.transform.position - transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GetComponent<SpriteAnimator>().animationMode == SpriteAnimator.AnimationMode.Attacking)
        {
            return;
        }
        Vector3 mouseToKing =
            Camera.main.ScreenToWorldPoint(Input.mousePosition) - king.transform.position;

        float angleToXaxis = Mathf.Atan2(mouseToKing.y, mouseToKing.x);
        Debug.DrawRay(
            king.transform.position,
            Quaternion.Euler(0, 0, Mathf.Rad2Deg * angleToXaxis) * Vector3.right * 100,
            Color.green
        );
        Vector3 desiredPosition =
            king.transform.position
            - Quaternion.Euler(0, 0, Mathf.Rad2Deg * angleToXaxis)
                * (_startingKingOffset * king.GetComponent<PlayerController>().command);
        float currentKingOffset = (desiredPosition - transform.position).magnitude;
        if (currentKingOffset > _epsilon)
        {
            Vector3 movementVec =
                (transform.position - desiredPosition).normalized
                * speed
                * Mathf.Clamp((transform.position - desiredPosition).magnitude, 0.5f, 1.5f);
            Debug.DrawRay(transform.position, movementVec, Color.black, 0.1f);

            transform.position -= movementVec;
        }
    }
}
