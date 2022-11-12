using UnityEngine;

public interface ICubeData
{
    void GetDropPoint();
    void GetFlayData(float expoValue, float flyDuration, float randomExpoValue, Color cubeColor);
    
    public Vector3 RandomDropPoint { get; }
    public float ExpoValue { get; }
    public float FlyDuration { get; }
    public float RandomExpoValue { get; }
    public Color CubeColor { get; }
}