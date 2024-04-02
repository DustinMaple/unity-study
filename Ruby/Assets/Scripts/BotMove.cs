using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

[RequireComponent(typeof(Rigidbody2D))]
public class BotMove : MonoBehaviour
{

    public List<Vector2> patrolTargets;
    public float speed = 0;
    
    private int _curPatrolIndex = 0;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (patrolTargets == null || patrolTargets.Count == 0)
        {
            return;
        }
        
        Vector2 cur = transform.position;
        Vector2 target = patrolTargets[_curPatrolIndex];

        float dx = target.x - cur.x;
        float dy = target.y - cur.y;
        
        float moveDis = speed * Time.deltaTime;
        float distance = Vector2.Distance(cur, target);
        if (moveDis >= distance)
        {
            _rigidbody2D.MovePosition(target);
            _curPatrolIndex++;

            if (_curPatrolIndex >= patrolTargets.Count)
            {
                _curPatrolIndex = 0;
            }
        }
        else
        {
            float rate = moveDis / distance;
            cur.x += dx * rate;
            cur.y += dy * rate;
            _rigidbody2D.MovePosition(cur);
        }

        PlayAnimator(dx, dy);
    }

    private void PlayAnimator(float dx, float dy)
    {
        if (_animator == null)
        {
            Debug.Log("ERROR! Can't play animation!");
            return;
        }

        float moveX = 0, moveY = 0;

        if (FloatComparer.AreEqual(dx, 0F, 0F))
        {
            moveY = dy < 0 ? -0.5F:0.5F;
        }
        else
        {
            moveX = dx < 0 ? -0.5F : 0.5F;
        }
        
        _animator.SetFloat("MoveX", moveX);
        _animator.SetFloat("MoveY", moveY);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        RubyController rubyController = other.gameObject.GetComponent<RubyController>();
        if (rubyController == null)
        {
            return;
        }
        
        rubyController.ChangeHp(-1);
    }
}
