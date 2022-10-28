using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public abstract class Entity : MonoBehaviour,IGetGridPosition
{
 public Action Dead;
 
 [SerializeField] private Vector2 _gridPosition;
 private event Action<bool, MonoBehaviour> _enable;
 
 public void CreateEntity(Action<bool,MonoBehaviour> action)
 {
  _enable += action;
 }
 
 protected virtual void OnEnable()
 {
  _enable?.Invoke(true,this);
 }

 public Vector2 GetGridPostion()
 {
  return _gridPosition;
 }
 
 protected virtual void OnDisable()
 {
  Dead += delegate { _enable?.Invoke(false, this); };
  Dead.Invoke();
 
 }
 
}
