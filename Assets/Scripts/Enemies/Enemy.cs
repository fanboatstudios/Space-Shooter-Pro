using UnityEngine;

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
	[SerializeField] private Explosion explosion;
	private Animator enemyAnimator;
	private CircleCollider2D circleCollider2D;
	[SerializeField] private GameObject laserPrefab;
	[SerializeField] private AudioClip laserClip;
	private float fireRate;
	private float nextFire = 0.0f;

	private void Awake()
	{
		explosion = GetComponent<Explosion>();
		if (explosion == null)
		{
			Debug.LogError("Explosion is NULL");
		}
		else
		{
			explosion.enabled = false;
		}

		circleCollider2D = GetComponent<CircleCollider2D>();
		if(circleCollider2D == null)
		{
			Debug.LogError("CircleCollider2D is NULL");
		}

		enemyAnimator = GetComponent<Animator>();
		if(enemyAnimator == null)
		{
			Debug.LogError("Enemy Animator is NULL");
		}
		else
		{
			enemyAnimator.ResetTrigger("OnEnemyDeath");
		}

		player = FindObjectOfType<Player>();
		if(this.player == null)
		{
			Debug.LogError("Player is NULL!");
		}

		if(laserPrefab == null)
		{
			Debug.LogError("Laser Prefab is NULL");
		}

		if(laserClip == null)
		{
			Debug.LogError("Laser Audio Clip is NULL");
		}

	}

	private void Update()
	{
		TranslateMovement();
		FireWeapon();
	}

	private void FireWeapon()
	{
		if (Time.time > nextFire)
		{
			nextFire = Time.time + Random.Range(3f,7f);
			Instantiate(laserPrefab, transform.position, Quaternion.identity, transform);
			AudioSource.PlayClipAtPoint(laserClip, transform.position);
		}
	}


	private void TranslateMovement()
	{
		transform.Translate(Vector3.down * Time.deltaTime * speed);

		if(transform.position.y < enemyBoundsLowerY && EnemyDestroyed() == false)
		{
			transform.position = new Vector3(Random.Range(-enemyBoundsX, enemyBoundsX), enemyBoundsUpperY, 0);
		}
	}

	private void OnTriggerEnter2D(Collider2D collidedWith)
	{
		if (collidedWith.CompareTag("Player"))
		{
			var player = collidedWith.gameObject.GetComponent<Player>();
			if (player != null)
			{
				player.TakeDamage();
			}

			DestroyEnemy();
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

			DestroyEnemy();
		}
	}

	private void DestroyEnemy()
	{
		enemyAnimator.SetTrigger("OnEnemyDeath");
		circleCollider2D.enabled = false;
		explosion.enabled = true;
		Destroy(gameObject, 2.8f);
	}

	private bool EnemyDestroyed()
	{
		return circleCollider2D.enabled ? false : true;
	}
}
