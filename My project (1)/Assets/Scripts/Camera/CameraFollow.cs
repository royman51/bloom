using UnityEngine;


public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform _target;


    [SerializeField] private Vector3 _offset = new Vector3(0f, 1f, -10f);


    [SerializeField] private float _smoothSpeed = 5f;

    private void LateUpdate()
    {
        if (_target == null)
        {
            return;
        }

        Vector3 targetPos = _target.position + _offset;

        transform.position = Vector3.Lerp(

            transform.position,

            targetPos,

            _smoothSpeed * Time.deltaTime
        );
    }
}