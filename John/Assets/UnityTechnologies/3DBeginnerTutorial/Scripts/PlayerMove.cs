using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerMove : MonoBehaviour
{
    public float turnSpeed = 20f;

    private Animator _animator;
    private Rigidbody _rigidbody;

    private float _horizontal;
    private float _vertical;

    private Vector3 _moveDir;
    private Quaternion _targetRotation = Quaternion.identity;
    private bool _isWalking = false;


    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        TryMove();
    }

    private void TryMove()
    {
        bool walking = !Mathf.Approximately(_horizontal, 0F) || !Mathf.Approximately(_vertical, 0F);
        if (!walking)
        {
            if (_isWalking)
            {
                _animator.SetBool("IsWalking", false);
            }

            _isWalking = false;
            return;
        }

        if (!_isWalking)
        {
            _animator.SetBool("IsWalking", true);
        }

        _isWalking = true;
        
        _moveDir.Set(_horizontal, 0f, _vertical);
        _moveDir.Normalize();

        Vector3 targetRotate = Vector3.RotateTowards(transform.forward, _moveDir, turnSpeed * Time.deltaTime, 0f);
        _targetRotation = Quaternion.LookRotation(targetRotate);
    }

    private void OnAnimatorMove()
    {
        _rigidbody.MovePosition(_moveDir * _animator.deltaPosition.magnitude);
        _rigidbody.MoveRotation(_targetRotation);
    }
}