using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateAction
{
	public class StateManager : MonoBehaviour
	{
		public MovementProperties movementProperties;
		public State currentState;
		[HideInInspector] public float deltaTime;
		[HideInInspector] public Transform transformInstance;
		[HideInInspector] public Rigidbody rigidbodyInstance;
		[HideInInspector] public Animator animatorInstance;
		[HideInInspector] public LayerMask ignoreLayers;

		private void Start ()
		{
			transformInstance = this.transform;
			ignoreLayers = ~( 1 << 9 | 1 << 3 );
			animatorInstance = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator> ();

			rigidbodyInstance = GetComponent<Rigidbody> ();
			rigidbodyInstance.drag = 4;
			rigidbodyInstance.angularDrag = 999;
			rigidbodyInstance.constraints = RigidbodyConstraints.FreezeRotation;
		}

		private void FixedUpdate ()
		{
			deltaTime = Time.deltaTime;
			if (currentState != null)
			{
				currentState.FixedTick (this);
			}
		}

		private void Update ()
		{
			deltaTime = Time.deltaTime;
			if (currentState != null)
			{
				currentState.Tick (this);
			}
		}
	}
}
