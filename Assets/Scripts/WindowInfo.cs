using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowInfo : MonoBehaviour
{
    [SerializeField] private Button buttonExitInfo = null;
    
    void Start()
    {
        buttonExitInfo.onClick.AddListener(OnExitInfoClicked);
    }

    private void OnExitInfoClicked()
    {
        SetState(false);
    }

    public void SetState(bool state)
    {
        gameObject.SetActive(state);
    }
}
