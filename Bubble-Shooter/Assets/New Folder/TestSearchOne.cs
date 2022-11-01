using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Reaction/Search/SearchOne")]
public class TestSearchOne : SearchEntity
{
    public override List<List<Entity>> Search(Entity entity, Vector2 startSearchPostition, Entity[,] gridEntity)
    {
        Debug.Log("Тип Начат поиск");
        return new List<List<Entity>>();
    }
}
