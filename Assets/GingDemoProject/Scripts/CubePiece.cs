using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class CubePiece : CubeActions
{
    private Rigidbody _mainRig;
    private Collider _mainCol;
    private MeshRenderer _mainMesh;
    
    private CubeData _myCubeData;
    private ICubeData _cubeData;
    private CubeActions _cubeActionsClass;
    
    private void Awake()
    {
        _mainCol = GetComponent<Collider>();
        _mainRig = GetComponent<Rigidbody>();
        _mainMesh = GetComponent<MeshRenderer>();
        
        _cubeData = GetComponent<ICubeData>();
        _myCubeData = GetComponent<CubeData>();
        _cubeActionsClass = GetComponent<CubeActions>();
        
        _cubeActionsClass.GeneralDataSet(_mainCol, _mainRig, _mainMesh, _myCubeData, _cubeData);
    }
    
    private void Start()
    {
        _mainCol.enabled = false;
    }
}
