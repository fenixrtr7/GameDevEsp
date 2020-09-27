using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : Manager<CombatManager>
{
    public GameObject combat;

    public void StartCombat()
    {
        GameManager.Instance.UpdateState(GameManager.GameState.COMBAT);
        StartCoroutine(Spawner.Instance.SpawnArrowDuel());

        UI_Items.Instance.generalItems.pnlCombat.SetActive(true);
        combat.SetActive(true);
    }

    public void EndCombat()
    {
        UI_Items.Instance.generalItems.pnlCombat.SetActive(false);
        combat.SetActive(false);
    }
}
