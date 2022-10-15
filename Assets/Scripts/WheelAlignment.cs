using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WheelAlignment : MonoBehaviour
{
    public GameObject wheelbase;
    public GameObject wheelGraphic;
    public WheelCollider wheelColl;
    public bool streeable;
    public float steeringAngle;
    private void Update()
    {
        alingMesheToCollider();
    }

    private void alingMesheToCollider()
    {
        if (streeable)
        {
            wheelColl.steerAngle = steeringAngle;
        }


        wheelGraphic.transform.eulerAngles = new Vector3(wheelbase.transform.eulerAngles.x,
            wheelbase.transform.eulerAngles.y, wheelbase.transform.eulerAngles.z);

        wheelGraphic.transform.rotation = wheelColl.transform.rotation;

        wheelGraphic.transform.Rotate(wheelColl.steerAngle / 60 * 360 * Time.deltaTime , 0, 0);

        updateWheel();
    }

    private void updateWheel()
    {
        Vector3 pos;
        Quaternion rot;
        wheelColl.GetWorldPose(out pos, out rot);
        wheelGraphic.transform.position = pos;
        wheelGraphic.transform.rotation = rot;
    }
}
