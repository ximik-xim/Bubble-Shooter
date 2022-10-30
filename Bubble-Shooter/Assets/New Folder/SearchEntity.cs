using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class SearchEntity : ScriptableObject
{
    public abstract List<List<Entity>> Search(Entity entity, Vector2 startSearchPostition, Entity[,] gridEntity);
}
