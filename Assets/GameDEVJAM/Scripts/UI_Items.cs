using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    [System.Serializable]
    public class General
    {
        public GameObject pnlMainMenu;
        public GameObject pnlPause;
        public GameObject pnlFreeExploration;
        public GameObject pnlBattle;
        public GameObject pnlLoading;
        public GameObject pnlCombat;
        public Image imageFade;

        public void FadePanel()
        {
            
        }
    }

    [System.Serializable]
    public class MainMenu
    {
        public Image imgTest;
    }

    [System.Serializable]
    public class PauseMenu
    {
        public Image imgTest;
    }

    [System.Serializable]
    public class FreeExploration
    {
        public Text txtTriggerMessage;
    }

    [System.Serializable]
    public class Dialogue
    {
        public Image imgTest;
    }

    [System.Serializable]
    public class Duel
    {
        public Sprite spriteWin;
        public Sprite spriteLose;
        public Sprite spriteOverkill;
        public Sprite spriteDuel;
        public Image imageSpawn;
        public TextMeshProUGUI textCounter;

        public void ChangeImageAndActive(Sprite spr)
        {
            imageSpawn.sprite = spr;
            imageSpawn.enabled = true;
        }
    }

}

[System.Serializable]
public class UI_Items : Manager<UI_Items>
{

    public UI.General generalItems = new UI.General();
    public UI.MainMenu mainMenuItems = new UI.MainMenu();
    public UI.PauseMenu profileItems = new UI.PauseMenu();
    public UI.FreeExploration freeExplorationItems = new UI.FreeExploration();
    public UI.Dialogue dialogueItems = new UI.Dialogue();
    public UI.Duel battleItems = new UI.Duel();

}
