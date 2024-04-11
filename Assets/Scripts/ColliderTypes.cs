using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColliderType
{
    Platform = 1,
    Enemy = 2,
}

[RequireComponent(typeof(Collider))]
public class ColliderTypes : MonoBehaviour
{
    [SerializeField]
    private ColliderType type;
    public ColliderType Type => type;
}
