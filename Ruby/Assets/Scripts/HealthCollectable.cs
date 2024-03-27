using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    public int changeValue = 1;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log($"{col.gameObject.name} enter");
        
        RubyController rubyController = col.GetComponent<RubyController>();
        if (rubyController == null)
        {
            return;
        }

        if (!rubyController.CanChange(changeValue))
        {
            return;
        }

        rubyController.ChangeHp(changeValue);
        Destroy(gameObject);

    }
}
