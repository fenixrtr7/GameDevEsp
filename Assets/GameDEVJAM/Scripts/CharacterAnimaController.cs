using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimaController : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Walking(bool value)
    {
        anim.SetBool("walk", value);
    }

}
