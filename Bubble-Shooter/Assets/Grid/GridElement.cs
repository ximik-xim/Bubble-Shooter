using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridElement : MonoBehaviour,IGetGridPosition
{
   [SerializeField] private Vector2 _vector2;
    private TestGrid _testGrid;

    public void Create(Vector2 elementGridPosition, TestGrid testGrid)
    {
        _vector2 = elementGridPosition;
        _testGrid = testGrid;
    }

    public void Reaction(Entity entity,TypeSearch typeSearch,TypeActionOnEntity typeActionOnEntity)
    {
        _testGrid.Reaction(entity, _vector2, typeSearch, typeActionOnEntity);
    }

    public void OcupationCell(Entity entity)
    {
        // Vector2 direction = positionEntity - (Vector2) gameObject.transform.position;
        // float angale = Vector2.SignedAngle(direction, Vector2.up);
        // Ocupation(angale);
        Debug.Log(gameObject.name);
        Vector2 positionEntity = entity.transform.position;
        _testGrid.OcupationCell(positionEntity, transform.position, _vector2, entity);
        //Debug.Log(gameObject.name +" "+ angale +" "+ gameObject.transform.position);
    }

   
    public bool ThereEntity()
    {
        return _testGrid.ThereEntity(_vector2);
    }
    
    public Vector2 GetGridPostion()
    {
        return _vector2;
    }
}
