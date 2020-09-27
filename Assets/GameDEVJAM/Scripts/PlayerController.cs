using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    public Animator CameraAnim;
    public SpriteRenderer characterSprite;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.RUNNING)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            if (moveHorizontal > 0)
                characterSprite.flipX = false;
            else if (moveHorizontal < 0)
                characterSprite.flipX = true;

            Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
            rb.velocity = movement * speed;
        }
    }
}
