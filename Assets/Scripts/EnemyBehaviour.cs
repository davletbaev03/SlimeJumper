using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_AudioClip;

    [SerializeField] private Vector2 jumpForce = new Vector2(0f,900f);
    [SerializeField] private float timeToJump = 1f;
    [SerializeField] private float minTimeToJump = 1f;
    [SerializeField] private float maxTimeToJump = 5f;
    [SerializeField] private DateTime jumpTime = DateTime.Now;

    
    void Update()
    {
        if (DateTime.Now - jumpTime > TimeSpan.FromSeconds(timeToJump))
            EnemyJump();
    }

    private void EnemyJump()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        timeToJump = UnityEngine.Random.Range(minTimeToJump, maxTimeToJump);
        jumpTime = DateTime.Now;
        rb.AddForce(jumpForce);
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

