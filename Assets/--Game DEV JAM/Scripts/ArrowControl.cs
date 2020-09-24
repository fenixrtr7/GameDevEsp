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

    void FixedUpdate()
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.RUNNING)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }
}
