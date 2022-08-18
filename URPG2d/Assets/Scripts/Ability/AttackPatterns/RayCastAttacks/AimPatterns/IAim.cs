using UnityEngine;

public interface IAim
{ 
    public Vector2? GetHitPoint(Vector2 startPoint,Camera camera, float range);
}
