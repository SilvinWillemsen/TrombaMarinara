using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransCompensator : MonoBehaviour
{
    public Vector3 rotOffset;
    Vector3 angles;
    GameObject hapticDevice;
    public GameObject bow;
    Collider stringCollider;
    // Start is called before the first frame update
    void Start()
    {
        hapticDevice = GameObject.Find("HapticDevice");
        stringCollider = GameObject.Find("HapticString").GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 bowPosition = bow.transform.position;
        Vector3 closestPoint = stringCollider.ClosestPoint(bowPosition);
        angles = hapticDevice.GetComponent<HapticPlugin>().stylusRotationWorld.eulerAngles;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x + rotOffset.x, angles.y + 90.0f + rotOffset.y, transform.eulerAngles.z + rotOffset.z);
        transform.position = new Vector3(closestPoint.x, bowPosition.y, closestPoint.z);
        print(bowPosition);
        //this.transform.position = new Vector3(GameObject.Find("HapticString").transform.position.x,
        //                                        GameObject.Find("Grabber").transform.position.y,
        //                                        GameObject.Find("HapticString").transform.position.z);

    }
}
