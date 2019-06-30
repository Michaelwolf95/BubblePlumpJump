using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Coin : CollectableBase
{
    private void OnTriggerEnter(Collider other)
    {
        if (isCollected) return;
        if (other.attachedRigidbody != null)
        {
            if (other.attachedRigidbody.tag == "Player")
            {
                Debug.Log("Collected Coin.");
                isCollected = true;
                CoinCollectionManager.Instance.AddCoin(1);

                OnCollected();

                foreach (Transform child in this.transform)
                {
                    Destroy(child.gameObject);
                }

                Destroy(this.gameObject, 1f);
            }
        }
    }
}

