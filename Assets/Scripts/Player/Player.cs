using System;
using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{
	[SerializeField] private float playerSpeed = 5.0f;
	[SerializeField] private float speedBoostMultiplier = 1.25f;
	[SerializeField] private float thrustMulitplier = 1.75f;
	[SerializeField] private float playerBoundsX = 11.27f;
	[SerializeField] private float playerBoundsUpperY = 0f;
	[SerializeField] private float playerBoundsLowerY = -3.8f;
	[SerializeField] private int playerLives = 3;
	[SerializeField] private float fireRate = 0.15f;
	[SerializeField] private GameObject[] weaponPrefabs;
	[SerializeField] private float powerUpCooldown = 5f;
	[SerializeField] private Transform laserContainer;
	private WaitForSeconds coolDownWait;
	private float nextFire = 0.0f;
	[SerializeField] private int activeWeaponIndex = 0;
	[SerializeField] private Explosion explosion;
	[SerializeField] private GameObject shieldVisualizer;
	[SerializeField] private bool shieldIsActive = false;

	[SerializeField] private int score = 0;

	// Engine Animations
	private Engine rightEngine, leftEngine;

	// Sound FX
	[SerializeField] private AudioClip laserClip;
	[SerializeField] private AudioClip hitClip;

	void Start()
	{
		coolDownWait = new WaitForSeconds(powerUpCooldown);
		explosion = GetComponent<Explosion>();
		rightEngine = transform.GetChild(2).gameObject.GetComponent<Engine>();
		leftEngine = transform.GetChild(3).gameObject.GetComponent<Engine>();

		if (weaponPrefabs.Length == 0)
		{
			Debug.LogError("Weapon Prefabs array is empty.");
		}

		if (shieldVisualizer == null)
		{
			Debug.LogError("Shield was not found.");
		}
		else
		{
			shieldVisualizer.SetActive(false);
		}
		
		if(rightEngine == null | leftEngine == null)
		{
			Debug.LogError("1 or more engines are NULL");
		}

		if(explosion == null)
		{
			Debug.LogError("Explosion is NULL");
		}
		else
		{
			explosion.enabled = false;
		}

		if(hitClip == null)
		{
			Debug.LogError("Hit Clip is NULL");
		}

		UIManager.Instance.UpdateLives(playerLives);
	}

	void Update()
	{
		CheckForThruster();
		TranslateMovement();
		CheckForFireWeapon();
	}

	private void CheckForThruster()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			if(thrustMulitplier > 1f)
			{
				playerSpeed *= thrustMulitplier;
			}
		}
		else if(Input.GetKeyUp(KeyCode.LeftShift))
		{
			if (thrustMulitplier > 1f)
			{
				playerSpeed /= thrustMulitplier;
			}
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
	
	private void CheckForFireWeapon()
	{
		if (Input.GetKey(KeyCode.Space) && Time.time > nextFire && weaponPrefabs[activeWeaponIndex] != null)
		{
			FireWeapon();
		}
	}
	
	private void FireWeapon()
	{
		nextFire = Time.time + fireRate;
		Instantiate(weaponPrefabs[activeWeaponIndex], transform.position, Quaternion.identity, laserContainer);
		AudioSource.PlayClipAtPoint(laserClip, transform.position);
	}

	public void TakeDamage()
	{
		if (shieldIsActive)
		{
			shieldVisualizer.SetActive(false);
			shieldIsActive = false;
			return;
		}

		playerLives--;
		UIManager.Instance.UpdateLives(playerLives);

		switch (playerLives)
		{
			case 2:
				rightEngine.Enabled = true;
				AudioSource.PlayClipAtPoint(hitClip, transform.position);
				break;
			case 1:
				leftEngine.Enabled = true;
				AudioSource.PlayClipAtPoint(hitClip, transform.position);
				break;
			default:
				SpawnManager.Instance.OnPlayerDeath();
				GameManager.Instance.GameOver();
				explosion.enabled = true;
				Destroy(gameObject, 0.1f);
				break;
		}
	}

	public void EnablePowerupWeapon(int weaponPrefabIndex)
	{
		if(weaponPrefabs[weaponPrefabIndex] != null)
		{
			activeWeaponIndex = weaponPrefabIndex;
			StartCoroutine(PowerDownWeaponRoutine());
		}
	}

	public void EnablePowerupSpeed()
	{
		playerSpeed *= speedBoostMultiplier;
		StartCoroutine(PowerDownSpeedRoutine());
	}

	IEnumerator PowerDownWeaponRoutine()
	{
		yield return coolDownWait;
		activeWeaponIndex = 0;
	}

	IEnumerator PowerDownSpeedRoutine()
	{
		yield return coolDownWait;
		playerSpeed /= speedBoostMultiplier;
	}

	public void EnablePowerupShield()
	{
		shieldIsActive = true;
		shieldVisualizer.SetActive(true);
	}

	public void AddScore(int points = 10)
	{
		score += points;
		UIManager.Instance.UpdateScore(score);
	}

	private void OnTriggerEnter2D(Collider2D colliedWith)
	{
		if (colliedWith.CompareTag("EnemyLaser"))
		{
			TakeDamage();
		}
	}
}
