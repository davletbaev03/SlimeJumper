using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BackGroundParallax : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    private Vector3 previousCamPos;

    [SerializeField] private float parallaxSpeed = 0.5f;

    void Start()
    {
        // �������������� ���������� ������� ����
        previousCamPos = mainCamera.transform.position;
    }

    private void Update()
    {
        // ��������� �������� ���� � ���������� ����������
        Vector2 offset = (Vector2)(mainCamera.transform.position - previousCamPos) * parallaxSpeed;

        // ��������� �������� � ������� ������� ����
        transform.position += new Vector3(offset.x, offset.y, 0);

        // ��������� ���������� ������� ����
        previousCamPos = mainCamera.transform.position;
    }
}
