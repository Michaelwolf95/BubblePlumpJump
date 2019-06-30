using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MichaelWolfGames;

public class CollectableSFX : SubscriberBase<CollectableBase>
{
    public AudioClip clip;


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
        AudioSource source = GetComponent<AudioSource>();
        if(source)
        {
            source.PlayOneShot(clip);
        }
    }
}