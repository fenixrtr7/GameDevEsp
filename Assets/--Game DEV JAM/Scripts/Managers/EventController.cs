using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public EEventType eCurrentEvent;
    public List<Interaction> objInteractions;
    public List<Timer> objTimers;
    public bool autoProgressDialogPhase;
    private string _dynamicID;
    
    // Start is called before the first frame update
    void Start()
    {
        _dynamicID = EventManager.Instance.AddDynamicObject(this.name, this.gameObject, this);
    }

    public void OnDialogCalled()
    {
        if (eCurrentEvent == EEventType.chat)
            return;
        EEventType prevEvent = eCurrentEvent;
        eCurrentEvent = EEventType.chat;
        EventManager.Instance.UpdateEvent(_dynamicID, prevEvent, eCurrentEvent);
    }

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
            unlockNextDuel,
            unlockNextDialog,
            unlockNextDialogPhase,
            disableSelfOffCameras,
            enableSelfOffCameras,
            changeEvent
        };
        public EventController objConnected;
        public EEventTrigger trigger;
        public EEventAction action;
        //public EEventType eventTriggered;
        //public EEventType eventToChange;

        public void UpdateEventTrigger()
        {

        }

    }

    public class Timer
    {
        public enum EEventTimerAction
        {
            unlockNextDuel,
            unlockNextDialog,
            unlockNextDialogPhase,
            disableSelfOffCameras,
            enableSelfOffCameras,
            changeEvent
        };
        public float timeInSecs;
        public EEventTimerAction action;
    }
}
