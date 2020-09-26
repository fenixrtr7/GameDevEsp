using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class NPCController : MonoBehaviour
{

    public Vector2 rangeTimer;
    public Transform characterSprite;
    public bool randomMovement = false;
    public bool isMoving = false;

    private NavMeshAgent m_navMeshAgent;
    private Sequence newSequ;
    private Vector3 targetPosition;

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
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        newSequ = DOTween.Sequence();
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        characterSprite.rotation = Quaternion.Euler(0, 0, 0);

        if (Vector3.Distance(transform.position, targetPosition) < 0.3)
            isMoving = false;

        if (isMoving)
        {
            //set move animation
        }
        else
        {
            //setIdle animation
        }
    }

    public void Init()
    {
        GetNewAction();
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

    private void GetNewAction()
    {

        newSequ.AppendCallback(() =>
        {
            switch (UnityEngine.Random.Range(1, 3))
            {
                case 1:
                    if (randomMovement)
                        MoveCharacterTo(GetNewRandomPosition());
                    break;
                case 2:
                    //set other animation
                    break;
            }
        });
        newSequ.AppendInterval(UnityEngine.Random.Range(rangeTimer.x, rangeTimer.y));
        newSequ.AppendCallback(() =>
        {
            //Set idle animation
        });
        newSequ.SetLoops(-1, LoopType.Restart);

    }

    public void MoveCharacterTo(Vector3 newPos)
    {
        targetPosition = newPos;
        isMoving = true;
        m_navMeshAgent.SetDestination(targetPosition);
    }
}