using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowAuthors : MonoBehaviour
{
    [SerializeField] private Button buttonExitAuthors = null;
    
    void Start()
    {
        buttonExitAuthors.onClick.AddListener(OnExitAuthorsClicked);
    }

    private void OnExitAuthorsClicked()
    {
        SetState(false);
    }

    public void SetState(bool state)
    {
        gameObject.SetActive(state);
    }
}
