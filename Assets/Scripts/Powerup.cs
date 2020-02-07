using UnityEngine;

public class Powerup : MonoBehaviour
{
	[SerializeField] private float speed = 2.5f;
	[SerializeField] private int powerUpWeaponIndex = 0;
	[SerializeField] private PowerupType powerupType = PowerupType.None;
	[SerializeField] private float destroyAtYValue = -7;
	[SerializeField] private AudioClip powerupClip;
	private void Update()
	{
		transform.Translate(Vector3.down * Time.deltaTime * speed);

		if (transform.position.y < destroyAtYValue)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collidedWith)
	{
		if (collidedWith.CompareTag("Player"))
		{
			var player = collidedWith.GetComponent<Player>();

			if (powerupClip != null)
				AudioSource.PlayClipAtPoint(powerupClip, transform.position);

			if(player != null)
			{
				switch (powerupType)
				{
					case PowerupType.Tripleshot:
						player.EnablePowerupWeapon(powerUpWeaponIndex);
						break;
					case PowerupType.Speed:
						player.EnablePowerupSpeed();
						break;
					case PowerupType.Shield:
						player.EnablePowerupShield();
						break;
					default:
						break;

				}
				Destroy(gameObject);
			}
		}
	}
}
