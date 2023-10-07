using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandle : MonoBehaviour
{
    public float physPushForce = 10.0f;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Phys"))
        {
            Vector3 pushDirection = -other.GetContact(0).normal;
            other.gameObject.GetComponent<Rigidbody>().AddForce(pushDirection * physPushForce, ForceMode.Impulse);
            Debug.Log("Pushed");
        }
    }
}
