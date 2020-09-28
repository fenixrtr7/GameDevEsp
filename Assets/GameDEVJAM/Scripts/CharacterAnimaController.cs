using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimaController : MonoBehaviour
{
    public Animator anim;
    public bool flipX = false;

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
        if (flipX)
        { 
            anim.SetBool("walkL", value);
            anim.SetBool("walkR", false);
        }
        else
        {
            anim.SetBool("walkR", value);
            anim.SetBool("walkL", false);
        }
    }

}
