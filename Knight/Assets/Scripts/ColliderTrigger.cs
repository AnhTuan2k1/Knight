using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour {

    public event EventHandler OnPlayerEnterTrigger;

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.CompareTag("Player")) {
            // Player inside trigger area!
            OnPlayerEnterTrigger?.Invoke(this, EventArgs.Empty);
        }
    }

}
