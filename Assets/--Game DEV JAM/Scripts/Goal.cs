using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    BoxCollider box;
    MeshRenderer mesh;
    private void Start() {
        box = GetComponent<BoxCollider>();
        mesh = GetComponent<MeshRenderer>();
    }
    public void OnGoal()
    {
        box.enabled = true;
        mesh.enabled = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player")
        {
            GameManager.Instance.NextLevel();
        }
    }
}
