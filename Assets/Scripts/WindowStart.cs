using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WindowStart : MonoBehaviour
{
    [SerializeField] private Button buttonStart = null;
    [SerializeField] private Button buttonExit = null;
    [SerializeField] private Button buttonInfo = null;
    [SerializeField] private Button buttonAuthors = null;
    [SerializeField] private WindowInfo windowInfo = null;
    [SerializeField] private WindowAuthors windowAuthors = null;
    [SerializeField] int nextLevel = 1;
    private bool isActive = true;

    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    private void Start()
    {
        buttonStart.onClick.AddListener(OnStartClicked);
        buttonExit.onClick.AddListener(OnExitClicked);
        buttonInfo.onClick.AddListener(OnInfoClicked);
        buttonAuthors.onClick.AddListener(OnAuthorsClicked);
    }

    private void OnAuthorsClicked()
    {
        windowAuthors.SetState(true);
    }

    private void OnStartClicked()
    {
        SceneManager.LoadScene($"Level {nextLevel}");
    }

    private void OnExitClicked()
    {
        Application.Quit();
    }

    private void OnInfoClicked()
    {
        windowInfo.SetState(true);
    }

    public void SetState(bool state)
    {
        gameObject.SetActive(state);
    }
}
