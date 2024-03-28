using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //最大修复次数
    public int maxFixTime = 1;
    
    private bool _fixFinish = false;
    private int _fixTime = 0;

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
        
    }

    public void Fix()
    {
        _fixTime++;

        if (_fixTime >= Mathf.Max(1, maxFixTime))
        {
            _fixFinish = true;

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
