﻿using System;
using UnityEngine;
using ECM.Common;
using HutongGames.PlayMaker;

namespace CharacterControl
{
    public class HoverJumpState : ECMPlaymakerStateBase
    {
        public float moveSpeed = 2f;

        public FsmGameObject bubbleObject;

        public FsmEvent cancelEvent;

        private bool cancelBubble = false;

        public override void HandleOnEnter()
        {
            bubbleObject.Value.SetActive(true);
            controller.animator.SetBool("IsHovering", true);
            //controller.animator.SetTrigger("HoverJump");

            controller.maxMidAirJumps = 1;
            cancelBubble = false;
        }

        public override void HandleOnExit()
        {
            controller.maxMidAirJumps = 0;
            cancelBubble = false;
            bubbleObject.Value.SetActive(false);
            controller.animator.SetBool("IsHovering", false);
            //controller.animator.SetTrigger("EXIT_HOVER");
            if (Input.GetButton("Jump") == false)
            {
                
            }
            else
            {
                
               // this.Finish();
            }

            //controller.allowVerticalMovement = false;

            //controller.movement.gravity = originalGravity;
        }

        public override void HandleOnMoveUpdate()
        {
            if (cancelBubble || controller.isGrounded)
            {
                Fsm.Event(cancelEvent);
            }
            else if (this.controller.movement.velocity.y <= 0)
            {
                if (Input.GetButton("Jump") == false)
                {
                 //   bubbleObject.Value.SetActive(false);
                    //controller.animator.SetTrigger("EXIT_HOVER");
                   // controller.animator.SetBool("IsHovering", false);
                    Fsm.Event(cancelEvent);
                }
                else
                {
                    this.Finish();
                }
            }
        }

        public override void HandleInput()
        {
            controller.moveDirection = new Vector3
            {
                x = Input.GetAxisRaw("Horizontal"),
                y = 0.0f,
                z = Input.GetAxisRaw("Vertical")
            };

            cancelBubble = Input.GetButtonUp("Jump");

            // Transform moveDirection vector to be relative to camera view direction

            controller.moveDirection = controller.moveDirection.relativeTo(Camera.main.transform);
        }

        public override Vector3 CalcDesiredVelocity()
        {
            controller.speed = this.moveSpeed;

            if (controller.useRootMotion && controller.applyRootMotion)
                return controller.rootMotionController.animVelocity;

            return controller.moveDirection * controller.speed;
        }

        public override void Animate()
        {
            // Compute move vector in local space

            var move = controller.transform.InverseTransformDirection(controller.moveDirection);

            // Update the animator parameters

            var forwardAmount = controller.animator.applyRootMotion
                ? move.z
                : Mathf.InverseLerp(0.0f, moveSpeed, controller.movement.forwardSpeed);

            controller.animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
            controller.animator.SetFloat("Turn", Mathf.Atan2(move.x, move.z), 0.1f, Time.deltaTime);

            controller.animator.SetBool("OnGround", controller.movement.isGrounded);

            if (!controller.movement.isGrounded)
                controller.animator.SetFloat("Jump", controller.movement.velocity.y, 0.1f, Time.deltaTime);
        }


    }
}