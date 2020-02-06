using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class transformCompensator : MonoBehaviour
{
    public Vector3 offset;
    Vector3 angles;
    GameObject hapticDevice;
    public GameObject bow;
    Collider stringCollider;
    // Start is called before the first frame update
    void Start()
    {
        hapticDevice = GameObject.Find("HapticDevice");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 bowPosition = bow.transform.position;
        Vector3 closestPoint = stringCollider.ClosestPoint(bowPosition);
        angles = hapticDevice.GetComponent<HapticPlugin>().stylusRotationWorld.eulerAngles;

        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x + offset.x, angles.y + 90.0f + offset.y, this.transform.eulerAngles.z + offset.z);

        this.transform.position = new Vector3(GameObject.Find("HapticString").transform.position.x,
                                                bowPosition.y,
                                                GameObject.Find("HapticString").transform.position.z);

        //this.transform.position = new Vector3(GameObject.Find("HapticString").transform.position.x,
        //                                        GameObject.Find("Grabber").transform.position.y,
        //                                        GameObject.Find("HapticString").transform.position.z);

        //this.transform.position = new Vector3(this.transform.position.x,
        //                                        GameObject.Find("Grabber").transform.position.y,
        //                                        this.transform.position.z);
    }
}
