using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun : MonoBehaviour
{
    [SerializeField]
    private List<Entity> fireEntity;

    [SerializeField]//удалить потом 
    private Pher Test;
    
    
    public void Fire(Vector2 forfawd)
    {
        //написать завтра логику для выстрела под углом
    }

    private void Start()
    {

        StartCoroutine(Tsest());
    }

    private void TestFire()
    {
        //Entity entity = fireEntity[0];
        
        Entity entity = Instantiate(Test,transform.position,Quaternion.identity);

        Collider2D Collider2D = entity.gameObject.AddComponent<CircleCollider2D>();
        Rigidbody2D rigidbody2D = entity.gameObject.AddComponent<Rigidbody2D>();
        TestWrapper testWrapper = entity.gameObject.AddComponent<TestWrapper>();

        testWrapper.Creat(rigidbody2D, Collider2D);
        rigidbody2D.gravityScale = 0;

        entity.gameObject.transform.position = transform.position;
        rigidbody2D.AddForce(Vector2.up * 50);
    }

    IEnumerator Tsest()
    {
        yield return new WaitForSeconds(5);
        TestFire();
        StartCoroutine(Tsest());
    }
}
