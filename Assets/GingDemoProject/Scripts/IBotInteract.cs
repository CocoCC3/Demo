using UnityEngine;

public interface IBotInteract
{
    void BotTriggerAction(Transform followObj, Transform player);
    void AddBotToList(PlayerStackController playerStackController);
}