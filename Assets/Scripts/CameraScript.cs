using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameObject heroControl;

    private void Start()
    {
        heroControl = GameObject.FindGameObjectWithTag("Player");
        this.transform.position = new Vector3(heroControl.transform.position.x + 1.3f, 3f, -10f);
    }

    private void Update()
    {
        if (heroControl != null)
            this.transform.position = new Vector3(heroControl.transform.position.x + 1.3f, 3f, -10f);
    }
}
