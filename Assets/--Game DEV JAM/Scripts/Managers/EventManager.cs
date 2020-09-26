using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EEventType { idle, battle, deafeated, chat, walking, none};
public class EventManager : Manager<EventManager>
{
    public int intsForDynamicIDs;
    public Dictionary<string, DynamicObject> dic_dynamicObjects;
    private void Awake()
    {
        intsForDynamicIDs = -1;
    }

    public void UpdateEvent(string objID, EEventType prevEvent = EEventType.none, EEventType nextEvent = EEventType.none)
    {
        foreach (KeyValuePair<string, DynamicObject> obj in dic_dynamicObjects)
        {
            if (nextEvent == EEventType.deafeated && obj.Value.controller.autoProgressDialogPhase)
                dic_dynamicObjects[obj.Key].controller.dialogSequence.phase++;
            if (obj.Value.controller.objInteractions == null)
                continue;
            IterateOnInteractions(obj, prevEvent, nextEvent);
        }
    }

    private void IterateOnInteractions(KeyValuePair<string, DynamicObject> obj, EEventType prevEvent, EEventType nextEvent)
    {
        foreach (EventController.Interaction inter in obj.Value.controller.objInteractions)
        {
            if (inter.trigger == EventController.Interaction.EEventTrigger.onChagedEvent && inter.eventTriggered == nextEvent
                || inter.trigger == EventController.Interaction.EEventTrigger.onDialogPhaseShown && prevEvent == EEventType.chat
                || inter.trigger == EventController.Interaction.EEventTrigger.onEnemyDefeated && nextEvent == EEventType.deafeated
                || inter.trigger == EventController.Interaction.EEventTrigger.onInteracted && prevEvent == EEventType.none && nextEvent == EEventType.none
                || inter.trigger == EventController.Interaction.EEventTrigger.onObjEnabled && prevEvent == EEventType.none
                || inter.trigger == EventController.Interaction.EEventTrigger.onObjDisabled && nextEvent == EEventType.none)
                ActivateActions(inter, obj, nextEvent);
        }
    }

    private void ActivateActions(EventController.Interaction inter, KeyValuePair<string, DynamicObject> obj, EEventType nextEvent)
    {
        if (inter.action == EventController.Interaction.EEventAction.changeEvent)
            obj.Value.controller.OnActionCalled(nextEvent);
        else if (inter.action == EventController.Interaction.EEventAction.unlockNextDialog)
            dic_dynamicObjects[obj.Key].controller.dialogSequence.phase++;
        else if (inter.action == EventController.Interaction.EEventAction.unlockDuel)
            dic_dynamicObjects[obj.Key].controller.duelActive = true;
        else if (inter.action == EventController.Interaction.EEventAction.disableSelfOffCameras || inter.action == EventController.Interaction.EEventAction.enableSelfOffCameras)
            dic_dynamicObjects[obj.Key].controller.enableOrDisableSelf = true;
        else if (inter.action == EventController.Interaction.EEventAction.followPlayer)
            dic_dynamicObjects[obj.Key].controller.followPlayer = true;
        else if (inter.action == EventController.Interaction.EEventAction.moveTowardsPoint)
            dic_dynamicObjects[obj.Key].controller.FollowPoint();
    }

    public string AddDynamicObject(string name, GameObject obj, EventController controller)
    {
        string dynamicID;
        int intId = DynamicID();
        if (dic_dynamicObjects == null)
            dic_dynamicObjects = new Dictionary<string, DynamicObject>();
        dynamicID = name + intId.ToString();
        dic_dynamicObjects.Add(dynamicID, new DynamicObject(obj, controller));
        return dynamicID;
    }

    private int DynamicID()
    {
        intsForDynamicIDs++;
        return intsForDynamicIDs;
    }

    [System.Serializable]
    public class DynamicObject
    {
        public GameObject objectRelated;
        public EventController controller;

        public DynamicObject(GameObject _objectRelated, EventController _controller)
        {
            objectRelated = _objectRelated;
            controller = _controller;
        }
    }
}


