using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caught : MonoBehaviour
{
    public GameObject player;

    public GameEnd gameEnd;

    private bool _isCaught;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && !_isCaught)
        {
            _isCaught = true;
            Debug.Log("Enter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player && _isCaught)
        {
            _isCaught = false;
            Debug.Log("Exit");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isCaught)
        {
            Vector3 direction = player.transform.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit " + hit.collider.gameObject.name);
                if (hit.collider.gameObject == player)
                {
                    gameEnd.Caught();
                }
            }
            else
            {
                Debug.Log("Hit nothing");
            }
        }
    }
}
