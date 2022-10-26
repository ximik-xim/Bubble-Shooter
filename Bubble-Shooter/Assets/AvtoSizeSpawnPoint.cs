using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvtoSizeSpawnPoint : MonoBehaviour
{
    [SerializeField] private float _height;
    [SerializeField] private float _width;

    [Range(0,10)]
    [SerializeField] private float _indentationSize;
    [Range(0,10)]
    [SerializeField] private float _sidesIndentation;

    private void OnDrawGizmos()
    {
        Vector3 scaleWindow = transform.localScale;

        float sizeCubeX = _width + _indentationSize;
        float sizeCubeY = _height + _indentationSize;

        int countCubeX = (int) ((scaleWindow.x + _indentationSize - _sidesIndentation) / sizeCubeX);
        int countCubeY = (int) ((scaleWindow.y + _indentationSize) / sizeCubeY);

        float currentCenterPositionX = transform.localPosition.x - transform.localScale.x / 2 + _width / 2;
        float currentCenterPositionY = transform.localPosition.y + transform.localScale.y / 2 - _height / 2;
        
        Gizmos.color = Color.blue;

        for (int j = 0; j < countCubeY; j++)
        {
            currentCenterPositionX = transform.localPosition.x - transform.localScale.x / 2 + _width / 2;
            if (j % 2 == 0)
            {
                currentCenterPositionX += _sidesIndentation;
            }
            
            for (int i = 0; i < countCubeX; i++)
            {
                Gizmos.DrawWireCube(new Vector2(currentCenterPositionX, currentCenterPositionY), new Vector2(_width, _height));
                currentCenterPositionX += _width + _indentationSize;
            }
            
            currentCenterPositionY -= _height + _indentationSize;
        }
        
    }
}
