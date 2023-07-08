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
    private float _epsilon = .1f;

    void Start()
    {
        king = GameObject.Find("Goblin King");
        _startingKingOffset = king.transform.position - transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.y
        );
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
            - Quaternion.Euler(0, 0, Mathf.Rad2Deg * angleToXaxis) * _startingKingOffset;
        Debug.DrawLine(transform.position, desiredPosition, Color.black, 0.1f);
        float currentKingOffset = (desiredPosition - transform.position).magnitude;
        if (currentKingOffset > _epsilon)
        {
            Vector3 movementVec =
                (transform.position - desiredPosition).normalized
                * speed
                * Mathf.Min((transform.position - desiredPosition).magnitude, 3);
            transform.position -= movementVec;
            if (movementVec.magnitude > _epsilon * speed)
            {
                if (movementVec.x < 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else if (movementVec.x > 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }
        }
    }
}
