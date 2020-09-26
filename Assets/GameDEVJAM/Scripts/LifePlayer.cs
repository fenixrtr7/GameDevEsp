using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePlayer : Life
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Arrow"))
        {
            QuitLife(other.gameObject.GetComponent<ArrowControl>().damage);
            other.gameObject.SetActive(false);
        }
    }
}
