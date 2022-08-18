using UnityEngine;

public class LinearAim : IAim
{
    private LayerMask _layerMask;

    public LinearAim(LayerMask unitMask)
    {
        _layerMask = unitMask;
    }
    public Vector2? GetHitPoint(Vector2 startPoint,Camera camera, float range)
    {
        Vector2 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - startPoint).normalized;
        RaycastHit2D hit = Physics2D.Raycast(startPoint,direction,range,_layerMask);

        Vector3 EndPoint;

        if (hit)
        {
            Debug.Log(hit.point);
            EndPoint = hit.point;
        }
        else
        {      
            EndPoint = startPoint + (direction * range);
        }
        
        return EndPoint;
    }
}