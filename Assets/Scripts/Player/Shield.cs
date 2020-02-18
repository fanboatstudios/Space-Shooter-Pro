using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Shield : MonoBehaviour, IDamageable
{
	[SerializeField] private SpriteRenderer shieldVisualization;
	[SerializeField] private Color[] damageVisualization;
	[SerializeField] private int hits = 3;
	public int Hits { get { return hits; } }

	public void Damage(int amount)
	{
		hits--;
	}

	private void Awake()
	{
		shieldVisualization = GetComponent<SpriteRenderer>();
	}

	void OnEnable()
	{
		if (damageVisualization != null && damageVisualization.Length > 0)
			hits = damageVisualization.Length - 1;
	}

	void Update()
	{
		var color = damageVisualization[Hits];
		if (color != null)
			shieldVisualization.color = color;
	}
}
