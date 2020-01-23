using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	private float speed = 4.0f;
	[SerializeField]
	private float enemyBoundsX = 9.0f;


	private void Update()
	{
		TranslateMovement();
	}

	private void TranslateMovement()
	{
		transform.Translate(Vector3.down * Time.deltaTime * speed);

		if(transform.position.y < -6.0f)
		{
			transform.position = new Vector3(Random.Range(-enemyBoundsX, enemyBoundsX), 7, 0);
		}
	}

	private void OnTriggerEnter(Collider collidedWith)
	{
		Debug.Log($"Collided with, {collidedWith.tag}");
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
			Destroy(collidedWith.gameObject);
			Destroy(gameObject);
		}
	}
}
