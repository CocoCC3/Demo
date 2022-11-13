using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void PlayerStackSet();
    public static event PlayerStackSet OnPlayerStackSetAction;
    public static void PlayerStackSetAction()
    {
        OnPlayerStackSetAction?.Invoke();
    }
    
    public delegate void InputChange(bool isMoving);
    public static event InputChange onInputChange;
    public static void InvokeInputEvent(bool isMoving)
    {
        onInputChange?.Invoke(isMoving);
    }
    
    public delegate void ChildBot(bool isMoving);
    public static event ChildBot OnChildBotAction;
    public static void ChildBotAction(bool isMoving)
    {
        OnChildBotAction?.Invoke(isMoving);
    }
}
