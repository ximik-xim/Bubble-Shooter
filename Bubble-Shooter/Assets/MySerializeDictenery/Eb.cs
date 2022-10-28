using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Eb<K,D>:Dictionary<K,D>
{
    [SerializeField]
    private List<Et<K, D>> _listKeyAndData;

    public void Initialization()
    {
        for (int i = 0; i < _listKeyAndData.Count; i++)
        {
            this.Add(_listKeyAndData[i]._k,_listKeyAndData[i]._d);    
        }
        //пока просто очищаю, но потом можно будет прямое взаимодействие захерачить, ноооо как то ща в падлу
        _listKeyAndData.Clear();
    }
}

[System.Serializable]
public class Et<K, D>
{
    public K _k;
    public D _d;
}