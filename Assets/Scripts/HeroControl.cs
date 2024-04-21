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
    [SerializeField] private float maxDashDuration = 0.3f;//������������ ������������ �����
    [SerializeField] private float gravityScale = 5f;//���� ����������

    [SerializeField] private float jumpDelay = 1.5f;//�������� ������
    private DateTime jumpTime = DateTime.Now;
    [SerializeField] private float dashDelay = 0.3f;//�������� �����
    private DateTime dashTime = DateTime.Now;

    [SerializeField] private WindowDeath windowDeath = null;
    [SerializeField] private WindowStart windowStart = null;
    [SerializeField] private WindowFinish windowFinish = null;

    private float spaceDownTime;
    private bool isSpaceDown = false;
    private bool canJump = true;//������, ����� ������ �� ���������� ����� ��� ���� �������� �� ����� ���������� �� �����

    

    private void Start()
    {
        windowStart.OnStartClick += PlayLevelSound;
    }
    void Update()
    {
        animator.SetBool("canJump", canJump);
        CheckSpaceKey(KeyCode.Space);
        
        animator.SetBool("isDashed", isSpaceDown);

        if (Input.GetKeyUp(KeyCode.Space))
            isSpaceDown = false;
    }

    private void CheckSpaceKey(KeyCode key)
    {
        if (windowDeath.IsActive || windowStart.IsActive)
            return;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if(canJump && (DateTime.Now - jumpTime) > TimeSpan.FromSeconds(jumpDelay)) 
        {
            Debug.LogError("Jump");
            rb.AddForce(jumpForce);
            jumpTime = DateTime.Now;
        }

        if (Input.GetKeyDown(key) && (DateTime.Now - dashTime) > TimeSpan.FromSeconds(dashDelay))
        {
            isSpaceDown = true;
            Debug.LogError("Dash");
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
        // ��������� ����������
        rb.gravityScale = 0;

        // ���� �������� �����
        while (isSpaceDown)
        {
            if (Time.time - spaceDownTime > maxDashDuration)
                break;
            yield return new WaitForSeconds(0.1f);
        }

        // �������� ���������� ����� ��������� �������
        rb.gravityScale = gravityScale;
        rb.velocity = Vector2.zero;
    }

    private void PlayLevelSound()
    {
        m_AudioSource.PlayOneShot(m_AudioClip, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                //TODO DeathAnimation
                Debug.LogError("skill issue");
                windowDeath.deathFromText.text = "������-�����";
                windowDeath.IsActive = true;
                m_AudioSource.Stop();
                windowDeath.SetState(true);
                break;
            case "DeathZone":
                //TODO DeathAnimation
                Debug.LogError("skill issue");
                windowDeath.deathFromText.text = "������ ��� �����";
                windowDeath.IsActive = true;
                m_AudioSource.Stop();
                windowDeath.SetState(true);
                break;

            case "Finish":
                Debug.LogError("LevelComplete");
                m_AudioSource.Stop();
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
                Debug.LogError("platform");
                canJump = true;
                break;

            case "Enemy":
                canJump = true;
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
                canJump = false;
                break;

            case "Enemy":
                Debug.LogError("skill gain");
                canJump = false;
                break;
        }
    }
}
