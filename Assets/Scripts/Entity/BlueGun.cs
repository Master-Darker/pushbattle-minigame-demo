using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGun : Gun
{
    public float thresholdX;
    public float thresholdZ;
    private Camera mainCamera;
    private float cameraZ;

    protected override void Start()
    {
        mainCamera = Camera.main;
        thresholdX = Mathf.Abs(thresholdX);
        thresholdZ = Mathf.Abs(thresholdZ);
        cameraZ = mainCamera.WorldToScreenPoint(new Vector3(0, 0, -thresholdZ)).z;
        base.Start();
    }

    protected override void OnUpdate()
    {
        var cameraPoint = Input.mousePosition;
        cameraPoint.z = cameraZ;
        var worldPoint = mainCamera.ScreenToWorldPoint(cameraPoint);
        var pointX = Mathf.Clamp(worldPoint.x, -thresholdX, thresholdX);
        var pointZ = Mathf.Clamp(worldPoint.z, -thresholdZ, thresholdZ);
        var target = new Vector3(pointX, transform.position.y, pointZ);
        var dir = target - transform.position;
        transform.forward = dir.normalized;
    }
}
