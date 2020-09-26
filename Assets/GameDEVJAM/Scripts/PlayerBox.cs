using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBox : MonoBehaviour
{
    bool isInBox = false;
    int contador = 0;
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
        if (contador == 2)
        {
            isInBox = true;
        }
        else
        {
            isInBox = false;
        }

        if (typeArrow != TypeArrow.NONE)
        {
            //Debug.Log("Pasamos");
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

            else if (Input.GetKeyDown(KeyCode.LeftArrow) && (typeArrow == TypeArrow.UP) && isInBox)
            {
                lifePlayer.QuitLife(arrowSongDirections.damage);
                currentArrow.gameObject.SetActive(false);
                
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && (typeArrow == TypeArrow.DOWN) && isInBox)
            {
                lifePlayer.QuitLife(arrowSongDirections.damage);
                currentArrow.gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && (typeArrow == TypeArrow.RIGHT) && isInBox)
            {
                lifePlayer.QuitLife(arrowSongDirections.damage);
                currentArrow.gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && (typeArrow == TypeArrow.UP) && isInBox)
            {
                lifePlayer.QuitLife(arrowSongDirections.damage);
                currentArrow.gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && (typeArrow == TypeArrow.DOWN) && isInBox)
            {
                lifePlayer.QuitLife(arrowSongDirections.damage);
                currentArrow.gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && (typeArrow == TypeArrow.LEFT) && isInBox)
            {
                lifePlayer.QuitLife(arrowSongDirections.damage);
                currentArrow.gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && (typeArrow == TypeArrow.UP) && isInBox)
            {
                lifePlayer.QuitLife(arrowSongDirections.damage);
                currentArrow.gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && (typeArrow == TypeArrow.RIGHT) && isInBox)
            {
                lifePlayer.QuitLife(arrowSongDirections.damage);
                currentArrow.gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && (typeArrow == TypeArrow.LEFT) && isInBox)
            {
                lifePlayer.QuitLife(arrowSongDirections.damage);
                currentArrow.gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && (typeArrow == TypeArrow.LEFT) && isInBox)
            {
                lifePlayer.QuitLife(arrowSongDirections.damage);
                currentArrow.gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && (typeArrow == TypeArrow.RIGHT) && isInBox)
            {
                lifePlayer.QuitLife(arrowSongDirections.damage);
                currentArrow.gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && (typeArrow == TypeArrow.DOWN) && isInBox)
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
            contador++;

            if (contador == 1)
            {
                currentArrow = other.GetComponent<ArrowControl>();
                typeArrow = currentArrow.typeArrow;
                Debug.Log("Arrow " + typeArrow);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Arrow"))
        {
            contador--;

            if (contador == 0)
            {
                currentArrow = null;
                typeArrow = TypeArrow.NONE;
            }
        }
    }
}
