using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar Inst { get; private set; }
    public Image mask;
    
    private float _originWidth;
    
    private void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _originWidth = mask.rectTransform.rect.width;
    }

    public void SetRate(float rate)
    {
        if (mask == null)
        {
            Debug.LogError("mask is null");
            return;
        }
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _originWidth * rate);
        
    }
}
