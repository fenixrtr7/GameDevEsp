using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Life : MonoBehaviour
{
    public ProgressBar progressBar;
    public int life = 100;

    private void Start() {
        Init();
    }

    public void Init()
    {
        progressBar.BarValue = life;
    }
    
    public void QuitLife(int damage)
    {
        life -= damage;
        progressBar.BarValue = life;
    }
}
