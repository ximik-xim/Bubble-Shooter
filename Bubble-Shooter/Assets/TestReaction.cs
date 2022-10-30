using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestReaction : MonoBehaviour
{
    [SerializeField] private TestGrid _testGrid;

    [SerializeField] private Eb<TypeSearch, SearchEntity > _listSearch;
    [SerializeField] private Eb<TypeActionOnEntity,ActionOnEntity > _listAction;


    private Entity[,] _a;
    
    public void SetArray(Entity[,] a)
    {
        _a = a;
    }
    

    private void Start()
    {
        
        
    }

    public void Reaction(Entity entityCollision, Vector2 startSearchPostition, TypeSearch typeSearch, TypeActionOnEntity typeActionOnEntity)
    {
    var listDetectEntity = _listSearch[typeSearch].Search(entityCollision, startSearchPostition, _a);
    _listAction[typeActionOnEntity].Action(listDetectEntity,typeActionOnEntity);
    }
}
