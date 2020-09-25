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
    public int damage = 1;
    LifePlayer lifePlayer;
    LifeEnemy lifeEnemy;
    [SerializeField] bool isEspecial = false;

    private void Start() {
        lifePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<LifePlayer>();
        lifeEnemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<LifeEnemy>();
    }

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
            ConditionKey();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && typeArrow == TypeArrow.UP)
        {
            ConditionKey();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && typeArrow == TypeArrow.RIGHT)
        {
            ConditionKey();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && typeArrow == TypeArrow.LEFT)
        {
            ConditionKey();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && (typeArrow == TypeArrow.UP) && isInArrow)
        {
            lifePlayer.QuitLife(damage);
            gameObject.SetActive(false);            
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && (typeArrow == TypeArrow.DOWN) && isInArrow)
        {
            lifePlayer.QuitLife(damage);
            gameObject.SetActive(false);            
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && (typeArrow == TypeArrow.RIGHT) && isInArrow)
        {
            lifePlayer.QuitLife(damage);
            gameObject.SetActive(false);            
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && (typeArrow == TypeArrow.UP) && isInArrow)
        {
            lifePlayer.QuitLife(damage);
            gameObject.SetActive(false);            
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && (typeArrow == TypeArrow.DOWN) && isInArrow)
        {
            lifePlayer.QuitLife(damage);
            gameObject.SetActive(false);            
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && (typeArrow == TypeArrow.LEFT) && isInArrow)
        {
            lifePlayer.QuitLife(damage);
            gameObject.SetActive(false);            
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && (typeArrow == TypeArrow.UP) && isInArrow)
        {
            lifePlayer.QuitLife(damage);
            gameObject.SetActive(false);            
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && (typeArrow == TypeArrow.RIGHT) && isInArrow)
        {
            lifePlayer.QuitLife(damage);
            gameObject.SetActive(false);            
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && (typeArrow == TypeArrow.LEFT) && isInArrow)
        {
            lifePlayer.QuitLife(damage);
            gameObject.SetActive(false);            
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && (typeArrow == TypeArrow.LEFT) && isInArrow)
        {
            lifePlayer.QuitLife(damage);
            gameObject.SetActive(false);            
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && (typeArrow == TypeArrow.RIGHT) && isInArrow)
        {
            lifePlayer.QuitLife(damage);
            gameObject.SetActive(false);            
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && (typeArrow == TypeArrow.DOWN) && isInArrow)
        {
            lifePlayer.QuitLife(damage);
            gameObject.SetActive(false);            
        }
    }

    void ConditionKey()
    {
        if (isInArrow)
            {
                //Debug.Log("Punto");
                if (isEspecial)
                {
                    lifeEnemy.QuitLife(damage);
                }
                gameObject.SetActive(false);
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
            other.GetComponent<LifePlayer>().QuitLife(damage);
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
