using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XYPosFollower : MonoBehaviour
{
    public GameObject objectToFollow;
    public Vector3 offset; //calculated at 0.2
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(objectToFollow.transform.position.x + offset.x, this.transform.position.y + offset.y, objectToFollow.transform.position.z + offset.z);

        if (Input.GetKeyDown("space"))
            GetComponent<XYPosFollower>().enabled = false;
    }
}
