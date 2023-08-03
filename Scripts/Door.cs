using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent (typeof(Rigidbody2D))]
public class Door : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float _power;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.AddForce(Vector2.right* _power, ForceMode2D.Impulse);
    }
}
