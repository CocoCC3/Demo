using DG.Tweening;
using UnityEngine;

public class CubePiece : MonoBehaviour, ICubePieceInteract
{
    private Rigidbody _mainRig;
    private Collider _mainCol;
    private MeshRenderer _mainMesh;
    
    private CubeData _myCubeData;
    private ICubeData _cubeData;
    
    private void Awake()
    {
        _mainRig = GetComponent<Rigidbody>();
        _mainCol = GetComponent<Collider>();
        _mainMesh = GetComponent<MeshRenderer>();
        
        _cubeData = GetComponent<ICubeData>();
        _myCubeData = GetComponent<CubeData>();
    }
    
    private void Start()
    {
        _mainCol.enabled = false;
    }
    
    public void FlyCubeAction()
    {
        _cubeData.GetDropPoint();
        SetTheColor(_myCubeData.CubeColor);
        CubeFlyAction(_myCubeData.ExpoValue, _myCubeData.FlyDuration, _myCubeData.RandomDropPoint);
    }

    public void FlyToPlayerStackPosition(PlayerStackController playerStackController)
    {
        CubeFlyToPlayerAction(playerStackController);
    }
    
    private void CubeFlyAction(float jumpPower, float flyDuration, Vector3 randomDropPosition)
    {
        transform.DOJump(randomDropPosition, jumpPower, 1, flyDuration).OnComplete(() => {
            _mainRig.velocity = Vector3.zero;
            _mainCol.enabled = true;
            //transform.DOScale(Vector3.zero, 0.3f).SetDelay(0.5f).SetEase(Ease.InCubic).OnComplete(() => gameObject.SetActive(false));
        });
        Debug.Log(gameObject.name + " JumpPower: " + jumpPower + " FlyDuration: " + flyDuration + " DropPoint " + randomDropPosition);
    }

    private void CubeFlyToPlayerAction(PlayerStackController playerStackController)
    {
        _mainRig.isKinematic = true;
        Collider[] allCol = GetComponents<Collider>();
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
}
