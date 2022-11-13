using System;
using System.Collections;
using System.Collections.Generic;
using Constants;
using UnityEngine;
using Random = UnityEngine.Random;

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
        var randomSize = Random.Range(10, 18);
        cubeInteract.CubeTriggerAction(randomSize);
    }
    
    void CollectableObjectTrigger(Collider other)
    {
        Debug.Log("CollectCubePiece" + " / " + other.gameObject.name);
        var cubePieceData = other.GetComponent<ICubePieceInteract>();
        if (cubePieceData == null) return;
        cubePieceData.FlyToPlayerStackPosition(_playerStackController);
        index++;
    }

    void BotTrigger(Collider other)
    {
        Debug.Log("TriggerWithBot" + " / " + other.gameObject.name);
        var botData = other.GetComponent<IBotInteract>();
        if (botData == null) return;
        botData.AddBotToList(_playerStackController);
        botData.BotTriggerAction(_playerStackController.GetBotFollowTransformAndModify(), transform);
    }
}
