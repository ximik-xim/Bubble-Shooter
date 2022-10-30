using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun : MonoBehaviour
{
    [SerializeField]
    private List<Entity> fireEntity;

    public void Fire(Vector2 forfawd)
    {
        //написать завтра логику для выстрела под углом
    }

    private void Start()
    {
        TestFire();
    }

    private void TestFire()
    {
        Entity entity = fireEntity[0];

        BoxCollider2D boxCollider2D = entity.gameObject.AddComponent<BoxCollider2D>();
        Rigidbody2D rigidbody2D = entity.gameObject.AddComponent<Rigidbody2D>();
        TestWrapper testWrapper = entity.gameObject.AddComponent<TestWrapper>();

        testWrapper.Creat(rigidbody2D, boxCollider2D);
        rigidbody2D.gravityScale = 0;

        entity.gameObject.transform.position = transform.position;
        rigidbody2D.AddForce(Vector2.up * 40);
    }
}
