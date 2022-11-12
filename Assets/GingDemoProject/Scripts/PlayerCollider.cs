using System;
using System.Collections;
using System.Collections.Generic;
using Constants;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private int index;
    private PlayerStackController _playerStackController;
    
    private void Awake()
    {
        _playerStackController = GetComponent<PlayerStackController>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.CubeSpawner)) SpawnerZoneTrigger(other);
        if (other.CompareTag(Tags.CollectableCube)) CollectableObjectTrigger(other);
        if (other.CompareTag(Tags.FollowerBot)) BotTrigger(other);
    }
    
    void SpawnerZoneTrigger(Collider other)
    {
        Debug.Log(other.gameObject.name);
        var cubeInteract = other.GetComponent<ICubeSpawnerInteract>();
        cubeInteract.CubeTriggerAction(10);
    }
    
    void CollectableObjectTrigger(Collider other)
    {
        Debug.Log("CollectCubePiece" + " / " + other.gameObject.name);
        var cubePieceData = other.GetComponent<ICubePieceInteract>();
        cubePieceData.FlyToPlayerStackPosition(_playerStackController);
        index++;
    }

    void BotTrigger(Collider other)
    {
        Debug.Log("TriggerWithBot" + " / " + other.gameObject.name);
        var botData = other.GetComponent<IBotInteract>();
        botData.AddBotToList(_playerStackController);
        botData.BotTriggerAction(_playerStackController.GetBotFollowTransformAndModify(), transform);
    }
}
