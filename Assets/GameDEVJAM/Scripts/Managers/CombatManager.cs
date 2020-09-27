using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : Manager<CombatManager>
{
    public GameObject combat;

    public void StartCombat(ArrowSongDirections duel)
    {
        GameManager.Instance.UpdateState(GameManager.GameState.COMBAT);
        Spawner.Instance.duel = duel;

        StartCoroutine(WaitTimeToStartDuel());
    }

    public void EndCombat()
    {
        AudioManager.instance.PlayClipInSource("AmbienceAudioSource");

        UI_Items.Instance.generalItems.pnlCombat.SetActive(false);
        combat.SetActive(false);

        GameManager.Instance.UpdateState(GameManager.GameState.RUNNING);
    }
    IEnumerator WaitTimeToStartDuel()
    {
        UI_Items.Instance.battleItems.textCounter.enabled = true;

        yield return new WaitForSeconds(3);
        UI_Items.Instance.battleItems.textCounter.enabled = false;
        StartCoroutine(Spawner.Instance.SpawnArrowDuel());

        UI_Items.Instance.generalItems.pnlCombat.SetActive(true);
        combat.SetActive(true);
    }
}
