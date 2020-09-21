using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private Rigidbody rb;

    [Header("Jump")]
    public float jumpForce = 2.0f;
    public bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        RecPlay.Instance.IniGame();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

        if (Input.GetAxis("Jump") == 1 && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    private void OnCollisionStay(Collision other) 
    {
        if (other.gameObject.CompareTag("ground"))
        {
            isGrounded = true;   
        }
    }
}
