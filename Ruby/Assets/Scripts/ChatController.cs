using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChatController : MonoBehaviour
{
    public static ChatController Inst { get; private set; }
    
    public TextMeshProUGUI textMeshPro;

    private int _messageSize;
    private int _index;
    private List<string> _messages;
    private int _maxPage;
    private int _curPage;

    private void Awake()
    {
        Inst = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
        if (!textMeshPro)
        {
            Debug.LogError("ChatUI has no TextMeshPro");
        }
    }

    private void Update()
    {
        _maxPage = textMeshPro.textInfo.pageCount;
        
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (_curPage < _maxPage)
            {
                PageDown();
            }
            else if(_index < _messageSize)
            {
                ShowMessage(_index++);
            }
            else
            {
                Reset();
            }
        }
    }

    private void Reset()
    {
        gameObject.SetActive(false);
        _index = 0;
        _messages = null;
        _maxPage = 0;
        _curPage = 0;
        textMeshPro.text = "";
    }

    private void PageDown()
    {
        textMeshPro.pageToDisplay = ++_curPage;
    }

    public void Show(List<string> messages)
    {
        _messages = messages;
        _messageSize = messages.Count;
        _index = 0;

        gameObject.SetActive(true);

        ShowMessage(_index++);
    }

    private void ShowMessage(int index)
    {
        textMeshPro.SetText(_messages[index]);
        _curPage = 1;
        textMeshPro.pageToDisplay = _curPage;
    }
}
