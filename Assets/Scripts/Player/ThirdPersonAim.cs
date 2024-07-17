using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonAim : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtual;
    [SerializeField] private GameObject crossHair;
    [SerializeField] private LayerMask whatIsGround;

    ThirdPersonCamera thirdPersonCamera;
    WeaponsController weaponsController;
    public Vector3 mousePos {  get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        thirdPersonCamera = GetComponent<ThirdPersonCamera>();
        weaponsController = GetComponent<WeaponsController>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, whatIsGround))
        {
            mousePos = hit.point;
        }


        if (InputManager.Instance.aimInput)
        {
            cinemachineVirtual.gameObject.SetActive(true);
            crossHair.SetActive(true);
            thirdPersonCamera.SetRotaionOnMove(false);
            weaponsController.SetIsRangeAttack(true);
            Vector3 worldAimTarget = mousePos;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20);
        }
        else
        {
            cinemachineVirtual.gameObject.SetActive(false);
            crossHair.SetActive(false);
            thirdPersonCamera.SetRotaionOnMove(true);
            weaponsController.SetIsRangeAttack(false);
        }
    }
}
