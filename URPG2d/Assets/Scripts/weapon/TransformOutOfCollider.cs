using UnityEngine;

public class TransformOutOfCollider : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private Camera _camera;
    private Vector3 _localPosition;
    // Update is called once per frame
    private void Start()
    {
        _localPosition = transform.localPosition;
    }
    private void Update()
    {
        _localPosition.x = (_targetTransform.position.x < _camera.ScreenToWorldPoint(Input.mousePosition).x) ? 0.4f : -0.4f;
        transform.localPosition = _localPosition;
        
    }
}
