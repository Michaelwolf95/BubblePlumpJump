using System;
using UnityEngine;
using ECM.Common;
using HutongGames.PlayMaker;

namespace CharacterControl
{
    public class BubbleSlideState : ECMPlaymakerStateBase
    {
        public float moveSpeed = 8f;
        public FsmGameObject bubbleObject;

        private bool cancelBubble = false;
        private bool jump = false;

        public override void HandleOnEnter()
        {
            bubbleObject.Value.SetActive(true);
            controller.animator.Play("BubbleSkate");
        }

        public override void HandleOnExit()
        {
            bubbleObject.Value.SetActive(false);
        }

        public override void HandleOnMoveUpdate()
        {
            if (cancelBubble)
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

            cancelBubble = Input.GetButtonUp("Fire3");

            controller.jump = Input.GetButton("Jump");


            // Transform moveDirection vector to be relative to camera view direction

            controller.moveDirection = controller.moveDirection.relativeTo(Camera.main.transform);
        }

        public override Vector3 CalcDesiredVelocity()
        {
            controller.speed = this.moveSpeed;

            return controller.moveDirection * controller.speed;
        }

        public override void Animate()
        {
            
        }


    }
}
