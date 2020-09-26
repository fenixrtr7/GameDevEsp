using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEventType { idle, battle, deafeated, chat};
public class EventManager : Manager<EventManager>
{
    public int intsForDynamicIDs;
    private Dictionary<string, DynamicObject> dic_dynamicObjects;
    private void Awake()
    {
        intsForDynamicIDs = -1;
    }

    public void UpdateEvent(string objID, EEventType prevEvent, EEventType nextEvent)
    {
        foreach (KeyValuePair<string, DynamicObject> obj in dic_dynamicObjects)
        {
            if (dic_dynamicObjects[obj.Key].)
            {

            }
        }
    }

    private void UnlockNextDuel()
    {

    }

    private void UnlockNextDialog()
    {

    }

    private void UnlockNextDialogPhase()
    {

    }

    private void DisableOffCameras()
    {

    }

    private void EnableOffCameras()
    {

    }

    private void ChangeEvent(EEventType eventToSet)
    {

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
        public GameObject obj;
        public EventController controller;

        public DynamicObject(GameObject _obj, EventController _controller)
        {
            obj = _obj;
            controller = _controller;
        }
    }
}


