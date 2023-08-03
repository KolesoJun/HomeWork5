using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    private const float VolueMax = 1f;
    private const float VolueMin = 0f;

    [SerializeField] private float _step;
    [SerializeField] private float _setback;

    private WaitForSeconds _timePause;
    private AudioSource _audio;
    private float _volue;
    private bool _isWorking;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _timePause = new WaitForSeconds(_setback);
    }

    public void AnxietyWork(int countThiefs)
    {
        if (_isWorking == false && countThiefs >= 1)
        {
            _isWorking = true;
            _volue = VolueMax;
            _audio.Play();
        }

        if (countThiefs <= 0)
        {
            _volue = VolueMin;
            _isWorking = false;
        }

        StartCoroutine(ChangeVolue());

        if (_audio.volume == VolueMin)
        {
            StopCoroutine(ChangeVolue());
            _audio.Stop();
        }    
    }

    private IEnumerator ChangeVolue()
    {
        while (_audio.volume != _volue)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, _volue, _step);
            yield return _timePause;
        }
    }
}


