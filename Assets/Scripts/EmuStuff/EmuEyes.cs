using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmuEyes : MonoBehaviour
{

    public bool Sprinting, CameraInputEnabled;
    public float InputSensitivity, MaxCamInterpolateDist, MinCamInterpolateDist, FovChangeSpeed, MaxFovChange, FovCalcMultiplier, FovMod;
    [SerializeField]
    private Transform cameraPivot;
    private float xRot, fov, fovSmoothener;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private EmuMovement movement;

    private void Start()
    {

        fov = Camera.main.fieldOfView;
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {

        if(CameraInputEnabled)
        {

            player.Rotate(Vector3.up, Input.GetAxisRaw("Mouse X") * InputSensitivity);
            xRot -= Input.GetAxisRaw("Mouse Y") * InputSensitivity;
            xRot = Mathf.Clamp(xRot, -90, 90);
            cameraPivot.localEulerAngles = new Vector3(xRot, 0, 0);

        }

        float targetFov = Mathf.Clamp((float)Mathf.Pow(movement.Velocity.magnitude, FovCalcMultiplier), 0, MaxFovChange);
        float fovChange = Time.deltaTime * Mathf.Clamp(Mathf.Abs(targetFov - fovSmoothener), 0.05f, MaxFovChange) * FovChangeSpeed;

        if(targetFov > fovSmoothener)
            fovSmoothener = Mathf.Clamp(fovSmoothener + fovChange, fovSmoothener, targetFov);
        else if(targetFov < fovSmoothener)
            fovSmoothener = Mathf.Clamp(fovSmoothener - fovChange, targetFov, fovSmoothener);

        Camera.main.fieldOfView = fov + fovSmoothener + FovMod;

    }

}
