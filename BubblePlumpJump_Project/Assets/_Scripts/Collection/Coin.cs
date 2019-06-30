using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Coin : MonoBehaviour, ICollectable
{
    private bool isCollected = false;

    private Action _onCollected;
    public Action OnCollected
    {
        get { return _onCollected; }
        set { _onCollected = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCollected) return;
        if (other.attachedRigidbody != null)
        {
            if (other.attachedRigidbody.tag == "Player")
            {
                Debug.Log("Collected Coin.");
                isCollected = true;
                Destroy(this.gameObject);
                CoinCollectionManager.Instance.AddCoin(1);
            }
        }
    }
}

