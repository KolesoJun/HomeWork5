using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _points;
    [SerializeField] private Enemy _prefab;
    [SerializeField] private float _pauseSpawn;

    private WaitForSeconds _timePause;
    private Coroutine _coroutineSpawn;

    private void Start()
    {
        _timePause = new WaitForSeconds(_pauseSpawn);
        _coroutineSpawn = StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        foreach (SpawnPoint point in _points)
        {
            Instantiate(_prefab, point.transform.position, Quaternion.identity);
            yield return _timePause;
        }
    }
}
