using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : MonoBehaviour
{
    [SerializeField] float speed = 6;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.RUNNING)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }
}
