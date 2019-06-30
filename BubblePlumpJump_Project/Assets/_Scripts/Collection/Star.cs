using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ICollectable
{
    Action OnCollected { get; set; }
}

public class Star : MonoBehaviour, ICollectable
{
    private bool isCollected = false;

    private Action _onCollected;
    public Action OnCollected {
        get { return _onCollected; }
        set { _onCollected = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCollected) return;
        if(other.attachedRigidbody != null)
        {
            if(other.attachedRigidbody.tag == "Player")
            {
                Debug.Log("Collected Star.");
                isCollected = true;
                Destroy(this.gameObject);
                StarCollectionManager.Instance.AddStar(1);
            }
        }
    }
}
