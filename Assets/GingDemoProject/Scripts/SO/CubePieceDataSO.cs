using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "CubePieceDataSO", menuName = "ScriptableObjects/CubePieceData", order = 1)]
public class CubePieceDataSO : ScriptableObject
{
    [SerializeField] public List<CubePieceClass> cubePieceDataList;
}

[Serializable]
public class CubePieceClass
{
    public CubePieceData cubePieceData;
    public CubePieceClass(CubePieceData cubePieceData)
    {
        this.cubePieceData = cubePieceData;
    }
}

[Serializable]
public class CubePieceData
{
    public GameObject cubePieceObj;
    public Color cubeColor;
    public float forcePower;
    public float duration;
    public float randomExpoValue;
    public CubePieceData(GameObject cubePieceObj, float forcePower, float duration, float randomExpoValue, Color cubeColor)
    {
        this.randomExpoValue = randomExpoValue;
        this.cubePieceObj = cubePieceObj;
        this.forcePower = forcePower;
        this.cubeColor = cubeColor;
        this.duration = duration;
    }
}
