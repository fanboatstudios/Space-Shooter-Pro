using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Engine : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	private Animator animator;
	private bool engineEnabled;

	public bool Enabled { 
		get { return engineEnabled; } 
		set
		{
			engineEnabled = value;
			spriteRenderer.enabled = true;
			animator.enabled = true;
		} 
	}


	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		
		spriteRenderer.enabled = false;
		animator.enabled = false;
	}
}
