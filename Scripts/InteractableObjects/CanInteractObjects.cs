using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanInteractObjects : MonoBehaviour
{
    [SerializeField] private GameObject jointObject;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            jointObject.GetComponent<FixedJoint>().connectedBody = null;
            collision.gameObject.GetComponent<PlayerInteractions>().SetReJoint(true);
        }
    }
}
