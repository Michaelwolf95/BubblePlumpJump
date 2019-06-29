using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM.Common;
using ECM.Controllers;

public class ECMPlaymakerCharacterController : BaseCharacterController
{
    public PlayMakerFSM stateMachine;
    public ECMPlaymakerStateBase currentCharacterState = null;

    protected override void Move()
    {
        base.Move();
        if (currentCharacterState == null) return;
        currentCharacterState.HandleOnMoveUpdate();
    }

    /// <summary>
    /// Overrides 'BaseCharacterController' HandleInput,
    /// to perform custom controller input.
    /// </summary>
    protected override void HandleInput()
    {
        if (currentCharacterState == null) return;

        currentCharacterState.HandleInput();
    }

    /// <summary>
    /// Overrides 'BaseCharacterController' CalcDesiredVelocity method to handle different speeds,
    /// eg: running, walking, etc.
    /// </summary>
    protected override Vector3 CalcDesiredVelocity()
    {
        if (currentCharacterState == null) return Vector3.zero;
        return currentCharacterState.CalcDesiredVelocity();

    }

    /// <summary>
    /// Overrides 'BaseCharacterController' Animate method.
    /// 
    /// This shows how to handle your characters' animation states using the Animate method.
    /// The use of this method is optional, for example you can use a separate script to manage your
    /// animations completely separate of movement controller.
    /// 
    /// </summary>
    protected override void Animate()
    {
        if (currentCharacterState == null) return;

        currentCharacterState.Animate();
    }

}
