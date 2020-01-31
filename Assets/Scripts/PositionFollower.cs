using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFollower : MonoBehaviour
{
    public GameObject RightHandAnchor;
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(RightHandAnchor.transform.position.x, 0.74f, RightHandAnchor.transform.position.z + offset);
    }
}
