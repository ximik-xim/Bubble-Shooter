using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public abstract class Fabric<K, T> : TestFab where T : Entity where K : Enum 
{
 //   [SerializeField] protected int _countLine;
    [SerializeField] protected TestGrid _testGrid;
    [SerializeField] protected Eb<K, T> _containerEntity;

    protected Pool<T> _pool = new Pool<T>();

    protected void Start()
    {
        _containerEntity.Initialization();
        
        // if (_countLine > _testGrid.GetSizeLine())
        // {
        //     _countLine = _testGrid.GetSizeLine();
        // }
    }

    protected virtual List<List<Entity>> Create(int countLine)
    {
        List<List<Entity>> list = new List<List<Entity>>();
        for (int i = 0; i < countLine; i++)
        {
            list.Add(new List<Entity>());
            for (int j = 0; j < _testGrid.GetSizeLine(); j++)
            {
                T prefabEntity = RandomTypeEnum();
                    
                Entity entity = InstantiatePrefabe(prefabEntity);

                list[i].Add(entity);
            }
        }
    
        return list;
    }

    private T RandomTypeEnum()
    {
        int countElementType = Enum.GetNames(typeof(K)).Length;
        int idType = Random.Range(1, countElementType); //Рандом с 1 т.к 0 знач всегда должно быть None в enum
        K type = (K)Enum.GetValues(typeof(K)).GetValue(idType);
        T prefabEntity = _containerEntity[type];
        
        return prefabEntity;
    }

    private Entity InstantiatePrefabe(T prefabEntity)
    {
        Entity entity = Instantiate(prefabEntity, Vector3.zero, Quaternion.identity);
        entity.transform.parent = transform;
        entity.gameObject.SetActive(true);
        _pool.EnableObject.Add((T) entity);
        entity.CreateEntity(UpdatePool);
        
        return entity;
    }
    
    // public virtual void CCreate(int countLine)
    // {
    //     _testGrid.AddEntity(Create(countLine)); 
    // }
    public override void Spawn(int countLine)
    {
        _testGrid.AddEntity(Create(countLine));
    }

    protected void UpdatePool(bool activeObject,MonoBehaviour monoBehaviour)
    {
        T entity = monoBehaviour as T;  
       
        if (activeObject == true)
        {
            _pool.DisavleObject.Remove(entity);
            _pool.EnableObject.Add(entity);
            return;
        }
       
        _pool.EnableObject.Remove(entity);
        _pool.DisavleObject.Add(entity);
    }
}
