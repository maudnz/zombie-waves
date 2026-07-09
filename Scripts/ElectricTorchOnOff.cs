
using UnityEngine;
using UnityEngine.InputSystem;


public class ElectricTorchOnOff : MonoBehaviour
{
    public InputActionProperty vrAction;

    public AudioSource audioSource;
    public AudioClip click;


    private bool flashLightOn = true;
    private float switchInterval = 0f;




    void Update()
    {
        switchInterval += Time.deltaTime;
        if (switchInterval >= 0.5f)
        { 
            if (Input.GetKeyDown(KeyCode.F) || vrAction.action.WasPressedThisFrame())
            {
                inputkey();
                switchInterval = 0f;

            }
        }
    }


    void inputkey()
    {
        audioSource.PlayOneShot(click);

        if (flashLightOn)
        {
            flashLightOn = false;
            GetComponent<Light>().intensity = 0.0f;

        }
        else if (!flashLightOn)
        {
            flashLightOn = true;
            GetComponent<Light>().intensity = 7.0f;

        }
    }

}
