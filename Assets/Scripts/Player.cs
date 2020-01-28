using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{
	[SerializeField] private float playerSpeed = 5.0f;
	[SerializeField] private float playerBoundsX = 11.27f;
	[SerializeField] private float playerBoundsUpperY = 0f;
	[SerializeField] private float playerBoundsLowerY = -3.8f;
	[SerializeField] private int playerLives = 3;
	[SerializeField] private float fireRate = 0.15f;
	[SerializeField] private GameObject[] weaponPrefabs;
	[SerializeField] private float powerUpWeaponCooldown = 5f;
	[SerializeField] private Transform laserContainer;

	private float nextFire = 0.0f;
	private SpawnManager spawnManager;
	[SerializeField] private int activeWeaponIndex = 0;

	void Start()
	{
		spawnManager = FindObjectOfType<SpawnManager>();

		if(spawnManager == null)
		{
			Debug.LogError("SpawnManager was not found.");
		}

		if (weaponPrefabs.Length == 0)
		{
			Debug.LogError("Weapon Prefabs array is empty.");
		}
	}

	void Update()
	{
		TranslateMovement();
		CheckForFireWeapon();
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
	}

	public void TakeDamage()
	{
		playerLives--;

		if (playerLives <= 0)
		{
			if(spawnManager != null)
			{
				spawnManager.OnPlayerDeath();
			}

			Destroy(gameObject);
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

	IEnumerator PowerDownWeaponRoutine()
	{
		yield return new WaitForSeconds(powerUpWeaponCooldown);
		activeWeaponIndex = 0;
	}
}
