using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvtoSizeSpawnPoint : MonoBehaviour
{
    public Vector2 FildSize
    {
        get => _fieldSize;
    }
    
   [SerializeField] private Vector2 _fieldSize;
   [SerializeField] private List<Vector2> _centrPoint;

    [SerializeField] private float _height;
    [SerializeField] private float _width;

    [Range(0,10)]
    [SerializeField] private float _indentationSize;

    private void OnDrawGizmos()
    {
        _centrPoint = new List<Vector2>();
        
        Vector3 scaleWindow = transform.localScale;

        float sizeCubeX = _width + _indentationSize;
        float sizeCubeY = _height + _indentationSize;

        int countCubeX = (int) ((scaleWindow.x + _indentationSize - _width / 2) / sizeCubeX);
        int countCubeY = (int) ((scaleWindow.y + _indentationSize) / sizeCubeY);

        float currentCenterPositionX = transform.localPosition.x - transform.localScale.x / 2 + _width / 2;
        float currentCenterPositionY = transform.localPosition.y + transform.localScale.y / 2 - _height / 2;

        Gizmos.color = Color.blue;
        
        for (int j = 0; j < countCubeY; j++)
        {
            currentCenterPositionX = transform.localPosition.x - transform.localScale.x / 2 + _width / 2;
            if (j % 2 == 0)
            {
                currentCenterPositionX += _width / 2;
            }
            
            for (int i = 0; i < countCubeX; i++)
            {
                Gizmos.DrawWireCube(new Vector2(currentCenterPositionX, currentCenterPositionY), new Vector2(_width, _height));
                _centrPoint.Add(new Vector2(currentCenterPositionX, currentCenterPositionY));
                currentCenterPositionX += _width + _indentationSize;
            }
            
            currentCenterPositionY -= _height + _indentationSize;
        }

        _fieldSize.x = countCubeX;
        _fieldSize.y = countCubeY;

        Gizmos.color=Color.cyan;
        Gizmos.DrawWireCube( transform.position,transform.localScale);
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.blue;
    //     foreach (var VARIABLE in _centrPoint)
    //     {
    //         Gizmos.DrawWireCube(new Vector2(VARIABLE.x, VARIABLE.y), new Vector2(_width, _height));    
    //     }
    //     
    // }
}
