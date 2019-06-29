using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
public abstract class ECMPlaymakerStateBase : FsmStateAction
{
    public ECMPlaymakerCharacterController controller;

    public override void Awake()
    {
        base.Awake();
        controller = this.Owner.GetComponent<ECMPlaymakerCharacterController>();
    }
    public override void OnEnter()
    {
        controller.currentCharacterState = this;
        HandleOnEnter();
    }

    public override void OnExit()
    {
        HandleOnExit();
    }

    public abstract void HandleOnEnter();
    public abstract void HandleOnExit();

    public abstract void HandleOnMoveUpdate();

    public abstract void HandleInput();

    public abstract Vector3 CalcDesiredVelocity();

    public abstract void Animate();

}
