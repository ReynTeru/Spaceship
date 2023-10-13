using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enablecollision : MonoBehaviour
{
    public GameObject ReflectionCamera;
    // Start is called before the first frame update

    private void Start()
    {
        ReflectionCamera.SetActive(false);
    }

    private void OnTriggerEnter (Collider other)
    {
        Debug.Log("Collided");
        if (other.gameObject.CompareTag("Player"))
        {
            ReflectionCamera.SetActive(true);
        }
    }
}
