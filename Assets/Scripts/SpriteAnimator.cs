using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _idleSprites;
    private int _spriteIndex = 0;

    [SerializeField]
    private Sprite[] _walkingSprites;

    [SerializeField]
    private Sprite[] AttackingSprites;

    [SerializeField]
    private float _animationFreq = 1;
    private float _spiteCounter;
    private Vector3 posLastFrame;
    private float _epsilon = .05f;

    public enum AnimationMode
    {
        Idle,
        Walking,
        Attacking,
    }

    public AnimationMode animationMode = AnimationMode.Idle;

    // Start is called before the first frame update
    void Start()
    {
        _spiteCounter = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float movementThisFrame = (posLastFrame - transform.position).magnitude;
        if (movementThisFrame < _epsilon * Time.deltaTime)
        {
            if (animationMode != AnimationMode.Idle)
            {
                _spriteIndex = Random.Range(0, 10);
                _spiteCounter = 0;
            }
            animationMode = AnimationMode.Idle;

            _spiteCounter += Time.deltaTime;
            if (_spiteCounter >= _animationFreq)
            {
                _spiteCounter -= _animationFreq;
                _spriteIndex++;
            }
            GetComponent<SpriteRenderer>().sprite = _idleSprites[
                _spriteIndex % _idleSprites.Length
            ];
        }
        else
        {
            if (animationMode != AnimationMode.Walking)
            {
                _spriteIndex = Random.Range(0, 10);
                _spiteCounter = 0;
            }
            animationMode = AnimationMode.Walking;

            animationMode = AnimationMode.Walking;
            _spiteCounter += movementThisFrame;
            if (_spiteCounter >= _animationFreq)
            {
                _spiteCounter -= _animationFreq;
                _spriteIndex++;
            }
            GetComponent<SpriteRenderer>().sprite = _walkingSprites[
                _spriteIndex % _walkingSprites.Length
            ];
        }

        posLastFrame = transform.position;
    }
}
