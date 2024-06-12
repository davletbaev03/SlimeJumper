using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingEnemyBehaviour : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_AudioClip;

    //[SerializeField] private Animator animator;

    [SerializeField] private float timeToShoot = 1f;
    [SerializeField] private float minTimeToShoot = 1f;
    [SerializeField] private float maxTimeToShoot = 5f;
    [SerializeField] private DateTime shotTime = DateTime.Now;
    [SerializeField] private GameObject fireball = null;
    [SerializeField] private Vector2 fireballVelocity = new Vector2(-400,0);


    private void Start()
    {
        fireball = Instantiate(fireball, transform.position, Quaternion.identity);
        Rigidbody2D fireballRB = fireball.GetComponent<Rigidbody2D>();
        fireballRB.AddForce(fireballVelocity);
    }
    void Update()
    {
        if (DateTime.Now - shotTime > TimeSpan.FromSeconds(timeToShoot))
        {
            ThrowFireball();
            //animator.SetBool("isJumped", true);
        }
        //if (DateTime.Now - jumpTime > TimeSpan.FromSeconds(0.3f))
        //    animator.SetBool("isJumped", false);
    }

    private void ThrowFireball()
    {
        fireball.transform.position = transform.position;

        timeToShoot = UnityEngine.Random.Range(minTimeToShoot, maxTimeToShoot);
        shotTime = DateTime.Now;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "DeathZone")
        {
            m_AudioSource.PlayOneShot(m_AudioClip, 0.5f);
            Debug.LogError("Enemy death");
            //TODO enemy death animation
        }
    }
}

