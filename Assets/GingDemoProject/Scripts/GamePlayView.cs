using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayView : MonoBehaviour
{
    public VariableJoystick JoystickData;
    public InputData InputData;
    float Angle;
    float HorizantalDir;
    float VerticalDir;
    bool isMoving;
    
    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            HorizantalDir = (JoystickData.Direction.y >= 0) ? 1 : -1;
            if (HorizantalDir > 0)
            {
                VerticalDir = (JoystickData.Direction.x <= 0) ? 1 : -1;
            }
            else
            {
                VerticalDir = (JoystickData.Direction.x >= 0) ? 1 : -1;
            }
            Angle = Vector2.Angle(Vector2.up, JoystickData.Direction) * HorizantalDir * VerticalDir;
            InputData.InputVector = new Vector2(JoystickData.Horizontal, JoystickData.Vertical);
            InputData.angle = Angle;
            isMoving = true;
            EventManager.InvokeInputEvent(isMoving);
        }
        else
        {
            InputData.InputVector = Vector2.zero;
            InputData.angle = 0;
            isMoving = false;
            EventManager.InvokeInputEvent(isMoving);
        }
    }
}
