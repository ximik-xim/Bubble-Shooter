using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWrapper : MonoBehaviour
{
    private Entity _entity;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private void OnEnable()
    {
        _entity = gameObject.GetComponent<Entity>();        
    }

    public void Creat(Rigidbody2D rigidbody, Collider2D collider)
    {
        _rigidbody = rigidbody;
        _collider = collider;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<GridElement>(out GridElement tests)) 
        {
            tests.Reaction(_entity, _entity.TypeSearch, _entity.TypeAction);  
            Destroy(this);
            Destroy(_rigidbody);
            Destroy(_collider);
        }
     

    }

    private void OnDisable()
    {
        
    }
}
