using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    
    private Rigidbody rb;
    private float movementX;
    private int count;
    private float movementY;
    private Camera playerCamera; // Referencia a la cámara
    private int speedcount;
    private Animator animator;
    
    public int GetCoinCount()
    {
        return count;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main; // Asigna la cámara principal (o la cámara que estás usando)
        count = 0;
        SetCountText();
    }

    void OnMove(InputValue moveValue)
    {
        Vector2 movementVector = moveValue.Get<Vector2>();

        // Obtén la dirección de la cámara
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;

        // Elimina la componente y para que solo obtengamos el plano horizontal
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Normaliza los vectores para que tengan longitud 1
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calcula la dirección de movimiento en función de la cámara
        Vector3 desiredMoveDirection = cameraForward * movementVector.y + cameraRight * movementVector.x;

        movementX = desiredMoveDirection.x;
        movementY = desiredMoveDirection.z;
    }
    
    void SetCountText() 
    {
        countText.text =  "Count: " + count.ToString();
    }

    void OnFire()
    {
        Debug.Log("Hola, soy un mensaje de debug en OnFire");
        rb.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        
        rb.AddForce(movement * speed);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnFire();
        }
    }
    
    void OnTriggerEnter (Collider other) 
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            
            Destroy(other.gameObject);
            count = count + 1;
            if (count % 5 == 0)
            {
                count *= 2;
            }
            SetCountText();
            speed *= 1.5f;
           
        }
    }

    
    
    

    void Update()
    {
        
    }
}