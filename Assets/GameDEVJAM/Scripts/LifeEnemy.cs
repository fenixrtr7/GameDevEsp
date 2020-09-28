using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeEnemy : Life
{
    public override void Dead()
    {
        base.Dead();

        // TO DO Sacar over kill
        UI_Items.Instance.battleItems.textCounter.text = "Over Kill!!";
        UI_Items.Instance.battleItems.textCounter.enabled = true;

        StartCoroutine(QuitText());

        PlayerBox.Instance.damagePlayer = 0;
    }

    IEnumerator QuitText()
    {
        yield return new WaitForSeconds(1.5f);

        UI_Items.Instance.battleItems.textCounter.enabled = false;
        UI_Items.Instance.battleItems.textCounter.text = "Duel!!!";
    }
}
