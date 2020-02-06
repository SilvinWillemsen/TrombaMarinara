using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public Vector3 contactPoint;
    public Vector3 localContactPoint;
    public bool touching = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //print("local contact point is: " + localContactPoint);
    }

    void OnCollisionStay(Collision collision)
    {
       
        foreach (ContactPoint contact in collision.contacts)
        {
            contactPoint = contact.point;
            //print(contact.thisCollider.name + " hit " + contact.otherCollider.name + ",and the collision force is: " + collision.relativeVelocity.magnitude);
            localContactPoint = transform.InverseTransformPoint(collision.contacts[0].point);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        //stouching = false;
    }


    private void OnTriggerStay(Collider other)
    {
        touching = true;
    }
    private void OnTriggerExit(Collider other)
    {
        touching = false;
    }
}
