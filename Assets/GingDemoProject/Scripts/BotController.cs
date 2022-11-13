using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController : MonoBehaviour, IBotInteract
{
    private Collider _mainCol;
    private NavMeshAgent _mainNav;
    private Animator _myAnim;
    
    private Transform _followObj;
    private Transform _player;
    private bool _canFollowStatue = false;
    
    [Header("WALK DATA")]
    [SerializeField] private float _followSpeed = 1f;
    [SerializeField] private float _rotSpeed = 4f;

    private void OnEnable()
    {
        EventManager.OnChildBotAction += ChildBotAction;
    }

    private void OnDisable()
    {
        EventManager.OnChildBotAction -= ChildBotAction;
    }
    
    private void Awake()
    {
        _myAnim = transform.GetChild(0).GetComponent<Animator>();
        _mainNav = GetComponent<NavMeshAgent>();
        _mainCol = GetComponent<Collider>();
        _mainNav.updateRotation = false;
    }
    
    public void AddBotToList(PlayerStackController playerStackController)
    {
        playerStackController.AddBotToStackList(this);
    }
    
    public void BotTriggerAction(Transform followObj, Transform player)
    {
        _myAnim.SetBool("Walk", true);
        _canFollowStatue = true;
        _followObj = followObj;
        _player = player;
        _mainCol.enabled = false;
    }
    
    private void Update()
    {
        if (!_canFollowStatue) return;
        if (_followObj == null) return;

        FollowPlayer(_followObj);
        LookToPlayer(_player);
    }
    
    private void FollowPlayer(Transform followObj)
    {
        followObj.position = new Vector3(followObj.position.x, 0f, followObj.position.z);
        transform.position = Vector3.Slerp(transform.position, followObj.position, _followSpeed * Time.deltaTime);
        //_mainNav.SetDestination(followObj.position);
    }
    
    private void LookToPlayer(Transform followObj)
    {
        Vector3 dir = followObj.position - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRot,  _rotSpeed * Time.deltaTime).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void ChildBotAction(bool isMoving)
    {
        if (!_canFollowStatue) return;
        if (_followObj == null) return;
        
        _myAnim.SetBool("Walk", isMoving);
    }
}
