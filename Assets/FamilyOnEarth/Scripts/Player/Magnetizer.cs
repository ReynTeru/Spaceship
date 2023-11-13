using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetizer : MonoBehaviour
{
    EMovementState CurrentMovementState;
    [SerializeField] GameObject MagnetPrefab;
    [SerializeField] float MagnetShootForce = 10.0f;
    [SerializeField] GameObject MagnetSpawnPoint;
    [SerializeField] float MagnetPower = 1.0f;
    [SerializeField] AnimationCurve AttractionCurve;
    [SerializeField] AnimationCurve RepulsionCurve;
    
    GameObject CurrentMagnet;
    bool bMagnetIsAttracting = true;
    // Start is called before the first frame update
    void Start()
    {
        CurrentMagnet = Instantiate(MagnetPrefab, MagnetSpawnPoint.transform.position, Quaternion.identity);
        CurrentMagnet.GetComponent<Magnet>().Recall();
        CurrentMagnet.transform.parent = MagnetSpawnPoint.transform;
        SwitchMagnetVisibility();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentMovementState = GetComponent<PlayerMovement>().GetMovementState();
        if (CurrentMovementState == EMovementState.GravityLess && CurrentMagnet.GetComponent<Magnet>().bIsStuck)
        {
            Vector3 direction = bMagnetIsAttracting ? CurrentMagnet.transform.position - transform.position : transform.position - CurrentMagnet.transform.position;
            float distance = direction.magnitude;
            float DistanceFactor = Mathf.Clamp(distance / 5f, 0.0f, 1.0f);
            DistanceFactor = bMagnetIsAttracting ? DistanceFactor : 1.0f - DistanceFactor;
            float ForceFactor = bMagnetIsAttracting ? AttractionCurve.Evaluate(DistanceFactor) : RepulsionCurve.Evaluate(DistanceFactor);
           // Debug.Log(DistanceFactor);
            gameObject.GetComponent<Rigidbody>().AddForce(direction.normalized * MagnetPower * ForceFactor);
        }
    }
    
    public void ShootMagnet()
    {
        if (CurrentMovementState == EMovementState.GravityLess && CurrentMagnet.GetComponent<Magnet>().bIsWithPlayer)
        {
            CurrentMagnet.transform.parent = null;
            CurrentMagnet.GetComponent<Magnet>().ForceDirection = MagnetSpawnPoint.transform.forward;
            CurrentMagnet.GetComponent<Magnet>().ForceMagnitude = MagnetShootForce;
            CurrentMagnet.GetComponent<Magnet>().Shoot();
        }
        else if (CurrentMovementState == EMovementState.GravityLess && !CurrentMagnet.GetComponent<Magnet>().bIsWithPlayer)
        {
            RecallMagnet();
        }
    }
    
    public void RecallMagnet()
    {
        if (CurrentMovementState == EMovementState.GravityLess && !CurrentMagnet.GetComponent<Magnet>().bIsWithPlayer)
        {
            CurrentMagnet.GetComponent<Magnet>().Recall();
            CurrentMagnet.transform.position = MagnetSpawnPoint.transform.position;
            CurrentMagnet.transform.rotation = Quaternion.LookRotation(MagnetSpawnPoint.transform.forward, MagnetSpawnPoint.transform.up);
            CurrentMagnet.transform.parent = MagnetSpawnPoint.transform;
        }
    }

    public void SwitchPolarity()
    {      
        if (CurrentMovementState == EMovementState.GravityLess)
        {
            bMagnetIsAttracting = !bMagnetIsAttracting;
            CurrentMagnet.GetComponent<Magnet>().SwitchColor(bMagnetIsAttracting);
        }
    }

    public void SwitchMagnetVisibility()
    {
        CurrentMagnet.GetComponent<MeshRenderer>().enabled = !CurrentMagnet.GetComponent<MeshRenderer>().enabled;
    }
}
