using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	private float PlayerSpeed = 1f;
	private float PlayerBoundsX = 11.27f;
	private float PlayerBoundsUpperY = 0f;
	private float PlayerBoundsLowerY = -3.8f;

	[SerializeField]
	private GameObject laserPrefab;
	[SerializeField]
	private Vector3 laserPrefabOffset;
	[SerializeField]
	private float fireRate = 0.5f;
	private float nextFire = 0.0f;

	// Start is called before the first frame update
	void Start()
	{
		transform.position = new Vector3(0, 0, 0);
	}

	// Update is called once per frame
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

		if (Mathf.Abs(transform.position.x) > PlayerBoundsX)
		{
			transform.position = new Vector3(-1 * transform.position.x, transform.position.y, 0);
		}

		transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, PlayerBoundsLowerY, PlayerBoundsUpperY), 0);

		transform.Translate(direction * Time.deltaTime * PlayerSpeed);
	}

	private void FireLaser()
	{
		nextFire = Time.time + fireRate;
		Instantiate(laserPrefab, transform.position + laserPrefabOffset, Quaternion.identity);
	}
}
