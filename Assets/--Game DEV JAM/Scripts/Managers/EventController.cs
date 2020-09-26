using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public EEventType eCurrentEvent;
    public List<Interaction> objInteractions;
    public List<Timer> objTimers;
    public bool autoProgressDialogPhase;
    public Dialog dialogSequence;
    private string _dynamicID;
    [HideInInspector]
    public bool duelActive;
    public bool enableOrDisableSelf;
    public bool followPlayer;
    
    void Start()
    {
        _dynamicID = EventManager.Instance.AddDynamicObject(this.name, this.gameObject, this);
    }

    private void Update()
    {
        if (enableOrDisableSelf)
        {
            if (this.enabled)
                this.enabled = false;
            else
                this.enabled = true;
            enableOrDisableSelf = false;
        }

        //if (Input.GetKeyDown(KeyCode.R))
        //{

        //}
    }

    public void OnActionCalled(EEventType eventToChange)
    {
        if (eCurrentEvent == eventToChange)
            return;
        EEventType prevEvent = eCurrentEvent;
        eCurrentEvent = eventToChange;
        EventManager.Instance.UpdateEvent(_dynamicID, prevEvent, eCurrentEvent);
        StateSettings();
    }

    public void FollowPoint()
    {

    }

    private void StateSettings()
    {
        switch (eCurrentEvent) ///TO DO: set all of assets and stuff
        {
            case EEventType.idle:
                break;
            case EEventType.chat:
                break;
            case EEventType.walking:
                break;
            case EEventType.battle:
                break;
            case EEventType.deafeated:
                break;
            case EEventType.none:
                break;
        }
        foreach (Timer timer in objTimers)
        {
            if (timer.activationEvent == eCurrentEvent)
                StartCoroutine(EExecuteTimer(timer));
        }
    }

    private IEnumerator EExecuteTimer(Timer timer)
    {
        yield return new WaitForSeconds(timer.timeInSecs);
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

        }
    }

}
