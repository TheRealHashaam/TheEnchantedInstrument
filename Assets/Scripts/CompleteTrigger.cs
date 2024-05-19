using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<SnakeCharmer>())
        {
            FindObjectOfType<GameManager>().LevelComplete();
        }
    }
}
