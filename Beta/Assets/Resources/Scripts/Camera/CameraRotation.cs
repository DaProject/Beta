using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour
{
    private Transform target;
    public float speedX;
    public float speedY;

    private float xAxis;

    private Vector3 angles;
    private Quaternion rotation;

    void Start()
    {
        xAxis = angles.y;

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (target)
        {
            xAxis = Input.GetAxis("Mouse X") * speedX;

            rotation = Quaternion.Euler(0, xAxis, 0);

            transform.rotation = rotation;
        }
    }
}
