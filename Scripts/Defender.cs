using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Transform _pointSpawnProjectile;
    [SerializeField] private float _pause;
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed;

    private ThiefController _goal;
    private int _pointCurrent;

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<ThiefController>(out ThiefController thief))
        {
            if (_goal == null)
                _goal = thief;

            StartCoroutine(SecurityProtocol());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<ThiefController>(out ThiefController thief))
            if (_goal == thief)
                _goal = null;

        StopCoroutine(SecurityProtocol());
    }

    public IEnumerator SecurityProtocol()
    {
        while (_goal != null)
        {
            GameObject.Instantiate(_projectile, _pointSpawnProjectile.position, Quaternion.identity);
            yield return new WaitForSeconds(_pause);
        }
    }

    private void Move()
    {
        Transform target = _points[_pointCurrent];

        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (transform.position == target.position)
        {
            _pointCurrent++;

            if (_pointCurrent >= _points.Length)
                _pointCurrent = 0;
        }
    }
}
