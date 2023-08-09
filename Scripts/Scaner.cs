using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaner : MonoBehaviour
{
    [SerializeField] private float _sizeMax;
    [SerializeField] private float _growth;

    private List<EnemyPathPoint> _points = new List<EnemyPathPoint>();

    public PlayerController Player { get; private set; }

    private void Update()
    {
        transform.localScale = new Vector3
            (Mathf.MoveTowards(transform.localScale.x, _sizeMax, _growth * Time.deltaTime),
            transform.localScale.y, 
            Mathf.MoveTowards(transform.localScale.z, _sizeMax, _growth * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<EnemyPathPoint>(out EnemyPathPoint pathPoint))
        {
            if (_points.Contains(pathPoint) == false)
            {
                _points.Add(pathPoint);
            }
        }

        if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
        {
            Player = player;
            GetComponentInParent<Enemy>().GetTarget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController _))
        {
            Player = null;
            GetComponentInParent<Enemy>().GetTarget();
        }
    }

    public EnemyPathPoint GetPoint()
    {
        if (_points.Count > 0)
            return _points[Random.Range(0, _points.Count)];
        return null;
    }
}
