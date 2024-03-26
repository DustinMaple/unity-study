using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{

    public float speed = 5F;

    private Rigidbody2D Rigidbody2D;

    private float horizontal;

    private float vertical;

    private int curHp;
    private int maxHp;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Vector2 cur = transform.position;
        cur.x += speed * horizontal * Time.deltaTime;
        cur.y += speed * vertical * Time.deltaTime;
        
        Rigidbody2D.position = cur;
    }

    public void ChangeHp(int value)
    {
        curHp = Mathf.Clamp(curHp + value, 0, maxHp);
    }
}
