using System;
using DG.Tweening;
using UnityEngine;

public abstract class CubeActions : MonoBehaviour, ICubePieceInteract
{
    public Rigidbody _mainRig;
    public Collider _mainCol;
    public MeshRenderer _mainMesh;
    
    public ICubeData _IcubeData;
    public CubeData _myCubeData;
    
    public void FlyCubeAction()
    {
        _IcubeData.GetDropPoint();
        SetTheColor(_myCubeData.CubeColor);
        transform.DOJump(_myCubeData.RandomDropPoint, _myCubeData.ExpoValue, 1, _myCubeData.FlyDuration).OnComplete(() => {
            _mainRig.velocity = Vector3.zero;
            _mainCol.enabled = true;
            //transform.DOScale(Vector3.zero, 0.3f).SetDelay(0.5f).SetEase(Ease.InCubic).OnComplete(() => gameObject.SetActive(false));
        });
        Debug.Log(transform.gameObject.name + " JumpPower: " + _myCubeData.ExpoValue + " FlyDuration: " + _myCubeData.FlyDuration + " DropPoint " + _myCubeData.RandomDropPoint);
    }
    
    public void FlyToPlayerStackPosition(PlayerStackController playerStackController)
    {
        _mainRig.isKinematic = true;
        Collider[] allCol = transform.GetComponents<Collider>();
        for (int i = 0; i < allCol.Length; i++)
        {
            allCol[i].enabled = false;
        }

        transform.SetParent(playerStackController.transform);
        var addedObjData = new PlayerStackClass(gameObject, false, true);
        playerStackController.AddCubeToList(addedObjData);
        EventManager.PlayerStackSetAction();
    }
    
    void SetTheColor(Color color)
    {
        _mainMesh.materials[0].color = color;
    }

    public void GeneralDataSet(Collider col, Rigidbody rig, MeshRenderer mesh, CubeData cubeData , ICubeData ıCubeData)
    {
        _mainCol = col;
        _mainRig = rig;
        _mainMesh = mesh;
        _myCubeData = cubeData;
        _IcubeData = ıCubeData;
    }
}
