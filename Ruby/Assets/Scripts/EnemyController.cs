using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //最大修复次数
    public int maxFixTime = 1;
    
    private bool _fixFinish = false;
    private int _fixTime = 0;
    private float _exitTime;

    public bool FixFinish
    {
        get => _fixFinish;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_exitTime > 0)
        {
            _exitTime -= Time.deltaTime;
            if (_exitTime < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Fix()
    {
        _fixTime++;

        if (_fixTime >= Mathf.Max(1, maxFixTime))
        {
            _fixFinish = true;
            _exitTime = 2f;
            Animator animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("Fixed");
            }

            BotMove botMove = GetComponent<BotMove>();
            if (botMove != null)
            {
                botMove.enabled = false;
            }

            Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
            if (rigidbody2D != null)
            {
                rigidbody2D.simulated = false;
            }
        }
    }
}
