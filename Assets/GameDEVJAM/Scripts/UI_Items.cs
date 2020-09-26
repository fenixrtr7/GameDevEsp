using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    // General Vars
    [System.Serializable]
    public class General
    {
        public GameObject pnlMainMenu;
        public GameObject pnlPause;
        public GameObject pnlFreeExploration;
        public GameObject pnlLoading;
    }

    // MainMenu Vars
    [System.Serializable]
    public class MainMenu
    {
        public Text txtName;
        public Text txtExp;
        public Text txtRacha;
        public Image imgProfile;
    }

    [System.Serializable]
    public class PauseMenu
    {
        public Text txtName;
        public Text txtExp;
        public Text txtRacha;
        public Image imgProfile;
    }

    // GeneProfileral Vars
    [System.Serializable]
    public class FreeExploration
    {
        public Text txtTriggerMessage;
    }

    // Ranking Vars
    [System.Serializable]
    public class Dance
    {
        public Transform usersContainer;
        public GameObject pnlLoading;
    }

    // CVEditor Vars
    [System.Serializable]
    public class Dialogue
    {
        public Image imgProfile;
    }

}

[System.Serializable]
public class UI_Items : Manager<UI_Items>
{

    public UI.General generalItems = new UI.General();
    public UI.MainMenu mainMenuItems = new UI.MainMenu();
    public UI.PauseMenu profileItems = new UI.PauseMenu();
    public UI.FreeExploration freeExplorationItems = new UI.FreeExploration();
    public UI.Dance cvEditorItems = new UI.Dance();
    public UI.Dialogue cvViewerItems = new UI.Dialogue();

}
