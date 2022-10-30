using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestReaction : MonoBehaviour
{
    [SerializeField] private Eb<TypeSearch, SearchEntity > _listSearch;
    [SerializeField] private Eb<TypeActionOnEntity,ActionOnEntity > _listAction;

    public void Reaction(Entity entityCollision, Vector2 startSearchPostition, TypeSearch typeSearch, TypeActionOnEntity typeActionOnEntity, Entity[,] grid)
    {
        var listDetectEntity = _listSearch[typeSearch].Search(entityCollision, startSearchPostition, grid);
        _listAction[typeActionOnEntity].Action(listDetectEntity, typeActionOnEntity);
    }
}
