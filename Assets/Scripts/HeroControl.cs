using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HeroControl : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_AudioClip;

    [SerializeField] private Animator animator;

    [SerializeField] private Vector2 jumpForce = new Vector2(0, 350);
    [SerializeField] private Vector2 dashForce = new Vector2(200, 0);
    [SerializeField] private float maxDashDuration = 0.3f;//максимальная длительность рывка
    [SerializeField] private float gravityScale = 5f;//сила гравитации

    [SerializeField] private float jumpDelay = 1f;//задержка прыжка
    private DateTime jumpTime = DateTime.Now;
    [SerializeField] private float dashDelay = 0.3f;//задержка рывка
    private DateTime dashTime = DateTime.Now;

    [SerializeField] private WindowDeath windowDeath = null;
    [SerializeField] private WindowFinish windowFinish = null;

    private float spaceDownTime;
    private bool isDashKeyDown = false;
    [SerializeField] private bool canJump = true;//булька, чтобы прыжок не выполнялся много раз пока персонаж не успел оторваться от земли
    [SerializeField] private bool canClimb = false;


    private void Start()
    {
        PlayLevelSound();
    }
    void Update()
    {
        animator.SetBool("canJump", canJump);
        CheckDashKey(KeyCode.Mouse0);

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isDashKeyDown = false;
            animator.SetBool("isDashed", isDashKeyDown);
        }    
           
    }

    private void CheckDashKey(KeyCode key)
    {
        if (windowDeath != null && windowDeath.IsActive || windowFinish.IsActive && windowFinish != null)
            return;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if ((canClimb && (DateTime.Now - jumpTime) > TimeSpan.FromSeconds(jumpDelay - 0.8f)) ||  
             canJump && !canClimb  && (DateTime.Now - jumpTime) > TimeSpan.FromSeconds(jumpDelay))
        {
            //Debug.LogError("Jump");
            rb.velocity = Vector2.zero;
            rb.AddForce(jumpForce);
            jumpTime = DateTime.Now;
        }
        else if (Input.GetKeyDown(key) && (DateTime.Now - dashTime) > TimeSpan.FromSeconds(dashDelay))
        {
            isDashKeyDown = true;
            animator.SetBool("isDashed", isDashKeyDown);
            //Debug.LogError("Dash");
            spaceDownTime = Time.time;
            rb.velocity = Vector2.zero;
            rb.AddForce(dashForce);
            StartCoroutine(DisableGravityForTime());
            dashTime = DateTime.Now;
        }
    }

    IEnumerator DisableGravityForTime()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // Отключаем гравитацию
        float velocityY = rb.velocity.y;
        rb.velocity = new Vector2(rb.velocity.x,0f);
        rb.gravityScale = 0;

        // Ждем заданное время
        while (isDashKeyDown)
        {
            if (Time.time - spaceDownTime > maxDashDuration)
                break;
            yield return new WaitForSeconds(0.1f);
        }

        // Включаем гравитацию после заданного времени
        rb.gravityScale = gravityScale;
        rb.velocity = new Vector2(0, velocityY);
    }

    private void PlayLevelSound()
    {
        if(m_AudioSource != null)
            m_AudioSource.PlayOneShot(m_AudioClip, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Fireball":
                //TODO DeathAnimation
                //Debug.LogError("skill issue");
                windowDeath.deathFromText.text = "Голова-глаза";
                windowDeath.IsActive = true;
                m_AudioSource.Stop();
                windowDeath.SetState(true);
                break;
            case "Enemy":
                //TODO DeathAnimation
                //Debug.LogError("skill issue");
                windowDeath.deathFromText.text = "Голова-темя";
                windowDeath.IsActive = true;
                m_AudioSource.Stop();
                windowDeath.SetState(true);
                break;
            case "DeathZone":
                //TODO DeathAnimation
                //Debug.LogError("skill issue");
                windowDeath.deathFromText.text = "Пропал без вести";
                windowDeath.IsActive = true;
                m_AudioSource.Stop();
                windowDeath.SetState(true);
                break;

            case "Finish":
                //Debug.LogError("LevelComplete");
                m_AudioSource.Stop();
                windowFinish.IsActive = true;
                windowFinish.SetState(true);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        string tag = collision.gameObject.tag;
        switch (tag)
        {
            case "Platform":
            case "Enemy":
                canJump = true;
                break;

            case "ClimbablePlatform":
                Debug.LogError("climb");
                canClimb = true;
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        switch (tag)
        {
            case "Enemy":
            case "Platform":
                canJump = false;
                break;

            case "ClimbablePlatform":
                canClimb = false;
                break;
        }
    }
}
