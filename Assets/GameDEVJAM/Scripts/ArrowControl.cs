using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ArrowControl : MonoBehaviour
{
    float speed = 12;

    public TypeArrow typeArrow;

    public float damage = 2.5f;
    
    public bool isEspecial = false;

    private void Start()
    {
        damage = 2.5f;
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.COMBAT)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }
}
