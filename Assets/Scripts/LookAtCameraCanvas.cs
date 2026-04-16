using UnityEngine;

public class LookAtCameraCanvas : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        transform.forward = cam.transform.forward;
    }
}
