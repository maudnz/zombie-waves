
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 6f;
    public float mouseSensitivity = 100f;
    public float jumpHeight = 2f;

    
    private float gravity = -9.81f;
    private float xRotation = 0f;
    private Vector3 velocity; // Memorizza la velocità verticale per la gravità
    private bool isGrounded;

    private Camera cameraChild;
    private CharacterController controller;

    

    void Start()
    {
        // Blocca e nasconde il cursore al centro dello schermo
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cameraChild = GetComponentInChildren<Camera>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // gestione gravità: CharacterController ha una proprietà nativa per sapere se il player tocca il terreno
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            //  piccola forza negativa costante per tenere il player "incollato" al terreno
            velocity.y = -2f;
        }



        // gestione movimento orizzontale (WASD)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);



        // gestione salt: salta solo se preme spazio E il giocatore si trova effettivamente a terra
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Formula fisica standard per calcolare la spinta verso l'alto necessaria
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Applichiamo la gravità nel tempo all'asse Y
        velocity.y += gravity * Time.deltaTime;

        // Muoviamo il controller verticalmente (Gravità + Salto)
        controller.Move(velocity * Time.deltaTime);



        // gestione mouse (visuale)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ferma la rotazione verticale (sull'asse x) a 90°

        cameraChild.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }


}
