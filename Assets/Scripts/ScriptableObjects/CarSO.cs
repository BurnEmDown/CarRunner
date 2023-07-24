using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CarSO : ScriptableObject
{
    public Vector2 size;
    public float speed;
    public Color color;
    public bool canMoveLanes;
}
