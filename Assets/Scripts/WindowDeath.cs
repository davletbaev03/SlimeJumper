using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WindowDeath : MonoBehaviour
{
    [SerializeField] private Button buttonRestart = null;
    [SerializeField] private Button buttonExit = null;
    [SerializeField] public TextMeshProUGUI deathFromText = null;
    [SerializeField] private string level = null;
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
        SceneManager.LoadScene($"{level}");
    }

    private void OnExitClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SetState(bool state)
    {
        gameObject.SetActive(state);
    }
}
