using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private AudioClip explosionClip;

    private void Awake()
    {
        if(explosionClip == null)
        {
            Debug.LogError("Explosion clip is NULL");
        }
    }

    void Start()
    {
        AudioSource.PlayClipAtPoint(explosionClip, transform.position);
        Destroy(gameObject, 2.5f);
    }
}
