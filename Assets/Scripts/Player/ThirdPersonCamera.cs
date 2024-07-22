using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private GameObject cinemachineTargetCamera;

    [SerializeField] private float sensivitive;
    [SerializeField] private float mixPitch;
    [SerializeField] private float maxPitch;
    private float cinemachineTargetYaw;
    private float cinemachineTargetPitch;

    [SerializeField] private Camera _camera;
    [SerializeField] protected float smooth;
    private float velocity;

    private float currentMinPitch;
    private float currentMaxPitch;

    private bool isRotaionOnMove;


    // Start is called before the first frame update
    void Start()
    {
        isRotaionOnMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InventorySystem.cameraLock += CameraLock;
        InventorySystem.cameraUnlock += CameraUnlock;
        SetPitch(mixPitch, maxPitch);
    }

    private void LateUpdate()
    {
        UpdateCameraRotation();  
    }

    public void SetPitch(float mixpitch, float maxPitch)
    {
        this.currentMinPitch = mixpitch;
        this.currentMaxPitch = maxPitch;
    }

    private void UpdateCameraRotation()
    {
        cinemachineTargetPitch = GetCameraRotaion(cinemachineTargetPitch, GetMouseSpeed(InputManager.Instance.mouseY), currentMinPitch, currentMaxPitch, true);
        cinemachineTargetYaw = GetCameraRotaion(cinemachineTargetYaw, GetMouseSpeed(InputManager.Instance.mouseX), float.MinValue, float.MaxValue, false);
        cinemachineTargetCamera.transform.rotation = Quaternion.Euler(cinemachineTargetPitch, cinemachineTargetYaw, 0);
    }

    private float GetCameraRotaion(float currentRotaion,float input, float min, float max, bool isXAxis)
    {
        currentRotaion += isXAxis ? -input : input;
        return Mathf.Clamp(currentRotaion, min, max);
    }

    public float GetMouseSpeed(float input)
    {
        return input * sensivitive * Time.deltaTime;
    }

    public Vector3 MoveRotation(Vector3 direction)
    {
        if(direction.magnitude >= .1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref velocity, smooth * Time.deltaTime);
            if (isRotaionOnMove)
            {
                transform.rotation = Quaternion.Euler(0, angle, 0);
            }
            return Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        }
        return Vector3.zero;
       
    }

    public void SetRotaionOnMove(bool state)
    {
        isRotaionOnMove = state;
    }

    public void CameraLock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void CameraUnlock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
