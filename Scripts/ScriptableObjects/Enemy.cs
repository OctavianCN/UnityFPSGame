using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public float health;
    public float rotationSpeed;
    public float speed;
    public float acceleration;
    public float visionRange;
    public float anugluarSpeed;
    public float visionAngle;
    public float shootAccuracy;
    public float leaveTargetRange;
}
