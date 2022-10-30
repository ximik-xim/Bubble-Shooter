using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{
  [SerializeField] 
  private int countLineSpawn;
  [SerializeField] 
  private int countShootBeforeSpawn;
  [SerializeReference]
  private List<TestFab> _fabric;//надо как то сделать инкальные все фабрики(что бы не дублировались)

  private void Start()
  {
    foreach (var VARIABLE in _fabric)
    {
      //VARIABLE
    }
  }
}
