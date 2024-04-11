using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameEnd : MonoBehaviour
{
    public GameObject player;

    public CanvasGroup successEndUI;
    public CanvasGroup failEndUI;
    public AudioSource winAudioSource;
    public AudioSource caughtAudioSource;


    private float _fadeDuration = 2f;
    private float _displayDuration = 3f;

    private float _timer;
    private bool _isSuccess;
    private bool _isCaught;
    private bool _audioPlaying;


    // Start is called before the first frame update
    void Update()
    {
        if (_isSuccess || _isCaught)
        {
            if (_isSuccess)
            {
                EndGame(successEndUI, true, winAudioSource);
            }
            else
            {
                EndGame(failEndUI, false, caughtAudioSource);
            }
        }
    }

    public void Caught()
    {
        _isCaught = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.gameObject == player && !_isSuccess)
        {
            Debug.Log("GameEnd");
            _isSuccess = true;
            _timer = 0f;
        }
    }

    private void EndGame(CanvasGroup ui, bool success, AudioSource audioSource)
    {
        if (!_audioPlaying)
        {
            _audioPlaying = true;
            audioSource.Play();
        }
        
        _timer += Time.deltaTime;
        if (_timer > _fadeDuration + _displayDuration)
        {
            if (success)
            {
                // Application.Quit();
                UnityEditor.EditorApplication.isPlaying = false;
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            ui.alpha = Mathf.Lerp(0f, 1f, _timer / _fadeDuration);
        }
    }
}