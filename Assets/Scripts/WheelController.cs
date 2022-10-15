using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public WheelAlignment[] steerableWheels;
    public float breakPower;
    public float horizontal;
    public float vertical;
    public float wheelRotateSpeed;
    public float wheelSteerinAngle;
    public float wheelAcceleration;
    public float wheelMaxSpeed;
    public Rigidbody rb;

    [Header("Lighting")]
    public MeshRenderer mesh;
    public Material orginalRed;
    public Material orginalWhite;
    public Material Red;
    public Material White;


    private void Update()
    {
        Control();
        
    }

    private void Control()
    {
        for (int i = 0; i < steerableWheels.Length; i++)
        {
            steerableWheels[i].steeringAngle = Mathf.LerpAngle(steerableWheels[i].steeringAngle, 
                0, Time.deltaTime * wheelRotateSpeed);
            steerableWheels[i].wheelColl.motorTorque = Mathf.Lerp(steerableWheels[i].wheelColl.motorTorque
                , 0, Time.deltaTime * wheelAcceleration);

            // ýnput
            horizontal = Input.GetAxis("Horizontal")*-1;
            vertical = Input.GetAxis("Vertical");

            if (vertical>.1f)
            {
                steerableWheels[i].wheelColl.motorTorque = Mathf.Lerp(steerableWheels[i].
                    wheelColl.motorTorque, wheelMaxSpeed, Time.deltaTime * wheelAcceleration);
            }
            if (vertical<-.1f)
            {
                LigthControl(true);
                steerableWheels[i].wheelColl.motorTorque = -Mathf.Lerp(steerableWheels[i].
                wheelColl.motorTorque, wheelMaxSpeed, Time.deltaTime * wheelAcceleration);
                rb.drag = .3f;
            }
            else
            {
                LigthControl(false);
                rb.drag = 0;
            }

            if (horizontal>.1f)
            {
                steerableWheels[i].steeringAngle = Mathf.LerpAngle(steerableWheels[i]
                    .steeringAngle, -wheelSteerinAngle, Time.deltaTime * wheelRotateSpeed);
            }
            if (horizontal < -.1f)
            {
                steerableWheels[i].steeringAngle = Mathf.LerpAngle(steerableWheels[i].
                wheelColl.steerAngle, wheelSteerinAngle, Time.deltaTime * wheelRotateSpeed);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                LigthControl(true);
                steerableWheels[i].wheelColl.brakeTorque =Mathf.Lerp(0,breakPower,Time.deltaTime*breakPower*10);
            }
            else if(Input.GetKeyUp(KeyCode.Space))
            {
                LigthControl(true);
                steerableWheels[i].wheelColl.brakeTorque = Mathf.Lerp(breakPower, 0, Time.deltaTime * breakPower*100);
            }


        }
    }
    private void LigthControl(bool active)
    {
        if (active)
        {
            mesh.material = Red;
            mesh.material = White;
        }
        else
        {
            mesh.material = orginalRed;
            mesh.material = orginalWhite;

        }
    }
}
