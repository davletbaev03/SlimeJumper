using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HeroControl : MonoBehaviour
{
    [SerializeField] private Vector2 jumpForce = new Vector2(0, 350);
    [SerializeField] private Vector2 dashForce = new Vector2(200, 0);
    [SerializeField] private float dashTime = 0.5f;//длительность рывка
    [SerializeField] private float gravityScale = 5f;//сила гравитации

    [SerializeField] private WindowDeath windowDeath = null;
    [SerializeField] private WindowStart windowStart = null;
    [SerializeField] private WindowFinish windowFinish = null;
    

    private bool isMidAir = false;//булька для определения возможности рывка
    private DateTime jumpTime;
    
    private bool isJumped = false;//булька, чтобы прыжок не выполнялся много раз пока персонаж не успел оторваться от земли
    private bool isDashed = false;//булька, чтобы рывок выполнялся только один раз
    void Update()
    {
        CheckSpaceKey(KeyCode.Space);
    }

    private void CheckSpaceKey(KeyCode key)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (!(windowDeath.IsActive || windowStart.IsActive) && Input.GetKey(key))
        {
            if(!isMidAir && !isJumped) 
            {
                Debug.LogError("Jump");
                isDashed = false;
                isJumped = true;
                rb.AddForce(jumpForce);
                jumpTime = DateTime.Now;
            }
            else if (!isDashed && (DateTime.Now - jumpTime) > TimeSpan.FromSeconds(0.18f))
            {
                Debug.LogError("Dash");
                isDashed = true;
                rb.velocity = Vector2.zero;
                StartCoroutine(DisableGravityForTime());
                rb.AddForce(dashForce);
            }
        }
    }

    IEnumerator DisableGravityForTime()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // Отключаем гравитацию
        rb.gravityScale = 0;

        // Ждем заданное время
        yield return new WaitForSeconds(dashTime);

        // Включаем гравитацию после заданного времени
        rb.gravityScale = gravityScale;
        rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        string tag = collision.gameObject.tag;
        switch (tag)
        {
            case "Platform":
                Debug.LogError("platform");
                isJumped = false;
                isMidAir = false;
                break;

            case "Enemy":
            case "DeathZone":
                //TODO DeathAnimation
                Debug.LogError("skill issue");
                windowDeath.IsActive = true;
                windowDeath.SetState(true);
                break;

            case "Finish":
                Debug.LogError("LevelComplete");
                windowFinish.SetState(true);
                break;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        switch (tag)
        {
            case "Platform":
                Debug.LogError("platform lose");
                isMidAir = true;
                break;

            case "Enemy":
                Debug.LogError("skill gain");
                break;
        }
    }
}
