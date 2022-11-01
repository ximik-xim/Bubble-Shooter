using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Reaction/Action/ActionOne")]
public class TestActionOne : ActionOnEntity
{
    public override void Action(List<List<Entity>> list, TypeActionOnEntity typeActionOnEntity)
    {
        Debug.Log("Тип че та произошло");
    }
}
