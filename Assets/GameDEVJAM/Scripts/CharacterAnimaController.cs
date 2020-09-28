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

    public void Idle()
    {
        anim.SetInteger("dance", 0);
        anim.SetBool("walkL", false);
        anim.SetBool("walkR", false);
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

    public void Dance_01()
    {
        anim.SetInteger("dance", 1);
    }

    public void Dance_02()
    {
        anim.SetInteger("dance", 2);
    }

    public void Dance_03()
    {
        anim.SetInteger("dance", 3);
    }

    public void Dance_04()
    {
        anim.SetInteger("dance", 4);
    }

}
