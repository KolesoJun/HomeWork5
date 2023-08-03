using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(ParticleSystem))]
public class ThiefController : MonoBehaviour
{
    public const string AxisHorizontal = "Horizontal";

    [SerializeField] private float _speed;
    [SerializeField] private float _force;
    [SerializeField] private float _health;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private ParticleSystem _effect;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _effect = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    public void TakeDamege(float damage)
    {
        _health -= damage;
        _effect.Play();

        if (_health <= 0)
            Destroy(gameObject);
    }

    private void Move()
    {
        float animationCorrect = 0;
        float horizontal = Input.GetAxis(AxisHorizontal) * Time.deltaTime;
        transform.Translate(horizontal * _speed, 0, 0);

        if (horizontal != 0)
            animationCorrect = 1;

        _animator.SetFloat(AnimatorThief.Parametrs.Speed, animationCorrect);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector2.up * _force);
            _animator.SetTrigger(AnimatorThief.Parametrs.Jump);
        }
    }
}
