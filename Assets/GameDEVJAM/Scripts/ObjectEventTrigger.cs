using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEventTrigger : MonoBehaviour
{
    private EventController eventController ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (eventController && Input.GetButtonDown("Jump"))
        {
            Debug.Log("Interactuar");
            eventController.OnActionCalled(EEventType.battle);
            UIManager.Instance.HideTriggerMessage();
            eventController = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "InteractiveObject")
        {
            UIManager.Instance.ShowTriggerMessage("Press SPACE BAR");
            eventController = other.GetComponent<EventController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "InteractiveObject")
        {
            UIManager.Instance.HideTriggerMessage();
            eventController = null;
        }
    }
}
