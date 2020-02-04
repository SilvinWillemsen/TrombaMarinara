using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AngleCompensator : MonoBehaviour
{

    Vector3 angles;
    GameObject hapticDevice;

    // Start is called before the first frame update
    void Start()
    {
        hapticDevice = GameObject.Find("HapticDevice");

        //this.transform.position = new Vector3 ( GameObject.Find("HapticString").transform.position.x, 
        //                                        this.transform.position.y, 
        //                                        GameObject.Find("HapticString").transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        angles = hapticDevice.GetComponent<HapticPlugin>().stylusRotationWorld.eulerAngles;
        this.transform.eulerAngles = new Vector3(0, angles.y + 90.0f, 0);

        this.transform.position = new Vector3 ( GameObject.Find("HapticString").transform.position.x, 
                                                GameObject.Find("Grabber").transform.position.y, 
                                                GameObject.Find("HapticString").transform.position.z);
    }
}
