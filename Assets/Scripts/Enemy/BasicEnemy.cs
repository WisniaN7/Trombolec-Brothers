using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(EnemyStateMachine))]
public class BasicEnemy : MonoBehaviour {
    [field: SerializeField, Min(0.1f)]
    public float WanderRadius { get; private set; } = 5f;

    [field: SerializeField]
    public Vector3 WanderPivotOffset { get; private set; } = Vector3.zero;

    [field: SerializeField, Min(0.1f)]
    public float Speed { get; private set; } = 1f;

    public Vector3 WanderPivot => transform.position + WanderPivotOffset;

    public bool HasReachedDestination => agent.remainingDistance <= agent.stoppingDistance;

    protected EnemyStateMachine stateMachine;

    protected NavMeshAgent agent;

    protected virtual void Awake() {
        stateMachine = GetComponent<EnemyStateMachine>();

        agent = GetComponent<NavMeshAgent>();
        agent.speed = Speed;
    }

    public void MoveTo(Vector3 destination) => agent.SetDestination(destination);

    public virtual void Attack() {

    }

    public virtual void Die() {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Handles.DrawWireDisc(WanderPivot, Vector3.up, WanderRadius);
    }

    private void OnCollisionEnter(Collision other) {
        if (other?.rigidbody?.velocity.magnitude > 5f) {
            stateMachine.TransitionTo(EnemyStateMachine.EnemyState.Die);
        }
    }
}
