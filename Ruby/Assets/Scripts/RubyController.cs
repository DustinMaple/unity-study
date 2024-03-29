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
    public Projectile projectile;

    private Rigidbody2D _rigidbody2D;

    private float _horizontal;

    private float _vertical;

    private float _invisibTime = 0;
    private bool _invisibling;
    private Animator _animator;
    private bool _moving = false;
    private Vector2 _look;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        curHp = maxHp;
        _animator = GetComponent<Animator>();
        _look = Vector2.down;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMove();
        TickInvisible();
        TryFire();
    }

    private void TryFire()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Fire();
        }
    }

    private void Fire()
    {
        Projectile fireProjectile = Instantiate(projectile, (Vector2)transform.position + Vector2.up * 0.5F, Quaternion.identity);
        fireProjectile.fire(_look);
        _animator.SetTrigger("Launch");
    }

    private void CheckMove()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
    }

    private void TickInvisible()
    {
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
        TryMove();
    }

    private void TryMove()
    {
        if (Mathf.Approximately(_horizontal, 0F) && Mathf.Approximately(_vertical, 0F))
        {
            if (_moving)
            {
                Stop();
            }
            
            return;
        }

        Move();
    }

    private void Move()
    {
        Vector2 cur = transform.position;
        Vector2 moveTo = new Vector2(cur.x + speed * _horizontal * Time.deltaTime,
            cur.y + speed * _vertical * Time.deltaTime);
        _look = moveTo - cur;
        _look.Normalize();

        _rigidbody2D.position = moveTo;
        _moving = true;

        _animator.SetFloat("Look X", _look.x);
        _animator.SetFloat("Look Y", _look.y);
        _animator.SetFloat("Speed", moveTo.magnitude);
    }

    private void Stop()
    {
        // stop
        _animator.SetFloat("Speed", 0F);
    }

    public void ChangeHp(int value)
    {
        if (!CanChange(value))
        {
            return;
        }

        curHp = Mathf.Clamp(curHp + value, 0, maxHp);
        UIHealthBar.Inst.SetRate(curHp * 1f / maxHp);

        if (value < 0)
        {
            if (invisibleConfig > 0)
            {
                _invisibTime = invisibleConfig;
                _invisibling = true;
            }
            
            _animator.SetTrigger("Hit");
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
