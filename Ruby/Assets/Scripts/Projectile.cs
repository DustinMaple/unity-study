using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float force = 50;
    
    private Rigidbody2D _rigidbody2D;

    private Vector2 _dir;

    private Vector2 _start;
    
    // Start is called before the first frame updated
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (((Vector2)transform.position - _start).magnitude > 20)
        {
            Destroy(gameObject);
        }
    }

    public void fire(Vector2 dir)
    {
        if (_dir != Vector2.zero)
        {
            return;
        }

        _start = transform.position;
        _dir = dir;
        _rigidbody2D.AddForce(dir * Mathf.Max(force, 50f));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject otherGameObject = other.gameObject;
        Debug.Log($"Projectile collide [{otherGameObject.name}]");

        EnemyController enemyController = otherGameObject.GetComponent<EnemyController>();
        if (enemyController!=null)
        {
            enemyController.Fix();
        }
        Destroy(gameObject);
    }
}
