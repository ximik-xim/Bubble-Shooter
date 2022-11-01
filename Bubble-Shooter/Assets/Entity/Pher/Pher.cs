using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pher : Entity,IGetTypeSpher
{
    [SerializeField] private TypeSpher _typeSpher;
    public TypeSpher GetType()
    {
        return _typeSpher;
    }

    public override bool Equals(object other)//нужно переопределить т.к именно по нему будем проверять одинакове ли элементы
    {
        if (other == null) 
        {
            return false;
        }

        if (other.GetType() != (object)this.GetType())
        {
            return false;
        }

        Pher pher = (Pher) other;
        if (pher._typeSpher != this._typeSpher)
        {
            return false;
        }

        return true;
    }
}
