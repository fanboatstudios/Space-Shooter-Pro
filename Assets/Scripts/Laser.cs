using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float speed = 8f;
    
    void Update()
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
}
