﻿using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	private float speed = 4.0f;
	[SerializeField]
	private float enemyBoundsX = 9.5f;
	[SerializeField]
	private float enemyBoundsUpperY = 6.5f;
	[SerializeField]
	private float enemyBoundsLowerY = -5.0f;
	[SerializeField]
	private int pointsAwarded = 10;

	private Player player;

	private void Start()
	{
		player = FindObjectOfType<Player>();

		if(this.player == null)
		{
			Debug.LogError("Player is NULL!");
		}
	}

	private void Update()
	{
		TranslateMovement();
	}

	private void TranslateMovement()
	{
		transform.Translate(Vector3.down * Time.deltaTime * speed);

		if(transform.position.y < enemyBoundsLowerY)
		{
			transform.position = new Vector3(Random.Range(-enemyBoundsX, enemyBoundsX), enemyBoundsUpperY, 0);
		}
	}

	private void OnTriggerEnter2D(Collider2D collidedWith)
	{
		if (collidedWith.CompareTag("Player"))
		{
			var player = collidedWith.gameObject.GetComponent<Player>();
			if (player != null) { 
				player.TakeDamage();
			}

			Destroy(gameObject);
		}
		else if (collidedWith.CompareTag("Laser"))
		{
			var laserParent = collidedWith.transform.parent;
			if (laserParent != null && laserParent.childCount <= 1)
			{
				Destroy(laserParent.gameObject);
			}
			else
			{
				Destroy(collidedWith.gameObject);
			}

			player.AddScore(pointsAwarded);

			Destroy(gameObject);
		}
	}
}
