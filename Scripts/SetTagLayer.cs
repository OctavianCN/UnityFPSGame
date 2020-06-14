using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTagLayer : MonoBehaviour
{
    [SerializeField] private string tag = null;
    [SerializeField] private int layer = 0;
    void Start()
    {
        foreach(Transform child in this.transform)
        {
            if(tag!=null)
                child.gameObject.tag = tag;
            child.gameObject.layer = layer;
        }
    }

}
