using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook cinemachine;
    [SerializeField] private Camera _camera;
    [SerializeField] protected float smooth;
    private float velocity;

    private float yAxisSpeed;
    private float xAxisSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InventorySystem.cameraLock += CameraLock;
        InventorySystem.cameraUnlock += CameraUnlock;
        xAxisSpeed = cinemachine.m_XAxis.m_MaxSpeed;
        yAxisSpeed = cinemachine.m_YAxis.m_MaxSpeed;
    }

    void CameraLock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cinemachine.m_YAxis.m_MaxSpeed = 0f;
        cinemachine.m_XAxis.m_MaxSpeed = 0f;
    }
    
    void CameraUnlock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cinemachine.m_YAxis.m_MaxSpeed = yAxisSpeed;
        cinemachine.m_XAxis.m_MaxSpeed = xAxisSpeed;
    }
    

    public Vector3 MoveRotation(Vector3 direction)
    {
        if(direction.magnitude >= .1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref velocity, smooth * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            return Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        }
        return Vector3.zero;
       
    }

    public Vector3 Rotation(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        return Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
    }
}
