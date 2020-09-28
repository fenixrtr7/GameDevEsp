using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class NPCController : MonoBehaviour
{
    public enum Type { STATIC, WALKER, INTERACTIVE};
    public Type type = Type.STATIC;

    public Vector2 rangeTimer;
    public Transform characterSprite;
    //public bool isPauseSequence = false;

    private bool randomMovement = false;
    private bool isMoving = false;
    private NavMeshAgent m_navMeshAgent;
    private Sequence newSequ;
    private Vector3 targetPosition;
    private Collider trigger;
    private EventController eventController;
    private CharacterAnimaController anim;

    [SerializeField]
    private NavMeshCoordi[] navMeshCoordinates;

    [System.Serializable]
    private class NavMeshCoordi
    {
        public Vector2 axisXRange;
        public Vector2 axisYRange;
    }

    private void Awake()
    {
        anim = GetComponent<CharacterAnimaController>();
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        trigger = GetComponent<Collider>();
        eventController = GetComponent<EventController>();
        newSequ = DOTween.Sequence();

        switch (type)
        {
            case Type.INTERACTIVE:
                trigger.enabled = true;
                eventController.enabled = true;
                randomMovement = false;
                break;
            case Type.STATIC:
                trigger.enabled = false;
                eventController.enabled = false;
                randomMovement = false;
                break;
            case Type.WALKER:
                trigger.enabled = false;
                eventController.enabled = true;
                randomMovement = true;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartNPCActions();
    }

    // Update is called once per frame
    void Update()
    {
        characterSprite.rotation = Quaternion.Euler(0, 0, 0);

        if (Vector3.Distance(transform.position, targetPosition) < 0.5 && isMoving)
        {
            isMoving = false;
            ContinueSecuence();
        }

        /*if (GameManager.Instance.CurrentGameState == GameManager.GameState.RUNNING && isPauseSequence)
        {
            ContinueSecuence();
        }*/

        if (isMoving)
        {
            anim.Walking(true);
        }
        else
        {
            anim.Walking(false);
        }

        //debug
        if (Input.GetKeyDown("k"))
        {
            Debug.Log("pause");
            newSequ.Pause();
        }

        //debug
        if (Input.GetKeyDown("l"))
        {
            newSequ.Play();
        }
    }

    private Vector3 GetNewRandomPosition()
    {
        int num = UnityEngine.Random.Range(0, navMeshCoordinates.Length);

        Vector2 axisX = navMeshCoordinates[num].axisXRange;
        Vector2 axisY = navMeshCoordinates[num].axisYRange;

        float x = UnityEngine.Random.Range(axisX.x, axisX.y);
        float y = UnityEngine.Random.Range(axisY.x, axisY.y);

        Vector3 pos = new Vector3(x, 0, y);

        return pos;
    }

    private void StartNPCActions()
    {
        newSequ.AppendCallback(() =>
        {
            switch (UnityEngine.Random.Range(1, 4))
            {
                case 1:
                    //walk
                    if (type == Type.WALKER && GameManager.Instance.CurrentGameState != GameManager.GameState.COMBAT)
                        MoveCharacterTo(GetNewRandomPosition());
                    break;
                case 2:
                    //idle
                    anim.Idle();
                    break;

                case 3:
                    //dance
                    anim.Dance_01();
                    break;
            }
        });
        newSequ.AppendInterval(UnityEngine.Random.Range(rangeTimer.x, rangeTimer.y));
        newSequ.SetLoops(-1, LoopType.Restart);

    }

    public void MoveCharacterTo(Vector3 newPos)
    {
        PauseSequence();
        targetPosition = newPos;
        isMoving = true;
        m_navMeshAgent.SetDestination(targetPosition);

        if (targetPosition.x > transform.position.x)
        {
            anim.flipX = false;
        }
        else
        {
            anim.flipX = true;
        }
    }

    public void PauseSequence()
    {
        //isPauseSequence = true;
        newSequ.Pause();
    }

    public void ContinueSecuence()
    {
        //isPauseSequence = false;
        newSequ.Play();
    }

    public void GoToBattle()
    {
        if (type == Type.WALKER)
        {
            Debug.Log("asdasdad");

            Vector3 playerPos = GameManager.Instance.player.transform.position;
            Vector3 centerPos = new Vector3(playerPos.x - 2, 0, playerPos.z + 2);

            Vector2 newAxisX = new Vector2(centerPos.x - 3, centerPos.x - 0.5f);
            Vector2 newAxisY = new Vector2(centerPos.x + 3, centerPos.x + 0.5f);

            float x = UnityEngine.Random.Range(newAxisX.x, newAxisX.y);
            float y = UnityEngine.Random.Range(newAxisY.x, newAxisY.y);

            Vector3 pos = new Vector3(x, 0, y);

            MoveCharacterTo(new Vector3(playerPos.x - Random.Range(-3.0f,3.0f), 0, playerPos.z + 2 + Random.Range(-0.50f, 0.50f)));
        }
    }

    public void GoRandom()
    {
        MoveCharacterTo(GetNewRandomPosition());
    }

        
}