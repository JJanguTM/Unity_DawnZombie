using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


namespace STM
{
    public class CameraSystem : MonoBehaviour
    {
        public static CameraSystem Instance { get; private set; } = null;

        public Camera mainCamera;
        public CinemachineVirtualCamera defaultCamera;
        public CinemachineVirtualCamera aimingCamera;

        public LayerMask aimingLayers;

        public Vector3 AimingTargetPoint { get; private set; } = Vector3.zero;

        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        private void Update()
        {
            // 조준 대상 위치 업데이트
            Ray ray = mainCamera.ScreenPointToRay(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, aimingLayers, QueryTriggerInteraction.Ignore))
            {
                AimingTargetPoint = hit.point;
            }
            else
            {
                AimingTargetPoint = ray.GetPoint(1000f);
            }
        }

        public void SetActiveAimingCamera(bool isAiming)
        {
            // Default 및 Aiming Cameras 전환
            defaultCamera.gameObject.SetActive(!isAiming);
            aimingCamera.gameObject.SetActive(isAiming);
        }
    }
}
