using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public bool bIsStuck = false;
    public bool bIsWithPlayer = true;
    public Vector3 ForceDirection;
    public float ForceMagnitude = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Recall()
    {
        bIsStuck = false;
        bIsWithPlayer = true;
        GetComponent<Rigidbody>().isKinematic = true;
        //GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
    }
    
    public void Shoot()
    {
        bIsStuck = false;
        bIsWithPlayer = false;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().AddForce(ForceDirection * ForceMagnitude, ForceMode.Impulse);
    }
    
    public void Stick()
    {
        bIsStuck = true;
        bIsWithPlayer = false;
        GetComponent<Rigidbody>().isKinematic = true;
        //GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
    }
    
    public void SwitchColor(bool bPole)
    {
        GetComponent<MeshRenderer>().material.color = bPole ? Color.blue : Color.red;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!bIsStuck && !bIsWithPlayer && !other.gameObject.CompareTag("Player"))
        {
            Stick();
        }
    }
}
