using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public GameObject player;

    public CanvasGroup endUI;
    
    private float _fadeDuration = 2f;
    private float _displayDuration = 3f;
    
    private float _timer;
    private bool _isEnd;
    
    // Start is called before the first frame update
    void Update()
    {
        if (_isEnd)
        {
            EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.gameObject == player && !_isEnd)
        {
            Debug.Log("GameEnd");
            _isEnd = true;
            _timer = 0f;
        }
    }

    private void EndGame()
    {
        _timer += Time.deltaTime;
        if (_timer > _fadeDuration + _displayDuration)
        {
            // Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            endUI.alpha = Mathf.Lerp(0f, 1f, _timer / _fadeDuration);
        }
    }
}
