using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WindowFinish : MonoBehaviour
{
    [SerializeField] private Button buttonNextLevel = null;
    [SerializeField] int nextLevel = 1;
    private bool isActive = false;

    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }
    void Start()
    {
        buttonNextLevel.onClick.AddListener(OnNextLevelClicked);
    }

    private void OnNextLevelClicked()
    {
        isActive = false;
        SetState(isActive);
        SceneManager.LoadScene($"Level {nextLevel}");
        
    }

    public void SetState(bool state)
    {
        gameObject.SetActive(state);
    }
}
