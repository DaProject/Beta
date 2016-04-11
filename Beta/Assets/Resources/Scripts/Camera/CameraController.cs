using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    
    void FixedUpdate()
    {
        //transform.LookAt(playerTransform.transform);

        transform.position = new Vector3(playerTransform.transform.position.x, 0, playerTransform.transform.position.z);
        transform.rotation = playerTransform.transform.rotation;
    }
}
