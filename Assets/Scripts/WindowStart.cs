using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowStart : MonoBehaviour
{
    [SerializeField] private Button buttonStart = null;
    [SerializeField] private Button buttonExit = null;
    [SerializeField] private Button buttonInfo = null;
    [SerializeField] private WindowInfo windowInfo = null;
    private bool isActive = true;

    public System.Action OnStartClick = null;

    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        buttonStart.onClick.AddListener(OnStartClicked);
        buttonExit.onClick.AddListener(OnExitClicked);
        buttonInfo.onClick.AddListener(OnInfoClicked);
    }

    private void OnStartClicked()
    {
        OnStartClick?.Invoke();
        isActive = false;
        SetState(isActive);
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
