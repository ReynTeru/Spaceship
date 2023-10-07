using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 pushDirection = other.GetContact(0).normal;
            gameObject.GetComponent<Rigidbody>().AddForce(pushDirection * other.gameObject.GetComponent<PlayerMovement>().PushForce, ForceMode.Impulse);
            Debug.Log("Pushed");
        }
    }
}
