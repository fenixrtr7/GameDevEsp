using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ArrowControl : MonoBehaviour
{
    [SerializeField] float speed = 6;

    public TypeArrow typeArrow;

    public int damage = 20;
    
    public bool isEspecial = false;

    private void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.COMBAT)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }
}
