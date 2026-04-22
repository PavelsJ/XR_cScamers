using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [Header("State")]
    public bool isDead;
    
    [Header("ANIMATED MODEL PARTS")] 
    public Transform[] animatedTransforms;

    [Header("PHYSICAL MODEL PARTS")] 
    public ConfigurableJoint[] joints;
    public Rigidbody[] rbs;

    private Quaternion[] initialLocalRotations;
    void Start()
    {
        initialLocalRotations = new Quaternion[joints.Length];

        // Save the initial local rotations of each joint
        for (int i = 0; i < joints.Length; i++)
        {
            initialLocalRotations[i] = joints[i].transform.localRotation;
        }
    }

    void FixedUpdate()
    {
        // Set the target rotation of each joint to match its corresponding animated transform
        for (int i = 0; i < joints.Length; i++)
        {
            if (i < animatedTransforms.Length)
            {
                joints[i].SetTargetRotationLocal(animatedTransforms[i].localRotation, initialLocalRotations[i]);
            }
        }
    }

    private void Update()
    {
        if (isDead)
        {
            rbs[^1].constraints &= ~RigidbodyConstraints.FreezePositionZ;
            rbs[^1].freezeRotation = false;

            foreach (ConfigurableJoint joint in joints)
            {
                joint.angularXMotion = ConfigurableJointMotion.Limited;
                joint.angularYMotion = ConfigurableJointMotion.Limited;
                joint.angularZMotion = ConfigurableJointMotion.Limited;
                
                JointDrive drive = new JointDrive
                {
                    positionSpring = 0,
                };
                
                joint.angularXDrive = drive;
                joint.angularYZDrive = drive;

            }
        }
    }
}