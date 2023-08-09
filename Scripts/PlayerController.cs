using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public const string AxisHorizontal = "Horizontal";
    public const string AxisVertical = "Vertical";

    [SerializeField] private float _speed;
    [SerializeField] private float _turingSpeed;
    [SerializeField] private float _force;
    [SerializeField] private int _health;

    private Rigidbody _rigidbody;
    private Animator _animator;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    public void TakeDamage(int damage)
    {
        if (_health > 1)
            _health -= damage;
        else
            Destroy(gameObject);
    }

    private void Move()
    {
        float horizontal = Input.GetAxis(AxisHorizontal) * _turingSpeed * Time.deltaTime;
        float vertical = Input.GetAxis(AxisVertical) * _speed * Time.deltaTime;
        float animationMove = 0;
        transform.Rotate(0,horizontal,0);
        transform.Translate(0, 0, vertical);

        if (horizontal != 0 || vertical != 0)
        {
            animationMove = 1;
            _animator.SetFloat(AnimatorPlayer.States.Move, animationMove);
        }
        else
            _animator.SetFloat(AnimatorPlayer.States.Move, animationMove);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector3.up*_force, ForceMode.Impulse);
        }
    }
}
