using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePlayer : Life
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Arrow"))
        {
            Debug.Log("Damage " +  PlayerBox.Instance.damagePlayer);
            QuitLife(PlayerBox.Instance.damagePlayer);
            other.gameObject.SetActive(false);
        }
    }

    public override void Dead()
    {
        base.Dead();

        Spawner.Instance.StopCoroutineSpawnDuel();

        CombatManager.Instance.EndCombat();
        Spawner.Instance.OffArrows();
    }
}
