using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _life = 1.7f;
    private float _damage = 1f;

    private void Start()
    {
        Destroy(gameObject, _life);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<ThiefController>(out ThiefController thief))
        {
            thief.TakeDamege(_damage);
            Destroy(gameObject);
        }
    }
}
