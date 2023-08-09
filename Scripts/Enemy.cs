using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    private Scaner _scaner;
    private Transform _target;
    private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _scaner = GetComponentInChildren<Scaner>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
            player.TakeDamage(_damage);

        if (collision.gameObject.TryGetComponent<EnemyPathPoint>(out _) && collision.transform == _target)
        {
            _target = null;
        }  
    }

    private void Update()
    {
        Move();
    }

    public void GetTarget()
    {
        if (_scaner.Player != null)
        {
            _target = _scaner.Player.transform;
        }
        else
        {
            ChangeTarget();
        }
    }

    private void Move()
    {
        if (_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
        }
        else
            GetTarget();
    }

    private void ChangeTarget()
    {
        if (_target != null)
        {
            if (_target.TryGetComponent<PlayerController>(out _) && _scaner.Player == null)
            {
                _target = null;
            }
        }

        EnemyPathPoint point = _scaner.GetPoint();

        if (_target == null && point != null)
        {
            _target = point.transform;
        }
    }
}
