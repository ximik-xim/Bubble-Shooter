using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour
{ 
    private Entity[,] _sizeGrid;
   [SerializeField] private AvtoSizeSpawnPoint _avtoSizeSpawnPoint;
   [SerializeField] private static TestReaction _reaction;//переделать!
   
  [SerializeField]  private int sizeGridX;
  [SerializeField]  private int sizeGridY;
    void Start()
    {
        _reaction.SetArray(_sizeGrid);
        
         sizeGridX = (int)_avtoSizeSpawnPoint.FildSize.x;//проверить а то мож проебываюсь т.к кол-во может быть на 1 больше
         sizeGridY = (int)_avtoSizeSpawnPoint.FildSize.y;
        
        _sizeGrid = new Entity[sizeGridX, sizeGridY];
    }

    public void AddEntity(List<List<Entity>> entity)
    {
        //привести в порядок эту херню и дописать логику для смены позиций у обьектов;
        if (entity.Count < sizeGridY)
        {
            if (CheckEmptyLine(entity.Count))
            {
                int lineOccupied = SearchLineOccupied();
                OffsetLine(lineOccupied);
                Add(entity);
            }
            else
            {
                DisactiveLineEntity(entity.Count);
                OffsetLine(entity.Count);   
                Add(entity);
            }
        }
        else if(entity.Count == sizeGridY)
        {
            DisactiveLineEntity(entity.Count);
            Add(entity);
        }
        else
        {
            //попытаться предусматреть эту хуйню в фабрике
            List<List<Entity>> list = new List<List<Entity>>();
            for (int i = 0; i < sizeGridY; i++)
            {
                for (int j = 0; j < sizeGridX; j++)
                {
                    list[i][j] = entity[entity.Count - i][j]; 
                }
            }
            
            DisactiveLineEntity(list.Count);
            Add(list);
        }
        
    }
    
    private bool CheckEmptyLine(int countLine)
    {
        int lineCheck = sizeGridY -= countLine;
        for (int i = 0; i < sizeGridX; i++)
        {
            if (_sizeGrid[lineCheck, i] != null)
            {
                return false;
            }
        }

        return true;
    }
    private int SearchLineOccupied()
    {
        bool thereEntity = false;
        for (int y = 0; y < sizeGridY; y++)
        {
            thereEntity = false;
            for (int x = 0; x < sizeGridX; x++)
            {
                if (_sizeGrid[y, x] != null)
                {
                    thereEntity = true;
                }
            }
        
            if (thereEntity == false)
            {
                return y;
            }
        }

        Debug.Log("ОШИБКА НЕ НАШЕЛ ПУСТОЙ ХЕРНИ");
        return 0;
    }
    private void DisactiveLineEntity(int CountLineDisactive)
    {
        for (int y = 0; y < CountLineDisactive; y++) 
        {
            for (int x = 0; x < sizeGridX; x++)
            {
                if (_sizeGrid[y, x] != null)
                {
                    _sizeGrid[y, x].gameObject.SetActive(false);
                }
            }
        }
    }
    private void OffsetLine(int countLineOffset)
    {
        int startLineOffset = sizeGridY - countLineOffset + 1;
        
        for (int y = 0; y < countLineOffset; y++)
        {
            for (int x = 0; x < sizeGridX; x++)
            {
                _sizeGrid[startLineOffset, x] = _sizeGrid[y, x];
            }
    
            startLineOffset++;
        }
    }

    private void Add(List<List<Entity>> entity)
    {
        for (int i = 0; i < entity.Count; i++)
        {
            for (int j = 0; j < sizeGridX; j++)
            {
                _sizeGrid[i, j] = entity[i][j];
            }
        }
    }

    //int lastLineRemain = sizeGridY - entity.Count;
    // private void DisactiveLineEntity(int startLineDisactive)
    // {
    //     bool thereEntity = false;
    //     for (int i = startLineDisactive; i < sizeGridY; i++) 
    //     {
    //          thereEntity = false;
    //         for (int j = 0; j < sizeGridX; j++)
    //         {
    //             if (_sizeGrid[i, j] != null)
    //             {
    //                 thereEntity = true;
    //                 _sizeGrid[i, j].gameObject.SetActive(false);
    //             }
    //         }
    //
    //         if (thereEntity == false)
    //         {
    //             break;
    //         }
    //     }
    // }

    // private void OffsetLine(int startLineOffset)
    // {
    //     for (int i = sizeGridY-startLineOffset; i < sizeGridY; i++)
    //     {
    //         for (int j = 0; j < sizeGridX; j++)
    //         {
    //             _sizeGrid[i, j] = _sizeGrid[startLineOffset, j];
    //         }
    //
    //         startLineOffset++;
    //     }
    // }
    
    // private int Check(int countLine)
    // {
    //     int lineCheck = sizeGridY -= countLine;
    //     for (int i = 0; i < sizeGridX; i++)
    //     {
    //         if (_sizeGrid[lineCheck, i] != null)
    //         {
    //             //return false;
    //            return ChekClirLine(lineCheck);
    //         }
    //     }
    //
    //     return lineCheck;
    // }
    //
    // private int ChekClirLine(int lastLineCheck)
    // {
    //     lastLineCheck += 1;
    //     bool thereEntity = false;
    //     
    //     for (int i = lastLineCheck; i < sizeGridY; i++)
    //     {
    //         thereEntity = false;
    //         for (int x = 0; x < sizeGridX; x++)
    //         {
    //             if (_sizeGrid[lastLineCheck, x] != null)
    //             {
    //                 thereEntity = true;
    //                 break;
    //             }
    //         }
    //     }
    //
    //     if (thereEntity)
    //     {
    //         return -1;
    //     }
    //
    //     return lastLineCheck;
    // }
    // Update is called once per frame

    public int GetSizeLine()
    {
        return (int) _avtoSizeSpawnPoint.FildSize.x;
    }
}
