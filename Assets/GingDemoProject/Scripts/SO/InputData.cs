using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputData", menuName = "ScriptableObjects/InputData", order = 1)]
public class InputData : ScriptableObject
{
    public Vector2 InputVector;
    public float angle;
    public bool isCarrying;
    public bool isChase;
    public bool isCarryingDishWasher;
}
