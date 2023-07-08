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
    private Sprite[] _attackingSprites;

    [SerializeField]
    private float _idleAnimationFreq = 1;

    [SerializeField]
    private float _attackAnimationFreq = 5;

    [SerializeField]
    private int _attackHitID = 0;

    [SerializeField]
    private float _walkingAnimationFreq = 1;
    private float _randomAniomationOffset = .1f;
    private float _spiteCounter;
    private Vector3 posLastFrame;
    private float _epsilon = .1f;

    [SerializeField]
    private float attack_cooldown = 1.5f;

    private float attack_counter = 0.0f;
    private float spin_cooldown = 0.5f;
    private float spin_counter = 0f;

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

    public void StartAttack()
    {
        if (attack_counter < attack_cooldown)
        {
            return;
        }
        animationMode = AnimationMode.Attacking;
        _spiteCounter = 0;
        _spriteIndex = 0;
        attack_counter = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //to buffer units on z axis
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.y / 1000
        );
        attack_counter += Time.deltaTime;
        spin_counter += Time.deltaTime;
        float movementThisFrame = (posLastFrame - transform.position).magnitude;
        if (
            movementThisFrame > _epsilon * Time.deltaTime
            && animationMode != AnimationMode.Attacking
            && spin_counter > spin_cooldown
        )
        {
            if ((posLastFrame - transform.position).x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                spin_counter = 0;
            }
            else if ((posLastFrame - transform.position).x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                spin_counter = 0;
            }
        }
        if (animationMode == AnimationMode.Attacking)
        {
            _spiteCounter += Time.deltaTime;
            if (_spiteCounter >= _attackAnimationFreq)
            {
                _spiteCounter -= _attackAnimationFreq;
                _spriteIndex++;
            }
            if (_spriteIndex == _attackHitID)
            {
                if (GetComponent<Attack>() != null)
                {
                    GetComponent<Attack>().TriggerAttack();
                }
            }
            else { }
            GetComponent<SpriteRenderer>().sprite = _attackingSprites[
                _spriteIndex % _attackingSprites.Length
            ];

            if (_spriteIndex > _attackingSprites.Length + 1)
            {
                animationMode = AnimationMode.Idle;
            }
        }
        else if (movementThisFrame < _epsilon * Time.deltaTime)
        {
            if (animationMode != AnimationMode.Idle)
            {
                _spriteIndex = Random.Range(0, 10);
                _spiteCounter = 0;
            }
            animationMode = AnimationMode.Idle;

            _spiteCounter += Time.deltaTime;
            if (_spiteCounter >= _idleAnimationFreq + _randomAniomationOffset)
            {
                _spiteCounter -= _idleAnimationFreq + _randomAniomationOffset;
                _spriteIndex++;
                _randomAniomationOffset = Random.Range(0, _idleAnimationFreq / 5);
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
            if (_spiteCounter >= _walkingAnimationFreq + _randomAniomationOffset)
            {
                _spiteCounter -= _walkingAnimationFreq + _randomAniomationOffset;
                _spriteIndex++;
                _randomAniomationOffset = Random.Range(0, _walkingAnimationFreq / 5);
            }
            GetComponent<SpriteRenderer>().sprite = _walkingSprites[
                _spriteIndex % _walkingSprites.Length
            ];
        }

        posLastFrame = transform.position;
    }
}
