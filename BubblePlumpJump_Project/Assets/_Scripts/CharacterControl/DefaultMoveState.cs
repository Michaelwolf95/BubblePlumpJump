using System;
using UnityEngine;
using ECM.Common;
using ECM.Controllers;
using HutongGames.PlayMaker;

namespace CharacterControl
{
    public class DefaultMoveState : ECMPlaymakerStateBase
    {
        #region EDITOR EXPOSED FIELDS

        [HutongGames.PlayMaker.Tooltip("The character's walk speed.")]
        public float _walkSpeed = 2.5f;

        [HutongGames.PlayMaker.Tooltip("The character's run speed.")]
        public float _runSpeed = 5.0f;

        public FsmEvent StartSlideEvent;
        #endregion

        #region PROPERTIES

        /// <summary>
        /// The character's walk speed.
        /// </summary>
        public float walkSpeed
        {
            get { return _walkSpeed; }
            set { _walkSpeed = Mathf.Max(0.0f, value); }
        }

        /// <summary>
        /// The character's run speed.
        /// </summary>
        public float runSpeed
        {
            get { return _runSpeed; }
            set { _runSpeed = Mathf.Max(0.0f, value); }
        }

        /// <summary>
        /// Walk input command.
        /// </summary>
        public bool walk { get; private set; }

        #endregion

        public override void HandleOnEnter()
        {
            
        }

        public override void HandleOnExit()
        {
            
        }

        public override Vector3 CalcDesiredVelocity()
        {
            controller.speed = walk ? walkSpeed : runSpeed;

            // If using root motion and root motion is being applied (eg: grounded),
            // use animation velocity as animation takes full control

            if (controller.useRootMotion && controller.applyRootMotion)
                return controller.rootMotionController.animVelocity;

            // else, convert input (moveDirection) to velocity vector

            return controller.moveDirection * controller.speed;
        }

        public override void Animate()
        {
            // If no animator, return
            if (controller.animator == null)
                return;

            // Compute move vector in local space

            var move = controller.transform.InverseTransformDirection(controller.moveDirection);

            // Update the animator parameters

            var forwardAmount = controller.animator.applyRootMotion
                ? move.z
                : Mathf.InverseLerp(0.0f, runSpeed, controller.movement.forwardSpeed);

            controller.animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
            controller.animator.SetFloat("Turn", Mathf.Atan2(move.x, move.z), 0.1f, Time.deltaTime);

            controller.animator.SetBool("OnGround", controller.movement.isGrounded);

            if (!controller.movement.isGrounded)
                controller.animator.SetFloat("Jump", controller.movement.velocity.y, 0.1f, Time.deltaTime);
        }


        public override void HandleInput()
        {
            // Handle your custom input here...

            controller.moveDirection = new Vector3
            {
                x = Input.GetAxisRaw("Horizontal"),
                y = 0.0f,
                z = Input.GetAxisRaw("Vertical")
            };

            //walk = Input.GetButton("Fire3");

            controller.jump = Input.GetButton("Jump");

            // Transform moveDirection vector to be relative to camera view direction
            controller.moveDirection = controller.moveDirection.relativeTo(Camera.main.transform);

            if (Input.GetButtonDown("Fire3"))
            {
                Fsm.Event(StartSlideEvent);
            }
        }

        public override void HandleOnMoveUpdate()
        {
            
        }
    }
}
