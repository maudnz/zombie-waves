using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.XR; 

public class GameMode : MonoBehaviour
{
    [Header("Rig")]
    public GameObject rigNormal; 
    public GameObject rigVR;

    [Header("GameManager")]
    public GameObject manNormal;
    public GameObject manVR;


    void Start()
    {
        if (XRSettings.isDeviceActive)
        {
            Debug.Log("VR detected");

            rigNormal.SetActive(false);
            rigVR.SetActive(true);

            manNormal.SetActive(false);
            manVR.SetActive(true);
        }
        else
        {
            Debug.Log("VR not detected");

            rigNormal.SetActive(true);
            rigVR.SetActive(false);

            manNormal.SetActive(true);
            manVR.SetActive(false);
        }
    }
}