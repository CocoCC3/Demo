using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawnerManager : MonoBehaviour, ICubeSpawnerInteract
{
    [Header("CUBE DATA SO")]
    [SerializeField] private CubePieceDataSO _cubePieceDataSO;

    [Space]
    [SerializeField] private Transform _spawnPosition;
    
    public void CubeTriggerAction(int value)
    {
        for (int i = 0; i < value; i++)
        {
            var randomCubeData = ReturnCubePieceData();
            if (randomCubeData == null) continue;
            
            var cubePiece = Instantiate(randomCubeData.cubePieceObj, _spawnPosition.position, Quaternion.identity);
            cubePiece.transform.SetParent(transform);
            cubePiece.GetComponent<CubeData>().GetComponent<ICubeData>().GetFlayData(
                randomCubeData.forcePower,
                randomCubeData.duration,
                randomCubeData.randomExpoValue, randomCubeData.cubeColor);
            cubePiece.GetComponent<ICubePieceInteract>().FlyCubeAction();
            
        }
    }
    
    CubePieceData ReturnCubePieceData()
    {
        var randomIndex = Random.Range(0, _cubePieceDataSO.cubePieceDataList.Count);
        return _cubePieceDataSO.cubePieceDataList[randomIndex].cubePieceData;
    }
}
