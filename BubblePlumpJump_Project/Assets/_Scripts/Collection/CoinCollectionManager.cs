﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MichaelWolfGames;
using MichaelWolfGames.MeterSystem;
using System;

public class CoinCollectionManager : MonoBehaviour, IMeterable
{
    public static CoinCollectionManager Instance;

    [SerializeField] private int currentCount;
    public int maxCount;

    public float CurrentValue
    {
        get
        {
            return currentCount;
        }
    }

    public float MaxValue
    {
        get
        {
            return maxCount;
        }
    }

    public float PercentValue
    {
        get
        {
            return ((float)currentCount) / ((float)maxCount);
        }
    }

    public Action<float> OnUpdateValue { get; set; }

    // Use this for initialization
    void Start()
    {
        Instance = this;
        OnUpdateValue(0);

    }

    public void AddCoin(int addAmount)
    {
        currentCount = Mathf.Clamp(currentCount + addAmount, 0, maxCount);
        OnUpdateValue(currentCount);
    }
}

