using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoAxisPositionFollower : MonoBehaviour
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

        //transform.position = new Vector3(objectToFollow.transform.position.x + offset.x, objectToFollow.transform.position.y + offset.y, objectToFollow.transform.position.z + offset.z);
        //if (OVRInput.Get(OVRInput.Button.Two))
        //{
        //    print("button b pressed");
        //    gameObject.GetComponent<PositionFollower>().enabled = false;
        //}

        if (Input.GetKey("space"))
            gameObject.GetComponent<TwoAxisPositionFollower>().enabled = false;

    }
}
