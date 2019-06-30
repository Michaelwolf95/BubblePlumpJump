using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MichaelWolfGames;

public class SpawnEffectOnCollected : SubscriberBase<CollectableBase>
{
    public GameObject effectPrefab;


    protected override void SubscribeEvents()
    {
        this.SubscribableObject.OnCollected += DoOnCollected;
    }

    protected override void UnsubscribeEvents()
    {
        this.SubscribableObject.OnCollected -= DoOnCollected;
    }

    public void DoOnCollected()
    {
        if (effectPrefab)
        {
            var go = GameObject.Instantiate(effectPrefab, this.transform.position, this.transform.rotation, null);
            Destroy(go, 1f);
        }
        //AudioSource.PlayClipAtPoint(clip, this.transform.position);
    }
}