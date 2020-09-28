using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Life : MonoBehaviour
{
    public ProgressBar progressBar;
    public float life = 100;
    bool isDead = false;

    private void Start() {
        Init();
    }

    public virtual void Init()
    {
        progressBar.BarValue = life;
        progressBar.limitValue = life;
    }
    
    public void QuitLife(float damage)
    {
        if(isDead)
            return;
        //Debug.Log("Damage " + damage);
        life -= damage;
        progressBar.BarValue = life;

        if (life <= 0)
        {
            Dead();
        }
    }

    public virtual void Dead()
    {
        isDead = true;
        // StopCoroutine(Spawner.Instance.spawnArrorDuelCoroutine);
        // Spawner.Instance.spawnArrorDuelCoroutine = null;
        
    }

    public void ResetLife()
    {
        life = 100;

        progressBar.BarValue = life;
        progressBar.limitValue = life;

        isDead = false;
    }
}
