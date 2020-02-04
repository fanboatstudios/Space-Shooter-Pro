using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Asteroid : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 3.0f;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject explosionPrefab;
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if(gameManager == null)
        {
            Debug.LogError("GameManager is NULL!");
        }
        
        if(explosionPrefab == null)
        {
            Debug.LogError("ExplosionPrefab is NULL");
        }
    }

    
    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collidedWith)
    {
        if (collidedWith.CompareTag("Laser"))
        {
            DestroyAsteroid(collidedWith);
        }
    }

    private void DestroyAsteroid(Collider2D collidedWith)
    {
        GetComponent<CircleCollider2D>().enabled = false;

        var laserParent = collidedWith.transform.parent;
        if (laserParent != null && laserParent.childCount <= 1)
        {
            Destroy(laserParent.gameObject);
        }
        else
        {
            Destroy(collidedWith.gameObject);
        }

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject, .4f);

        gameManager.EnableSpawning();
    }
}
