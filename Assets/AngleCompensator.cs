using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AngleCompensator : MonoBehaviour
{
    public Vector3 offset;
    Vector3 angles;
    GameObject hapticDevice;

    // Start is called before the first frame update
    void Start()
    {
        hapticDevice = GameObject.Find("HapticDevice");
    }

    // Update is called once per frame
    void Update()
    {
        angles = hapticDevice.GetComponent<HapticPlugin>().stylusRotationWorld.eulerAngles;

        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x + offset.x, angles.y + 90.0f + offset.y, this.transform.eulerAngles.z + offset.z);

        this.transform.position = new Vector3(  GameObject.Find("HapticString").GetComponent<CapsuleCollider>().ClosestPoint(GameObject.Find("Grabber").transform.position).x, // the x coords of the closest point on the string
                                                GameObject.Find("Grabber").transform.position.y,
                                                GameObject.Find("HapticString").GetComponent<CapsuleCollider>().ClosestPoint(GameObject.Find("Grabber").transform.position).z); // the x coords of the closest point on the string
    }
}
