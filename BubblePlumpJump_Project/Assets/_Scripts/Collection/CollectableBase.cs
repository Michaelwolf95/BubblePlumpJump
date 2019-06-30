using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MichaelWolfGames;
using System;

public abstract class CollectableBase : MonoBehaviour
{
    protected bool isCollected = false;

    public Action OnCollected = null;

}