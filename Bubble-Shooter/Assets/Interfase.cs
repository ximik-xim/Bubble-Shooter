using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGetGridPosition
{
    public Vector2 GetGridPostion();
}
public interface IGetTypeSpher
{
    public TypeSpher GetType();
}

public interface ISearchEntity
{
    public List<List<Entity>> SearchEntity(Entity entity,Vector2 startSearchPostition,int[,] gridEntity);
}

public interface IActionOnEntity
{
    public void ActionOnEntity(List<List<Entity>> list, TypeActionOnEntity typeActionOnEntity);
}