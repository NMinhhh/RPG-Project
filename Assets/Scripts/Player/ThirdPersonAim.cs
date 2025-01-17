using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ThirdPersonAim : MonoBehaviour
{
    [SerializeField] private GameObject aimTarget;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtual;
    [SerializeField] private GameObject crossHairIdle;
    [SerializeField] private GameObject crossHairOnTarget;
    [SerializeField] private LayerMask whatIsTarget;
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private Rig animRig;
    [SerializeField] protected float mixPitchAim;
    [SerializeField] protected float maxPitchAim;
    ThirdPersonCamera thirdPersonCamera;
    WeaponsController weaponsController;
    public Vector3 mousePos {  get; private set; }

    private bool isTarget;

    // Start is called before the first frame update
    void Start()
    {
        thirdPersonCamera = GetComponent<ThirdPersonCamera>();
        weaponsController = GetComponent<WeaponsController>();
        animRig.weight = 0;
        aimTarget.SetActive(false);
    }

    // Update is called once per frame

    public void Aim()
    {
        GetTargetAim();
        cinemachineVirtual.gameObject.SetActive(true);
        CrossHair();
        aimTarget.SetActive(true);
        aimTarget.transform.position = mousePos;
        thirdPersonCamera.SetRotaionOnMove(false);
        weaponsController.SetIsRangeAttack(true);
        Vector3 worldAimTarget = mousePos;
        worldAimTarget.y = transform.position.y;
        Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20);
        animRig.weight = 1;
        thirdPersonCamera.SetPitch(mixPitchAim, maxPitchAim);
    }

    public void NotAim()
    {
        cinemachineVirtual.gameObject.SetActive(false);
        crossHairIdle.SetActive(false);
        crossHairOnTarget.SetActive(false);
        aimTarget.SetActive(false);
        thirdPersonCamera.SetRotaionOnMove(true);
        weaponsController.SetIsRangeAttack(false);
        animRig.weight = 0;
        thirdPersonCamera.SetPitch(float.MinValue, float.MaxValue);

    }

    void CrossHair()
    {
        if (isTarget)
        {
            crossHairIdle.SetActive(false);
            crossHairOnTarget.SetActive(true);
        }
        else
        {
            crossHairIdle.SetActive(true);
            crossHairOnTarget.SetActive(false);
        }
    }

    public void GetTargetAim()
    {
        mousePos = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, whatIsTarget))
        {
            if(Physics.Raycast(ray, out RaycastHit enemyHit, 999f, whatIsEnemy))
            {
                isTarget = true;
            }
            else
            {
                isTarget = false;
            }
            mousePos = hit.point;
        }
        else
        {
            isTarget = false;
            mousePos = ray.GetPoint(50);
        }
    }

}
