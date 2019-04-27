using UnityEngine;
using System.Collections;

namespace StateAction
{
    public class AnimationHashes
    {
        public int vertical = Animator.StringToHash ("vertical");
        public int horizontal = Animator.StringToHash ("horizontal");
        public int leftFootForward = Animator.StringToHash ("leftFootForward");
        public int rightFootForward = Animator.StringToHash ("rightFootForward");
        public int grounded = Animator.StringToHash ("isGrounded");
        public int vaultWalk = Animator.StringToHash ("Vault Walk");
        public int interacting = Animator.StringToHash ("isInteracting");
        public int aiming = Animator.StringToHash ("aiming");
        public int shooting = Animator.StringToHash ("shooting");
        public int crouching = Animator.StringToHash ("crouch");
    }
}