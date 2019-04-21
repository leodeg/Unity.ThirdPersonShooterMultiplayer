using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Multiplayer;

namespace StateAction
{
    public class StateManager : MonoBehaviour, IHittable
    {
        [Header ("Layer Masks")]
        public bool setupDefaultLayerAtStart = true;
        public LayerMask groundLayers;
        public LayerMask weaponRaycastLayers;

        [Header ("State Properties")]
        public State currentBehaviorState;
        public Ballistics ballisticActions;

        [Header ("Inventory")]
        public Inventory inventory;

        [Header ("Animation")]
        public AnimationsName animationName;
        public AnimatorParameters animatorParameters;
        public AnimationHashes animationHashes;

        [Header ("State")]
        public VaultData vaultData;
        public StateProperties currentState;

        [Header ("Multiplayer")]
        public MultiplayerListener multiplayerListener;
        public bool isOfflineController;
        public StateActions offlineActions;

        [HideInInspector] public MovementProperties movementProperties;
        [HideInInspector] public float deltaTime;

        // References
        [HideInInspector] public Animator animatorInstance;
        [HideInInspector] public Rigidbody rigidbodyInstance;
        [HideInInspector] public Transform transformInstance;
        [HideInInspector] public AnimatorController animatorController;

        private void Start ()
        {
            animationHashes = new AnimationHashes ();
            rigidbodyInstance = GetComponent<Rigidbody> ();

            if (isOfflineController)
            {
                offlineActions.Execute (this);
            }
        }

        private void FixedUpdate ()
        {
            deltaTime = Time.deltaTime;
            if (currentBehaviorState != null)
            {
                currentBehaviorState.FixedTick (this);
            }
        }

        private void Update ()
        {
            deltaTime = Time.deltaTime;

            if (currentBehaviorState != null)
                currentBehaviorState.Tick (this);
        }

        public void SetCurrentState (State state)
        {
            if (state != null)
                currentBehaviorState.OnExit (this);

            currentBehaviorState = state;
            currentBehaviorState.OnEnter (this);

        }

        public void PlayAnimation (string animationName)
        {
            animatorInstance.CrossFade (animationName, 0.2f);
        }

        public void OnHit (StateManager shooter, Weapon weapon, Vector3 direction, Vector3 position)
        {
            throw new System.NotImplementedException ();
        }
    }
}
