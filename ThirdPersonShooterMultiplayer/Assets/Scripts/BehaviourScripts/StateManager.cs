using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Multiplayer;
using System;

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
        public PlayerStats playerStats;

        [Header ("Inventory")]
        public Inventory inventory;

        [Header ("OnHit")]
        public string hitEffectsName = "bloodEffects";

        [Header ("Animation")]
        public AnimationsName animationName;
        public AnimatorParameters animatorParameters;
        public AnimationHashes animationHashes;

        [Header ("State")]
        public VaultData vaultData;
        public StateProperties currentState;

        [Header ("Multiplayer")]
        public int photonID;
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
        [HideInInspector] public List<Rigidbody> ragdollRigidbodies = new List<Rigidbody> ();
        [HideInInspector] public List<Collider> ragdollColliders = new List<Collider> ();


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
            if (currentState.isDead) return;

            deltaTime = Time.deltaTime;
            if (currentBehaviorState != null)
            {
                currentBehaviorState.FixedTick (this);
            }
        }

        private void Update ()
        {
            if (currentState.isDead) return;

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

        public void OnHit (StateManager killerStateManager, Weapon weapon, Vector3 direction, Vector3 position)
        {
            ShowHitParticleEffect (direction, position);

            playerStats.health -= weapon.ammoType.damageValue;
            if (playerStats.health <= 0)
            {
                playerStats.health = 0;
                if (!currentState.isDead)
                {
                    currentState.isDead = true;
                    MultiplayerManager.Singleton.BroadcastKillPlayer (photonID, killerStateManager.photonID);
                }
            }
            currentState.healthChangeFlag = true;
        }

        private void ShowHitParticleEffect (Vector3 direction, Vector3 position)
        {
            GameObject hitParticle = Managers.GameManagers.GetObjectPool ().GetObject (hitEffectsName);
            if (hitParticle != null)
            {
                Quaternion rotation = Quaternion.LookRotation (-direction);
                hitParticle.transform.position = position;
                hitParticle.transform.rotation = rotation;
            }
            else
            {
                Debug.LogWarning ("StateManager::GameManagers::ObjectPool::ERROR::Hit particle is null or name of particle: [" + hitEffectsName + "] is wrong.");
            }
        }

        public void SpawnPlayer ()
        {
            if (currentState.isLocal)
            {
                currentState.healthChangeFlag = true;
                playerStats.health = 100;
            }

            currentState.isDead = false;
            animatorInstance.Play (animationName.locomotion);
        }

        public void KillPlayer ()
        {
            currentState.isDead = true;
            animatorInstance.CrossFade (animationName.death, 0.4f);
        }
    }
}
