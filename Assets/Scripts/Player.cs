﻿using UnityEngine;


public class Player : MonoBehaviour
{
	[SerializeField]
	private float playerSpeed = 5.0f;
	[SerializeField]
	private float playerBoundsX = 11.27f;
	[SerializeField]
	private float playerBoundsUpperY = 0f;
	[SerializeField]
	private float playerBoundsLowerY = -3.8f;
	[SerializeField]
	private int playerLives = 3;

	
	[SerializeField]
	private float fireRate = 0.5f;
	private float nextFire = 0.0f;

	
	[SerializeField] 
	private GameObject laserPrefab;
	private Vector3 laserPrefabOffset = new Vector3(0, -3, 0);


	void Start()
	{
		laserPrefabOffset = new Vector3(0, 0.8f, 0);
	}

	void Update()
	{
		TranslateMovement();
		if (Input.GetKey(KeyCode.Space) && Time.time > nextFire && laserPrefab != null)
		{
			FireLaser();
		}
	}

	private void TranslateMovement()
	{
		var horizontalInput = Input.GetAxis("Horizontal");
		var verticalInput = Input.GetAxis("Vertical");

		Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

		if (Mathf.Abs(transform.position.x) > playerBoundsX)
		{
			transform.position = new Vector3(-1 * transform.position.x, transform.position.y, 0);
		}

		transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, playerBoundsLowerY, playerBoundsUpperY), 0);

		transform.Translate(direction * Time.deltaTime * playerSpeed);
	}

	private void FireLaser()
	{
		nextFire = Time.time + fireRate;
		Instantiate(laserPrefab, transform.position + laserPrefabOffset, Quaternion.identity);
	}

	public void TakeDamage()
	{
		playerLives--;

		if(playerLives <= 0)
		{
			Destroy(gameObject);
		}
	}
}
