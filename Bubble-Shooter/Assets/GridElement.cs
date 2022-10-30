using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    public Vector2 GetGridPostion()
    {
        return _vector2;
    }
}
