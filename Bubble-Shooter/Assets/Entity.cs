using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour//,IGetGridPosition
{
    public TypeActionOnEntity TypeAction => _typeAction;
    public TypeSearch TypeSearch => _typeSearch;
    public UnityEvent Dead;

    //[SerializeField] private Vector2 _gridPosition;
    [SerializeField] private TypeSearch _typeSearch;
    [SerializeField] private TypeActionOnEntity _typeAction;
    private event Action<bool, MonoBehaviour> _enable;
 
    public void CreateEntity(Action<bool,MonoBehaviour> action)
    {
        _enable += action;
    }
 
    protected virtual void OnEnable()
    {
        _enable?.Invoke(true,this);
    }

    // public Vector2 GetGridPostion()
    // {
    //  return _gridPosition;
    // }
    //
    protected virtual void OnDisable()
    {
        //Dead += delegate { _enable?.Invoke(false, this); };
        Dead.AddListener(() => _enable?.Invoke(false, this));
        Dead.Invoke();
 
    }
 
}
