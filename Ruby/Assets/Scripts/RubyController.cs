using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{

    public float speed = 5F;
    public int curHp;
    public int maxHp = 5;
    public float invisibleConfig = 0;

    private Rigidbody2D _rigidbody2D;

    private float _horizontal;

    private float _vertical;

    private float _invisibTime = 0;
    private bool _invisibling;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        if (_invisibTime > 0)
        {
            _invisibTime -= Time.deltaTime;
            if (_invisibTime <= 0)
            {
                _invisibling = false;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 cur = transform.position;
        cur.x += speed * _horizontal * Time.deltaTime;
        cur.y += speed * _vertical * Time.deltaTime;
        
        _rigidbody2D.position = cur;
    }

    public void ChangeHp(int value)
    {
        if (!CanChange(value))
        {
            return;
        }

        curHp = Mathf.Clamp(curHp + value, 0, maxHp);

        if (invisibleConfig > 0)
        {
            _invisibTime = invisibleConfig;
            _invisibling = true;
        }
        
        Debug.Log($"Ruby change hp, change value[{value}], cur hp [{curHp}]");
    }

    public bool CanChange(int value)
    {
        if (value < 0)
        {
            return !_invisibling && curHp > 0;
        }
        else
        {
            return curHp < maxHp;
        }
    }
}
