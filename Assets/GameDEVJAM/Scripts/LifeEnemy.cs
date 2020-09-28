using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeEnemy : Life
{
    public override void Dead()
    {
        base.Dead();

        UI_Items.Instance.battleItems.ChangeImageAndActive(UI_Items.Instance.battleItems.spriteOverkill);
        StartCoroutine(QuitText());

        PlayerBox.Instance.damagePlayer = 0;
    }

    IEnumerator QuitText()
    {
        yield return new WaitForSeconds(1.5f);

        UI_Items.Instance.battleItems.imageSpawn.enabled = false;
    }
}
