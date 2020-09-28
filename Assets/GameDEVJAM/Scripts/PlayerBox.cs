using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBox : Manager<PlayerBox>
{
    bool isInBox = false;
    //int contador = 0;
    LifePlayer lifePlayer;
    LifeEnemy lifeEnemy;
    TypeArrow typeArrow = TypeArrow.NONE;
    ArrowSongDirections arrowSongDirections;
    ArrowControl currentArrow;
    public float damageEnemy = 10f;
    public float damagePlayer = 2.5f;
    public Sprite spriteAro;
    Sprite originalSprite;

    // Start is called before the first frame update
    void Start()
    {
        lifePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<LifePlayer>();
        lifeEnemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<LifeEnemy>();

        arrowSongDirections = Spawner.Instance.duel;

        originalSprite = this.GetComponent<SpriteRenderer>().sprite;
    }

    private void Update()
    {
        if (typeArrow != TypeArrow.NONE)
        {
            //Debug.Log("Pasamos");
            if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && typeArrow == TypeArrow.DOWN)
            {
                ConditionKey(1);
            }

            else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && typeArrow == TypeArrow.UP)
            {
                ConditionKey(2);
            }
            else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && typeArrow == TypeArrow.RIGHT)
            {
                ConditionKey(3);
            }
            else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && typeArrow == TypeArrow.LEFT)
            {
                ConditionKey(0);
            }

            else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && (typeArrow == TypeArrow.UP) && isInBox)
            {
                QuitLifePlayer();

            }
            else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && (typeArrow == TypeArrow.DOWN) && isInBox)
            {
                QuitLifePlayer();
            }
            else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && (typeArrow == TypeArrow.RIGHT) && isInBox)
            {
                QuitLifePlayer();
            }
            else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && (typeArrow == TypeArrow.UP) && isInBox)
            {
                QuitLifePlayer();
            }
            else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && (typeArrow == TypeArrow.DOWN) && isInBox)
            {
                QuitLifePlayer();
            }
            else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && (typeArrow == TypeArrow.LEFT) && isInBox)
            {
                QuitLifePlayer();
            }
            else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && (typeArrow == TypeArrow.UP) && isInBox)
            {
                QuitLifePlayer();
            }
            else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && (typeArrow == TypeArrow.RIGHT) && isInBox)
            {
                QuitLifePlayer();
            }
            else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && (typeArrow == TypeArrow.LEFT) && isInBox)
            {
                QuitLifePlayer();
            }
            else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && (typeArrow == TypeArrow.LEFT) && isInBox)
            {
                QuitLifePlayer();
            }
            else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && (typeArrow == TypeArrow.RIGHT) && isInBox)
            {
                QuitLifePlayer();
            }
            else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && (typeArrow == TypeArrow.DOWN) && isInBox)
            {
                QuitLifePlayer();
            }
        }
    }

    public void QuitLifePlayer()
    {
        lifePlayer.QuitLife(damagePlayer);
        currentArrow.gameObject.SetActive(false);
        AudioManager.Instance.dic_Audio["MainAudioSource"].PlayOneShot(AudioManager.instance.audioWrong);

    }
    List<Coroutine> listCorou = new List<Coroutine>();
    bool isPressed = false;

    IEnumerator ReturnOriginalSprite()
    {
        isPressed = true;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteAro;
        yield return new WaitForSeconds(0.03f);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = originalSprite;

        listCorou.Remove(listCorou[0]);
        isPressed = false;

    }

    void ConditionKey(int d = -1)
    {
        if (isInBox)
        {
            AudioManager.Instance.dic_Audio["MainAudioSource"].PlayOneShot(AudioManager.instance.audioRight);
            if (d == 0)
            {
                GameManager.Instance.player.GetComponent<CharacterAnimaController>().Dance_01();
            }
            else if (d == 1)
            {
                GameManager.Instance.player.GetComponent<CharacterAnimaController>().Dance_02();
            }
            else if (d == 2)
            {
                GameManager.Instance.player.GetComponent<CharacterAnimaController>().Dance_03();
            }
            else if (d == 3)
            {
                GameManager.Instance.player.GetComponent<CharacterAnimaController>().Dance_04();
            }
            //listIndex.Add(listCorouIndex);
            if (!isPressed)
            {
                listCorou.Add(StartCoroutine(ReturnOriginalSprite()));
            }

            if (currentArrow.isEspecial)
            {
                lifeEnemy.QuitLife(damageEnemy);
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
