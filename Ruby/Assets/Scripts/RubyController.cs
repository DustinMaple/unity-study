using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{

    public float speed = 5F;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 cur = transform.position;
        cur.x += speed * horizontal * Time.deltaTime;
        cur.y += speed * vertical * Time.deltaTime;
        transform.position = cur;
    }
}
