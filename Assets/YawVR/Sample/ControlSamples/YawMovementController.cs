using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YawVR;
using static YawVR.SampleYawControllerDelegateImplementation;

[RequireComponent(typeof(Rigidbody))]

public class YawMovementController : MonoBehaviour
{
    YawController yawController; // reference to YawController
    MotionCompensation motionCompensation;
    SampleYawControllerDelegateImplementation sampleYawControllerDelegateImplementation; // reference to SampleYawControllerDelegateImplementation

    private Rigidbody rigid;


    [SerializeField]
    private Vector3 multiplier = new Vector3(3f, 1f, -2f);
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        yawController = YawController.Instance();
        motionCompensation = yawController.gameObject.GetComponent<MotionCompensation>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (sampleYawControllerDelegateImplementation.swapYawVO == false)
        {
            //float x, y, z;
            Vector3 vel = transform.InverseTransformVector(rigid.velocity);

            vel.x *= multiplier.x;
            vel.y *= multiplier.y;
            vel.z *= multiplier.z;

            Vector3 v = new Vector3(vel.z, 0f, vel.x);

            yawController.TrackerObject.SetRotation(v);
        }
        else
        {
            if (motionCompensation?.GetDevice() == MotionCompensation.enumYawPitchRollDevice.YawVRController)
            {
                yawController.TrackerObject.SetRotation(transform.localEulerAngles);
            }
            else if (motionCompensation?.GetDevice() == MotionCompensation.enumYawPitchRollDevice.LeftController
                  || motionCompensation?.GetDevice() == MotionCompensation.enumYawPitchRollDevice.RightController)
            {
                Vector3 eulerAngles = new Vector3();

                try
                {
                    eulerAngles = motionCompensation.GetOpenXRControllerTransform().localEulerAngles;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }

                if (null != eulerAngles)
                {
                    yawController.TrackerObject.SetRotation(eulerAngles);
                }
            }
        }
    }


}
