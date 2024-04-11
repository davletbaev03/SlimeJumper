using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WindowDeath : MonoBehaviour
{
    [SerializeField] private Button buttonRestart = null;
    [SerializeField] private Button buttonExit = null;
    private bool isActive = false;

    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }
    void Start()
    {
        buttonRestart.onClick.AddListener(OnRestartClicked);
        buttonExit.onClick.AddListener(OnExitClicked);
    }

    private void OnRestartClicked()
    {
        isActive = false;
        SetState(isActive);
        SceneManager.LoadScene("Level 1");
    }

    private void OnExitClicked()
    {
        Application.Quit();
    }

    public void SetState(bool state)
    {
        gameObject.SetActive(state);
    }
}
