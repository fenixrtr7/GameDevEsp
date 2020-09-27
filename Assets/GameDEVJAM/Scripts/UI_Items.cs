using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public class Dance
    {
        public Image imgTest;
    }

    [System.Serializable]
    public class Dialogue
    {
        public Image imgTest;
    }

    [System.Serializable]
    public class Battle
    {
        public Image imgTest;
    }

}

[System.Serializable]
public class UI_Items : Manager<UI_Items>
{

    public UI.General generalItems = new UI.General();
    public UI.MainMenu mainMenuItems = new UI.MainMenu();
    public UI.PauseMenu profileItems = new UI.PauseMenu();
    public UI.FreeExploration freeExplorationItems = new UI.FreeExploration();
    public UI.Dance danceItems = new UI.Dance();
    public UI.Dialogue dialogueItems = new UI.Dialogue();
    public UI.Battle battleItems = new UI.Battle();

}
