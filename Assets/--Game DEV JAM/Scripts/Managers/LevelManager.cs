using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Manager<LevelManager>
{
    [SerializeField] List<GameObject> keys = new List<GameObject>();
    [SerializeField] GameObject goal;
    // Start is called before the first frame update
    void Start()
    {
        PhaseInitial();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //RecPlay.Instance.ChangeState(1);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            //RecPlay.Instance.ChangeState(4);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            //RecPlay.Instance.ChangeState(5);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            //RecPlay.Instance.ChangeState(3);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.ResetLevel();
        }
    }

    public void GetKey(GameObject key)
    {
        keys.Remove(key);

        if (keys.Count == 0)
        {
            //goal.GetComponent<Goal>().OnGoal();
        }
    }

    public void PhaseInitial()
    {
        goal = GameObject.FindGameObjectWithTag("goal");

        foreach(GameObject key in GameObject.FindGameObjectsWithTag("key")) {
             keys.Add(key);
        }

        //RecPlay.Instance.ChangeState(3);
    }
}
