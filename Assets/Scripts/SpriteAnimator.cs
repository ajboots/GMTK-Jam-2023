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
    private float _walkingAnimationFreq = 1;
    private float _randomAniomationOffset = .1f;
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

    public void StartAttack()
    {
        animationMode = AnimationMode.Attacking;
        _spiteCounter = 0;
        _spriteIndex = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float movementThisFrame = (posLastFrame - transform.position).magnitude;
        if (animationMode == AnimationMode.Attacking)
        {
            _spiteCounter += Time.deltaTime;
            if (_spiteCounter >= _idleAnimationFreq)
            {
                _spiteCounter -= _idleAnimationFreq;
                _spriteIndex++;
            }
            GetComponent<SpriteRenderer>().sprite = _attackingSprites[
                _spriteIndex % _attackingSprites.Length
            ];
            if (_spiteCounter > _attackingSprites.Length + 1)
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
