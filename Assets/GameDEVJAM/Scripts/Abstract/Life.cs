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

    public virtual void Init()
    {
        progressBar.BarValue = life;
        progressBar.limitValue = life;
    }
    
    public void QuitLife(int damage)
    {
        Debug.Log("Damage " + damage);
        life -= damage;
        progressBar.BarValue = life;

        if (life <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        // StopCoroutine(Spawner.Instance.spawnArrorDuelCoroutine);
        // Spawner.Instance.spawnArrorDuelCoroutine = null;
        Spawner.Instance.StopCoroutineSpawnDuel();

        CombatManager.Instance.EndCombat();
        Spawner.Instance.OffArrows();
    }

    public void ResetLife()
    {
        life = 100;
    }
}
