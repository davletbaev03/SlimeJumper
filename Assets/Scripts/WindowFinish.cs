using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WindowFinish : MonoBehaviour
{
    [SerializeField] private Button buttonNextLevel = null;
    void Start()
    {
        buttonNextLevel.onClick.AddListener(OnNextLevelClicked);
    }

    private void OnNextLevelClicked()
    {
        SetState(false);
        SceneManager.LoadScene("Level 2");
    }

    public void SetState(bool state)
    {
        gameObject.SetActive(state);
    }
}
