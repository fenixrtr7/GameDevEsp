using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CombatManager : Manager<CombatManager>
{
    public GameObject combat;

    public void StartCombat(ArrowSongDirections duel)
    {
        GameManager.Instance.UpdateState(GameManager.GameState.COMBAT);
        Spawner.Instance.duel = duel;

        Spawner.Instance.spawnArrorDuelCoroutine = StartCoroutine(Spawner.Instance.SpawnArrowDuel());

        UI_Items.Instance.generalItems.pnlCombat.SetActive(true);
        combat.SetActive(true);
    }

    public void EndCombat(bool lose)
    {
        if(!lose)
            UI_Items.Instance.battleItems.ChangeImageAndActive(UI_Items.Instance.battleItems.spriteWin);
        
        StartCoroutine(QuitText());

        GameObject.FindGameObjectWithTag("Player").GetComponent<LifePlayer>().ResetLife();
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<LifeEnemy>().ResetLife();
        PlayerBox.Instance.damagePlayer = 2.5f;

        Sequence newSequ = DOTween.Sequence();
        newSequ.AppendCallback(() =>
        {
            UI_Items.Instance.generalItems.pnlCombat.SetActive(false);
            combat.SetActive(false);
        });
        Animator mainCamera = GameManager.Instance.player.GetComponent<PlayerController>().CameraAnim;
        mainCamera.SetTrigger("battle");
        newSequ.AppendInterval(1);
        newSequ.AppendCallback(() =>
        {
            GameManager.Instance.UpdateState(GameManager.GameState.RUNNING);
            AudioManager.instance.PlayClipInSource("AmbienceAudioSource");
        });
    }

    IEnumerator QuitText()
    {
        yield return new WaitForSeconds(1.5f);

        UI_Items.Instance.battleItems.imageSpawn.enabled = false;
    }

    /*IEnumerator WaitTimeToStartDuel()
    {
        UI_Items.Instance.battleItems.textCounter.enabled = true;

        yield return new WaitForSeconds(3);
        UI_Items.Instance.battleItems.textCounter.enabled = false;
        StartCoroutine(Spawner.Instance.SpawnArrowDuel());

        UI_Items.Instance.generalItems.pnlCombat.SetActive(true);
        combat.SetActive(true);
    }*/
}
