using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float speed = 8f;

    void Update()
    {
        Movement();
    }

    protected virtual void Movement()
    {
        if (gameObject.CompareTag("Laser"))
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);

            if (transform.position.y >= 8f)
            {
                var laserParent = transform.parent;
                if (laserParent != null)
                {
                    Destroy(laserParent.gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        else if (gameObject.CompareTag("EnemyLaser"))
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);

            if (transform.position.y <= -6f)
            {
                var laserParent = transform.parent;
                if (laserParent != null)
                {
                    Destroy(laserParent.gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }

    }
}
