using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tromba : MonoBehaviour
{
	[SerializeField] protected PropCollector propCollector;
	[SerializeField] protected Transform trombaModel;

	[SerializeField] protected float trombaModelMaxSize = 1.5f;

	[SerializeField] public AudioSource audioSource;

	[SerializeField] private ParticleSystem tromboneParticles;

	public bool CanAttack { get; protected set; } = true;

	protected virtual void Update() { }

	public virtual void TromboneStart()
	{
		if (!CanAttack)
			return;
		tromboneParticles.Play();
	}

	public abstract void TromboneHold();

	public virtual void TromboneRelease()
	{
		tromboneParticles.Stop();
	}
}