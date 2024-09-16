using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WindowCompletionPercent : MonoBehaviour
{
    [SerializeField] private GameObject levelCompleteZone = null;
    [SerializeField] private GameObject player = null;
    [SerializeField] private TextMeshProUGUI completionText = null;
    private float startPoint;
    private float levelDistance;
    private float playerDistance;

    private void Start()
    {
        startPoint = player.transform.position.x;
        levelDistance = levelCompleteZone.transform.position.x - startPoint;
    }

    private void Update()
    {
        playerDistance = player.transform.position.x - startPoint;
        completionText.text = $"Пройдено: {Mathf.Floor(playerDistance / levelDistance * 100)}%";
    }
}
