using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGun : Gun
{
    public float rotateSpeed;
    public float halfAngle;
    public Props props;
    public Transform baffle;
    private Quaternion nextRotation;

    protected override void Start()
    {
        base.Start();
        nextRotation = transform.rotation;
        props.onReset += SetNextRotation;
    }

    protected override void OnUpdate()
    {
        if (transform.rotation != nextRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, nextRotation, Time.deltaTime * rotateSpeed);
        }
    }

    private void SetNextRotation()
    {
        var splitors = props.GetSplitorsOnTheSide(baffle.position.z, transform.position.z);
        var nextTarget = Vector3.zero;
        var multiple = 0;
        foreach (var item in splitors)
        {
            if (multiple < item.multiple)
            {
                multiple = item.multiple;
                nextTarget = item.transform.position;
            }
        }
        var nextDir = nextTarget - transform.position;
        nextRotation = Quaternion.LookRotation(nextDir);
    }
}
