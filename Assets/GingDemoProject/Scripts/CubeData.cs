using UnityEngine;

public class CubeData : MonoBehaviour, ICubeData
{
    public void GetDropPoint()
    {
        RandomDropPoint = new Vector3(Random.Range(-RandomExpoValue, RandomExpoValue), 0, Random.Range(-RandomExpoValue, RandomExpoValue)) + transform.position;
    }
    
    public void GetFlayData(float newExpoValue, float flyDuration, float randomExpoValue, Color cubeColor)
    {
        CubeColor = cubeColor;
        ExpoValue = newExpoValue;
        FlyDuration = flyDuration;
        RandomExpoValue = randomExpoValue;
    }
    
    public Vector3 RandomDropPoint { get; private set; }
    public float ExpoValue { get; private set; }
    public float FlyDuration { get; private set; }
    public float RandomExpoValue { get; private set; }
    public Color CubeColor { get; private set; }
}
