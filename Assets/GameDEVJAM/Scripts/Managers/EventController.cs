using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public EEventType eCurrentEvent;
    public bool autoProgressDialogPhase;
    public bool autoWatchDanceDuels;
    public List<Interaction> objInteractions;
    public List<Timer> objTimers;
    public Dialog dialogSequence;
    public List<Vector3> pointsToWalk;
    public ArrowSongDirections duel;
    private string _dynamicID;
    [HideInInspector]
    public bool duelActive;

    void Start()
    {
        _dynamicID = EventManager.Instance.AddDynamicObject(this.name, this.gameObject, this);
        OnActionCalled(EEventType.idle);
    }

    private void Update()
    {

    }

    public void OnActionCalled(EEventType eventToChange, bool interacted = false)
    {
        if (eCurrentEvent == eventToChange)
            return;
        _cancelTimer = true;
        EEventType prevEvent = eCurrentEvent;
        eCurrentEvent = eventToChange;
        if (prevEvent != EEventType.none)
        {
            EventManager.Instance.UpdateEvent(_dynamicID, prevEvent, eCurrentEvent, interacted);
            StateSettings();
        }
    }

    private void StateSettings()
    {
        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;
        }
        switch (eCurrentEvent) ///TO DO: set all of assets and stuff
        {
            case EEventType.idle:

                break;
            case EEventType.chat:
                break;
            case EEventType.walking:
                if (!_followingPoint)
                    StartWalk();
                break;
            case EEventType.battle:
                if (GameManager.Instance.CurrentGameState == GameManager.GameState.COMBAT)
                {
                    Debug.LogError("Combat state is already in use");
                    return;
                }
                GameManager.Instance.UpdateState(GameManager.GameState.COMBAT);
                if (this.duel == null)
                {
                    Debug.LogError("Doesn't exist a duel for this character");
                    return;
                }
                CombatManager.Instance.StartCombat(this.duel);

                break;
            case EEventType.deafeated:
                break;
            case EEventType.watching:
                MoveToWathPoint();
                break;
            case EEventType.none:
                break;
        }
        if (objTimers != null)
        {
            Timer timerSelected = null;
            foreach (Timer timer in objTimers)
            {
                if (timer.activationEvent == eCurrentEvent)
                {
                    if (timerSelected == null)
                        timerSelected = timer;
                    else
                        Debug.LogError("Can't have more than 1 timers for a state");
                }
            }
            if (timerSelected == null)
                return;
            if (_timerCoroutine != null)
                _timerCoroutine = StartCoroutine(EExecuteTimer(timerSelected));
        }
    }

    public void MoveToWathPoint()
    {

    }

    private bool _followingPoint;
    public void FollowPoint()
    {
        _followingPoint = true;
    }

    public void FollowPlayer()
    {

    }

    private int _walkIndex = 0;
    public void StartWalk()
    {
        if (pointsToWalk == null)
        {
            Debug.LogError("There are no points to move!");
            return;
        }
        for (int i = 0; i < pointsToWalk.Count; i++)
        {
            if (_walkCoroutine != null)
                return;
            if (i == _walkIndex)
            {
                _walkIndex++;
                _walkCoroutine = StartCoroutine(EWalking(pointsToWalk[i]));
            }
        }
    }

    private Coroutine _walkCoroutine;
    private IEnumerator EWalking(Vector3 pointToMove)
    {
        if (_followingPoint)
        {
            _walkCoroutine = null;
            yield return null;
        }
    }

    private bool _cancelTimer;
    private Coroutine _timerCoroutine;
    private IEnumerator EExecuteTimer(Timer timer)
    {
        yield return new WaitForSeconds(timer.timeInSecs);
        if (_cancelTimer)
        {
            _cancelTimer = false;
            yield return null;
        }
        _timerCoroutine = null;
        OnActionCalled(timer.activationEvent);
    }

    [System.Serializable]
    public class Interaction
    {
        public enum EEventTrigger
        {
            onObjEnabled,
            onObjDisabled,
            onEnemyDefeated,
            onDialogPhaseShown,
            onInteracted,
            onChagedEvent
        };
        public enum EEventAction
        {
            unlockDuel,
            unlockNextDialog,
            disableSelfOffCameras,
            enableSelfOffCameras,
            moveTowardsPoint,
            followPlayer,
            changeEvent
        };
        public EventController objConnected;
        public EEventTrigger trigger;
        public EEventAction action;
        public EEventType eventTriggered;
        public EEventType eventToChange;
        public Vector3 pointToMove;

        public void UpdateEventTrigger()
        {

        }

    }

    [System.Serializable]
    public class Timer
    {
        public float timeInSecs;
        public EEventType activationEvent;
        public Interaction.EEventAction action;
    }

    [System.Serializable]
    public class Dialog
    {
        public int phase;
        public int characterDialogIndex;
        public bool exclamation;

        public Dialog()
        {
            phase = 0;
            characterDialogIndex = 0;
            exclamation = false;
        }
    }

}
