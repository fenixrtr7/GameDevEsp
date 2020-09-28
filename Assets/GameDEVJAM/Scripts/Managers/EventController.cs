using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    //[HideInInspector]
    public bool duelActive;

    void Start()
    {
        _dynamicID = EventManager.Instance.AddDynamicObject(this.name, this.gameObject, this);
        OnActionCalled(EEventType.idle);
    }

    public void OnActionCalled(EEventType eventToChange, bool interacted = false, bool directFight = false)
    {
        if (eCurrentEvent == eventToChange)
            return;
        if(directFight)
            eventToChange = EEventType.battle;
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
                if (GameManager.Instance.CurrentGameState == GameManager.GameState.RUNNING)
                {

                    return;
                }

                this.gameObject.GetComponent<NPCController>().GoRandom();
                GameManager.Instance.UpdateState(GameManager.GameState.RUNNING);
                break;
                
            case EEventType.chat:
                if (GameManager.Instance.CurrentGameState == GameManager.GameState.DIALOG)
                {
                    Debug.LogError("Dialog state is already in use");
                    return;
                }

                if (this.dialogSequence == null)
                {
                    Debug.LogError("Doesn't exist a dialog for this character");
                    return;
                }

                GameManager.Instance.UpdateState(GameManager.GameState.DIALOG);

                if (dialogSequence.dialog >= dialogSequence.numDialogs)
                {
                    dialogSequence.dialog = 0;
                }
                string dialogKey = dialogSequence.character + "." + dialogSequence.phase.ToString() + "." + dialogSequence.dialog.ToString();
                EventManager.Instance.dialogueManager.StartDialogue(dialogKey);

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
                
                if (this.duel == null)
                {
                    Debug.LogError("Doesn't exist a duel for this character");
                    return;
                }

                GameManager.Instance.UpdateState(GameManager.GameState.COMBAT);

                Transform player = GameManager.Instance.player.transform;
                Animator mainCamera = GameManager.Instance.player.GetComponent<PlayerController>().CameraAnim;

                Sequence newSequ = DOTween.Sequence();
                newSequ.AppendCallback(() =>
                {
                    UI_Items.Instance.battleItems.ChangeImageAndActive(UI_Items.Instance.battleItems.spriteDuel);
                });
                newSequ.AppendInterval(3);
                newSequ.Join(player.DOMove(new Vector3(transform.position.x - 4, 0, transform.position.z), 2));
                mainCamera.SetTrigger("battle");
                newSequ.AppendInterval(1);
                newSequ.AppendCallback(() =>
                {
                    UI_Items.Instance.battleItems.imageSpawn.enabled = false;
                    CombatManager.Instance.StartCombat(this.duel);
                });

                break;
            case EEventType.deafeated:
                break;
            case EEventType.watching:
                this.gameObject.GetComponent<NPCController>().GoToBattle();
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
        [HideInInspector] public int phase;
        public string character;
        public bool exclamation;
        public int numDialogs;
        [HideInInspector] public int dialog;

        public Dialog()
        {
            phase = 0;
            character = "";
            exclamation = false;
            numDialogs = 0;
            dialog = 0;
        }
    }

}
