using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Manager<UIManager>
{
    [SerializeField] Button buttonReset;
    [SerializeField] Button buttonPlayGame;
    [SerializeField] Button buttonPause;
    [SerializeField] Button buttonContinue;
    [Header("Panels")]
    [SerializeField] GameObject panelWin;
    [SerializeField] GameObject panelPause;
    [SerializeField] GameObject panelControls;
    [SerializeField] GameObject panelMenu;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (buttonPlayGame != null)
        {
            buttonPlayGame.onClick.AddListener(delegate() { GameManager.Instance.StarGame(); });
        }else
        {
            Debug.Log("I don't have button play");
        }

        if (buttonReset != null)
        {
            buttonReset.onClick.AddListener(delegate() { GameManager.Instance.ResetLevel(); });
        }else
        {
            Debug.Log("I don't have button reset");
        }

        if (buttonPause != null)
        {
            buttonPause.onClick.AddListener(delegate () { AcivePause(true); });
        }
        else
        {
            Debug.Log("I don't have button pause");
        }

        if (buttonContinue != null)
        {
            buttonContinue.onClick.AddListener(delegate () { AcivePause(false); });
        }
        else
        {
            Debug.Log("I don't have button continue");
        }

        // record.onClick.AddListener(delegate() { GameManager.Instance.ResetLevel(); });
        // pause.onClick.AddListener(delegate() { GameManager.Instance.ResetLevel(); });
        // rewind.onClick.AddListener(delegate() { GameManager.Instance.ResetLevel(); });
        // stop.onClick.AddListener(delegate() { GameManager.Instance.ResetLevel(); });
    }

    public void ActivePanelControls()
    {
        panelControls.SetActive(true);
        panelMenu.SetActive(false);
    }

    public IEnumerator WinPanel()
    {
        panelWin.SetActive(true);
        panelControls.SetActive(false);
        yield return new WaitForSeconds(5f);
        panelWin.SetActive(false);
        StartCoroutine(BackMenu());
    }

    public void AcivePause(bool active)
    {
        panelPause.SetActive(active);
        GameManager.Instance.TogglePause();
    }

    public IEnumerator BackMenu()
    {
        panelMenu.SetActive(true);
        yield return null;
    }

    public void ShowTriggerMessage(string message)
    {
        UI_Items.Instance.freeExplorationItems.txtTriggerMessage.gameObject.SetActive(true);
        UI_Items.Instance.freeExplorationItems.txtTriggerMessage.text = message;
    }

    public void HideTriggerMessage()
    {
        UI_Items.Instance.freeExplorationItems.txtTriggerMessage.gameObject.SetActive(false);
    }
}
