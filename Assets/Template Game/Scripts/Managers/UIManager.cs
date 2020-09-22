using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Manager<UIManager>
{
    [SerializeField] Button reset;
    [SerializeField] Button playGame;
    [SerializeField] Button pause;
    [Header("Panels")]
    [SerializeField] GameObject panelWin;
    [SerializeField] GameObject panelControls;
    [SerializeField] GameObject panelMenu;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (playGame != null)
        {
            playGame.onClick.AddListener(delegate() { GameManager.Instance.StarGame(); });
        }else
        {
            Debug.Log("I don't have button play");
        }

        if (reset != null)
        {
            reset.onClick.AddListener(delegate() { GameManager.Instance.ResetLevel(); });
        }else
        {
            Debug.Log("I don't have button reset");
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

    public IEnumerator BackMenu()
    {
        panelMenu.SetActive(true);
        yield return null;
    }
}
