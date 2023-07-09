using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float timer = 0;
    private float sprite_timer = 0;
    private int current_sprite = 0;
    private bool live = true;
    private float lifetime;

    [SerializeField]
    Sprite arrowInGround;

    [SerializeField]
    Sprite[] arrowInFlight;

    // Start is called before the first frame update
    void Start()
    {
        transform.position += new Vector3(0, .05f, 0);
        GetComponent<Rigidbody2D>().velocity = (gameObject.transform.right * 5f);
        lifetime = .3f + Random.Range(0.2f, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!live)
        {
            return;
        }
        timer += Time.deltaTime;
        if (timer > sprite_timer)
        {
            sprite_timer += .15f;
            GetComponent<SpriteRenderer>().sprite = arrowInFlight[current_sprite % 2];
            current_sprite++;
        }
        if (timer > lifetime)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<SpriteRenderer>().sprite = arrowInGround;
            live = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        CapsuleCollider2D capsuleCollider = other as CapsuleCollider2D;
        if (capsuleCollider == null)
        {
            return;
        }
        if (other.CompareTag("Human") && live)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().isKinematic = true;
            gameObject.transform.parent = other.gameObject.transform;
            GameObject
                .Find("Particle Manager")
                .GetComponent<ParticleManager>()
                .playBlood(transform.position, transform.rotation, gameObject);
            live = false;
            GameObject.Destroy(gameObject, 2);
        }
    }
}
