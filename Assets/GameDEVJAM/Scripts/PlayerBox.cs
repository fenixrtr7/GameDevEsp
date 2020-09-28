using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBox : MonoBehaviour
{
    bool isInBox = false;
    //int contador = 0;
    LifePlayer lifePlayer;
    LifeEnemy lifeEnemy;
    TypeArrow typeArrow = TypeArrow.NONE;
    ArrowSongDirections arrowSongDirections;
    ArrowControl currentArrow;
    // Start is called before the first frame update
    void Start()
    {
        lifePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<LifePlayer>();
        lifeEnemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<LifeEnemy>();

        arrowSongDirections = Spawner.Instance.duel;
    }

    private void Update()
    {
        if (typeArrow != TypeArrow.NONE)
        {
            //Debug.Log("Pasamos");
            if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && typeArrow == TypeArrow.DOWN)
            {
                ConditionKey();
            }

            else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && typeArrow == TypeArrow.UP)
            {
                ConditionKey();
            }
            else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && typeArrow == TypeArrow.RIGHT)
            {
                ConditionKey();
            }
            else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && typeArrow == TypeArrow.LEFT)
            {
                ConditionKey();
            }

            else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && (typeArrow == TypeArrow.UP) && isInBox)
            {
                lifePlayer.QuitLife(arrowSongDirections.damage);
                currentArrow.gameObject.SetActive(false);

            }
            else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && (typeArrow == TypeArrow.DOWN) && isInBox)
            {
                lifePlayer.QuitLife(arrowSongDirections.damage);
                currentArrow.gameObject.SetActive(false);
            }
            else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && (typeArrow == TypeArrow.RIGHT) && isInBox)
            {
                lifePlayer.QuitLife(arrowSongDirections.damage);
                currentArrow.gameObject.SetActive(false);
            }
            else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && (typeArrow == TypeArrow.UP) && isInBox)
            {
                lifePlayer.QuitLife(arrowSongDirections.damage);
                currentArrow.gameObject.SetActive(false);
            }
            else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && (typeArrow == TypeArrow.DOWN) && isInBox)
            {
                lifePlayer.QuitLife(arrowSongDirections.damage);
                currentArrow.gameObject.SetActive(false);
            }
            else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && (typeArrow == TypeArrow.LEFT) && isInBox)
            {
                lifePlayer.QuitLife(arrowSongDirections.damage);
                currentArrow.gameObject.SetActive(false);
            }
            else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && (typeArrow == TypeArrow.UP) && isInBox)
            {
                lifePlayer.QuitLife(arrowSongDirections.damage);
                currentArrow.gameObject.SetActive(false);
            }
            else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && (typeArrow == TypeArrow.RIGHT) && isInBox)
            {
                lifePlayer.QuitLife(arrowSongDirections.damage);
                currentArrow.gameObject.SetActive(false);
            }
            else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && (typeArrow == TypeArrow.LEFT) && isInBox)
            {
                lifePlayer.QuitLife(arrowSongDirections.damage);
                currentArrow.gameObject.SetActive(false);
            }
            else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && (typeArrow == TypeArrow.LEFT) && isInBox)
            {
                lifePlayer.QuitLife(arrowSongDirections.damage);
                currentArrow.gameObject.SetActive(false);
            }
            else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && (typeArrow == TypeArrow.RIGHT) && isInBox)
            {
                lifePlayer.QuitLife(arrowSongDirections.damage);
                currentArrow.gameObject.SetActive(false);
            }
            else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && (typeArrow == TypeArrow.DOWN) && isInBox)
            {
                lifePlayer.QuitLife(arrowSongDirections.damage);
                currentArrow.gameObject.SetActive(false);
            }
        }
    }

    void ConditionKey()
    {
        //Debug.Log("Cool");
        if (isInBox)
        {
            //Debug.Log("Punto");

            if (currentArrow.isEspecial)
            {
                lifeEnemy.QuitLife(arrowSongDirections.damage);
            }
            currentArrow.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow"))
        {
            isInBox = true;
            currentArrow = other.GetComponent<ArrowControl>();
            typeArrow = currentArrow.typeArrow;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Arrow"))
        {
            isInBox = false;
            currentArrow = null;
            typeArrow = TypeArrow.NONE;
        }
    }

    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     if (other.CompareTag("Arrow"))
    //     {
            
    //     }
    // }
}
