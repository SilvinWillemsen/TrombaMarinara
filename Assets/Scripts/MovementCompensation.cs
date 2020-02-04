using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MovementCompensation : MonoBehaviour
{
    public GameObject hapticOmniGrabber;
    public GameObject hapticString;
    public GameObject tiltMother;
    public GameObject rightHandAnchor;
    public AudioMixer mastermixer;
    public HapticSurface hapticSurface;

    GameObject hapticDevice;
    public float speedScalar = 0.05f;
    public float forceScalar = 0.05f;
    Vector3 bowStringContactPoint;
    Vector3 oldPosition;
    Vector3 angles;
    Vector3 stylusWorldPosition;

    //distance calculator variables
    Vector3 sourceObjectPosition;
    Collider targetCollider;

    float bowingForce;
    float vel;
    float bowNutDistance;
    float bowingSpeed;
    float offsetDistance;
    float maxForce = 0.2f;
    float maxStiffness = 0.2f;

    bool inFrontOfBow = false;

    void Start()
    {
        oldPosition = transform.position;
        hapticDevice = GameObject.Find("HapticDevice");

        //position-scale relationship
        Vector3 newPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, 0.5f * this.transform.localScale.z - 0.02f);
        this.transform.localPosition = newPosition;

        //Objects for detecting position between the bow and the nut;
        sourceObjectPosition = GameObject.Find("Nut").transform.position;
        targetCollider = GameObject.Find("Strings").GetComponent<CapsuleCollider>();

    }

    // Update is called once per frame
    void Update()
    {

        //angles = hapticDevice.GetComponent<HapticPlugin>().stylusRotationWorld.eulerAngles;
        bowingForce = hapticDevice.GetComponent<HapticPlugin>().touchingDepth * forceScalar;
        Vector3 closestPoint = targetCollider.ClosestPoint(sourceObjectPosition);
        bowNutDistance = Vector3.Distance(sourceObjectPosition, closestPoint);


        stylusWorldPosition = hapticDevice.GetComponent<HapticPlugin>().stylusPositionWorld;

        //Audio Manipulation
        if (mastermixer.GetFloat("Velocity", out vel))
        {
            float stylusXVel = hapticDevice.GetComponent<HapticPlugin>().stylusVelocityRaw.x;
            float stylusYvel = hapticDevice.GetComponent<HapticPlugin>().stylusVelocityRaw.z;
            bowingSpeed = Mathf.Sign(stylusXVel) * Mathf.Sqrt(stylusXVel * stylusXVel + stylusYvel * stylusYvel) * speedScalar;

            //Assign values to the Trombamarina Sound Model via the AudioMixer
            mastermixer.SetFloat("Velocity", bowingSpeed + 0.5f);
            mastermixer.SetFloat("Force", bowingForce);
            mastermixer.SetFloat("Position", bowNutDistance);

        }

        //OUTDATED: Get contact point from between the string and the bow
        //CollisionDetector collisionDetector = hapticString.GetComponent<CollisionDetector>();
        //bowStringContactPoint = collisionDetector.contactPoint;

        //Distance between string and pen
        float xDist = hapticOmniGrabber.transform.position.x - hapticString.transform.position.x;
        float zDist = hapticOmniGrabber.transform.position.z - hapticString.transform.position.z;
        float xzDist = -Mathf.Sqrt(xDist * xDist + zDist * zDist);

        //print("Collision compensator z: " + this.transform.position.z + ", target z: " + target.transform.position.z);

        //Check for direction collision between the bow vs String a
        if (this.transform.position.z > hapticOmniGrabber.transform.position.z)
        {
            inFrontOfBow = false;
            if (bowingForce / forceScalar < maxForce)
                hapticSurface.hlStiffness = Remap(bowingForce / forceScalar, 0.0f, maxForce, 0.0f, maxStiffness);
            else
                hapticSurface.hlStiffness = maxStiffness;
        } else if ((this.transform.position.z - this.transform.position.z * 0.6) < hapticOmniGrabber.transform.position.z) {
            inFrontOfBow = true;
        }
        this.transform.localPosition = new Vector3(xzDist - (inFrontOfBow ? 1f : 0.0f), stylusWorldPosition.y + 0.25f, this.transform.localPosition.z);
       
        //OUTDATED
        //offsetDistance = (target.transform.position.x - contactPoint.x);
        //offsetPos = new Vector3(offsetDistance * -1.0f, target.transform.position.y, target.transform.position.z);
        //if (GameObject.Find("HapticString").GetComponent<CollisionDetector>().touching)
        //{
        //    offsetDistance = (Mathf.Tan((angles.y - 270) * Mathf.Deg2Rad)) * (target.transform.position.x - contactPoint.x);
        //    offsetPos = new Vector3(target.transform.position.x, target.transform.position.y, offsetDistance * -1.0f);
        //}
        //else
        //{
        //    offsetPos = new Vector3(100, 100, 100);
        //}
        //this.transform.position = offsetPos;

        // set the local x-position of the collisioncompensator to its distance to the string in the x-z plane
        // still need to implement the z coordinate (but it might not be necessary)


        //this.transform.position += new Vector3(0.0f, stylusWorld.y, 0.0f);

        //Old method of moving the colision box
        //Vector3 offsetPos;
        //offsetDistance = (Mathf.Tan((angles.y - 270) * Mathf.Deg2Rad)) * (target.transform.position.x - contactPoint.x);
    }
    float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
    
}
