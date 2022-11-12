using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController : MonoBehaviour, IBotInteract
{
    private Collider _mainCol;
    private NavMeshAgent _mainNav;
    
    private Transform _followObj;
    private Transform _player;
    public float _followSpeed = 1f;
    public float _rotSpeed = 4f;
    public bool _canFollowStatue = false;
    
    private void Awake()
    {
        _mainNav = GetComponent<NavMeshAgent>();
        _mainCol = GetComponent<Collider>();
    }
    
    public void AddBotToList(PlayerStackController playerStackController)
    {
        playerStackController.AddBotToStackList(this);
    }
    
    public void BotTriggerAction(Transform followObj, Transform player)
    {
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
        transform.position = Vector3.Lerp(transform.position, followObj.position, _followSpeed * Time.deltaTime);
    }

    private void LookToPlayer(Transform followObj)
    {
        Vector3 dir = followObj.position - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRot,  _rotSpeed * Time.deltaTime).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}
