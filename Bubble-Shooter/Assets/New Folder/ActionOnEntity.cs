using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public abstract class ActionOnEntity : ScriptableObject
{
    public abstract void Action(List<List<Entity>> list, TypeActionOnEntity typeActionOnEntity);
}
