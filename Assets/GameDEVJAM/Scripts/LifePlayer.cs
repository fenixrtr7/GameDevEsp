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
        UI_Items.Instance.battleItems.ChangeImageAndActive(UI_Items.Instance.battleItems.spriteLose);
        StartCoroutine(QuitText());

        base.Dead();

        Spawner.Instance.StopCoroutineSpawnDuel();

        CombatManager.Instance.EndCombat(true);
        Spawner.Instance.OffArrows();
    }

    IEnumerator QuitText()
    {
        yield return new WaitForSeconds(1.5f);

        UI_Items.Instance.battleItems.imageSpawn.enabled = false;
    }
}
