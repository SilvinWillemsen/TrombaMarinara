using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneObjectPositionFollower : MonoBehaviour
{
    public GameObject objectToFollow;
    public Vector3 offset; //calculated at 0.2

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + offset.x, objectToFollow.transform.position.y + offset.y, this.transform.position.z + offset.z);
    }
}
