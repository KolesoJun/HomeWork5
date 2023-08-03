using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BuoyancyEffector2D))]
public class PullPoint : MonoBehaviour
{
    private BuoyancyEffector2D _buoyancyEffector;
    private float _duration = -1f;
    private float _magnitude = 0.6f;

    private void Awake()
    {
        _buoyancyEffector = GetComponentInParent<BuoyancyEffector2D>();
        _buoyancyEffector.flowMagnitude = _magnitude;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Duck>(out _))
        {
            _buoyancyEffector.flowMagnitude *= _duration;
        }
    }
}
