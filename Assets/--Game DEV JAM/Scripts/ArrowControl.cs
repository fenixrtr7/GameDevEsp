using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeArrow
{
    UP,
    DOWN,
    RIGHT,
    LEFT,
    NONE
}

public class ArrowControl : MonoBehaviour
{
    [SerializeField] float speed = 6;

    public TypeArrow typeArrow;
    bool isInArrow = false;
    int contador = 0;

    void FixedUpdate()
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.RUNNING)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (contador == 2)
        {
            isInArrow = true;
        }
        else
        {
            isInArrow = false;
        }


        if (Input.GetKeyDown(KeyCode.DownArrow) && typeArrow == TypeArrow.DOWN)
        {
            if (isInArrow)
            {
                Debug.Log("Punto");
                gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && typeArrow == TypeArrow.UP)
        {
            if (isInArrow)
            {
                Debug.Log("Punto");
                gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && typeArrow == TypeArrow.RIGHT)
        {
            if (isInArrow)
            {
                Debug.Log("Punto");
                gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && typeArrow == TypeArrow.LEFT)
        {
            if (isInArrow)
            {
                Debug.Log("Punto");
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("box"))
        {
            contador++;
        }

        if(other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("box"))
        {
            contador--;
        }
    }
}
