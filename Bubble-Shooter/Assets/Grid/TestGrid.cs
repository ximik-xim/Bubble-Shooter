using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestGrid : MonoBehaviour
{ 
    private Entity[,] _sizeGrid;
   [SerializeField] private AvtoSizeSpawnPoint _avtoSizeSpawnPoint;
   [SerializeField] private GridElement _gridElement;
   [SerializeField] private TestReaction _reaction;//Подумать как сделать лтуше(не хочу делать статическим класс TestReaction, т.к он станет все доступен)!
   
  [SerializeField]  private int sizeGridX;
  [SerializeField]  private int sizeGridY;
    void Start()
    {
        sizeGridX = (int)_avtoSizeSpawnPoint.FildSize.x;//проверить а то мож проебываюсь т.к кол-во может быть на 1 больше
        sizeGridY = (int) _avtoSizeSpawnPoint.FildSize.y;

        _sizeGrid = new Entity[sizeGridY, sizeGridX];

        
        CreatGird();
    }


    private void CreatGird()
    {
        for (int y = 0; y < sizeGridY; y++)
        {
            for (int x = 0; x < sizeGridX; x++)
            {
                var Element =  Instantiate(_gridElement, _avtoSizeSpawnPoint._centrPoint[y][x], Quaternion.identity);
                Element.transform.parent = this.transform;
                Element.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(_avtoSizeSpawnPoint.Width, _avtoSizeSpawnPoint.Height);
                Element.Create(new Vector2(x, y), this);
                Element.name = "Grid Element[x " + x + ", y " + y + "]";
            }
        }
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
        int lineCheck = sizeGridY - countLine;
        Debug.Log(lineCheck);
        for (int x = 0; x < sizeGridX; x++)
        {
            if (_sizeGrid[lineCheck, x] != null) 
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

        Debug.LogError("ОШИБКА НЕ НАШЕЛ ПУСТОЙ ХЕРНИ");
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
                _sizeGrid[startLineOffset, x].transform.position = _avtoSizeSpawnPoint._centrPoint[startLineOffset][x];//******
            }
    
            startLineOffset++;
        }
    }

    private void Add(List<List<Entity>> entity)
    {
        for (int y = 0; y < entity.Count; y++)
        {
            for (int x = 0; x < sizeGridX; x++)
            {
                _sizeGrid[y, x] = entity[y][x];
                _sizeGrid[y, x].transform.position=_avtoSizeSpawnPoint._centrPoint[y][x];
            }
        }
    }
    
    public int GetSizeLine()
    {
        return (int) _avtoSizeSpawnPoint.FildSize.x;
    }

 
    public void Reaction(Entity entity,Vector2 startSearchPostition,TypeSearch typeSearch,TypeActionOnEntity typeActionOnEntity)
    {
        //почти все работает, осталось лишь или сделать статическим клсс TestReaction или ещё что то придумать
        //а ну ещё придумать че делать с шаром после столкновения
        //и продумать как проеврять то что мы столкнулись с пустым коллайдером или нет(думаю просто тупо проверку сделать сдесь на то что есть ли тут обьект(Entity) или нет)
        _reaction.Reaction(entity, startSearchPostition, typeSearch, typeActionOnEntity, _sizeGrid);
    }
    
    public bool ThereEntity(Vector2 position)
    {// а тут не все так просто, нужно как то учесть будет ещё напровление(откуда прилетел Enitity(шар))
        if (_sizeGrid[(int) position.y, (int) position.x] != null) 
        {
            return true;
        }

        return false;
    }

    public void OcupationCell(Vector2 positionEntity, Vector2 positionElement, Vector2 ppp,Entity entity)
    {
        Vector2 direction = positionEntity - positionElement;
        float angale = Vector2.SignedAngle(direction, Vector2.up);
        Vector2 tes = Ocupation(angale, (int) ppp.y);
        Vector2 ttt = tes + ppp;
        Debug.Log(ttt +"  "+ angale);
        int y = (int)ttt.x;
        int x = (int)ttt.y;

        Debug.Log(positionElement.y);
        if (_sizeGrid[x,y] != null)
        {
            Debug.LogError("Ошибка на позиции есть обьект");
        }
        //может потом сюда твины сдлеаю
        _sizeGrid[x,y]=entity;
        entity.transform.position = _avtoSizeSpawnPoint._centrPoint[x][y];
    }
    
    private Vector2 Ocupation(float angale,int namberLine)
    {
        int coficentOffset = 0;

        if (namberLine % 2 !=0)
        {
            coficentOffset = 1;
        }
        
        if (0 < angale & angale <= 45)
        {
            return new Vector2(1 - coficentOffset, -1);
        }
        
        if (45 < angale & angale <= 135)
        {
            return new Vector2(1, 0);
        }
        
        if (135 < angale & angale < 180)
        {
            return new Vector2(1 - coficentOffset, +1);
        }
        
        if (0 > angale & angale >= -45) 
        {
            return new Vector2(0 - coficentOffset, -1);
        }
        
        if (-45 > angale & angale >= -135) 
        {
            return new Vector2(-1, 0);
        }
        
        if (-135 > angale & angale > -180)
        {
            return new Vector2(0 - coficentOffset, 1);
        }
        
        if (angale == 0)
        {
            int y = Random.Range(0 - coficentOffset, 1 - coficentOffset);
            return new Vector2(y, -1);
        }
        
        if (angale == 180)
        {
            int y = Random.Range(0 - coficentOffset, 1 - coficentOffset);
            return new Vector2(y, 1);
        }

        Debug.LogError("УГОЛ НЕ НАЙДЕН");
        return Vector2.zero;
    }
}
