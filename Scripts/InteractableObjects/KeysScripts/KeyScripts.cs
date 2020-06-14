using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScripts : MonoBehaviour
{
    [SerializeField] private GameObject door;

    public void Destroy()
    {
        door.GetComponent<DoorScripts>().UnlockDoor();
        Destroy(this.gameObject);
    }
}
