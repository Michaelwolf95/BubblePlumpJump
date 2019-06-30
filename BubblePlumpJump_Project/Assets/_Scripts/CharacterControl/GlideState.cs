using System;
using UnityEngine;
using ECM.Common;
using HutongGames.PlayMaker;

namespace CharacterControl
{
    public class GlideState : ECMPlaymakerStateBase
    {
        public float moveSpeed = 8f;

        public float gravity = 2.5f;

        public FsmGameObject bubbleObject;

        private bool cancelBubble = false;
        private bool jump = false;


        private float originalGravity;

        public override void HandleOnEnter()
        {
            bubbleObject.Value.SetActive(true);
            //controller.animator.SetTrigger("StartBubbleSkate");
            controller.animator.SetBool("IsHovering", true);

            //controller.allowVerticalMovement = true;

            originalGravity = controller.movement.gravity;
            controller.movement.gravity = gravity;
        }

        public override void HandleOnExit()
        {
            bubbleObject.Value.SetActive(false);
            //controller.animator.SetTrigger("EXIT_HOVER");
            controller.animator.SetBool("IsHovering", false);

            //controller.allowVerticalMovement = false;

            controller.movement.gravity = originalGravity;
        }

        public override void HandleOnMoveUpdate()
        {
            if (cancelBubble || controller.isGrounded)
                this.Finish();
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
