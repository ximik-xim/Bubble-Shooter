using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWrapper : MonoBehaviour
{
    private Entity _entity;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private bool _collision = false;
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
        if (_collision == false)//подумать над этим
        {
            if (col.gameObject.TryGetComponent<GridElement>(out GridElement tests))
            {
                if (tests.ThereEntity())
                {
                    _collision = true;
                    tests.Reaction(_entity, _entity.TypeSearch, _entity.TypeAction);

                    if (_entity.OccupyCell)
                    {
                        tests.OcupationCell(_entity);
                        Destroy(this);
                        Destroy(_rigidbody);
                        Destroy(_collider);
                        return;
                    }

                    this.gameObject.SetActive(false);
                }

            }

        }
    }

    private void OnDisable()
    {
        
    }
}
