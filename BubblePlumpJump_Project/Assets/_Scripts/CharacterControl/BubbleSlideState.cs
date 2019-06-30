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

        private bool _CancelBubble = false;
        private bool _Jump = false;
        private bool _JustEnteredState = false;

        public override void HandleOnEnter()
        {
            _JustEnteredState = true;
            bubbleObject.Value.SetActive(true);
            controller.animator.Play("BubbleSkate");
        }

        public override void HandleOnExit()
        {
            bubbleObject.Value.SetActive(false);
            controller.animator.SetTrigger("EXIT");
        }

        public override void HandleOnMoveUpdate()
        {
            if (_CancelBubble)
                this.Finish();
        }

        [Range(0, 1)]
        public float _TurnSpeedMultiplier = 0.5f;
        [Range(0,1)] 
        public float _DefaultBubbleSpeed = 0.5f;

        public override void HandleInput()
        {
            controller.moveDirection = new Vector3
            {
                x = Input.GetAxisRaw("Horizontal") * _TurnSpeedMultiplier,
                y = 0.0f,
                z = Input.GetAxisRaw("Vertical") + _DefaultBubbleSpeed
            };               

            _CancelBubble = Input.GetButtonUp("Fire3");

            controller.jump = Input.GetButton("Jump");

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
            
        }


    }
}
