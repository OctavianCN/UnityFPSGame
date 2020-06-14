using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public GameObject player;
    public GameObject pickups;
    public GameObject door;
    public static GameData data;
    public static GameData defaultData;
    private void Awake()
    {
       defaultData = new GameData(player.GetComponent<PlayerHealth>(), player.GetComponent<PlayerInventory>(),door.GetComponent<DoorScripts>());
        this.LoadGame();

    }
 
    public void SaveGame()
    {
       
        SaveSystem.Save(this);
        Debug.Log("Saved");
    }
    public void LoadGame()
    {
        if (!MainMenuScript.GetNewGame())
        {
            
            Vector3 pos = new Vector3(data.positions[0], data.positions[1], data.positions[2]);
          //  List<int> itemList = data.items;
           // GameObject[] items = pickups.GetComponentsInChildren<GameObject>(true);
            player.transform.position = pos;
            player.GetComponent<PlayerHealth>().SetPlayerHealth(data.health);
            
            List<int> itemList = new List<int>();
            itemList = data.items;
            Transform[] allItems = pickups.GetComponentsInChildren<Transform>(true);
            Transform[] items = new Transform[pickups.transform.childCount];
            int index = 0;
            foreach(Transform i in allItems)
            {
                if(i.parent == pickups.transform)
                {
                    items[index] = i;
                    index++;
                    i.gameObject.SetActive(true);
                }
            }
            foreach (Transform item in items)
            {
               // Debug.Log(item.gameObject.name);
                 foreach (int itemId in itemList)
                 {
                   
                    if (item.gameObject.GetComponent<Pickup>().GetId()==itemId)
                     {
                       
                        player.GetComponent<PlayerPickup>().PickItem(item.gameObject);
                       // Debug.Log("Da");
                      
                     }
                  //  Debug.Log(item.gameObject.GetComponent<Pickup>().GetId());
                 }
            }
            foreach(Transform i in items)
            {
                i.gameObject.SetActive(false);
            }
            if(data.locked == false)
            {
                door.GetComponent<DoorScripts>().UnlockDoor();
                door.GetComponent<DoorScripts>().getKey().SetActive(false);
                door.GetComponent<DoorScripts>().getAnimator().SetTrigger("Unlocked");
            }
        }
        else
        {
            
            Vector3 pos = new Vector3(defaultData.positions[0], defaultData.positions[1], defaultData.positions[2]);
            player.transform.position = pos;
            player.GetComponent<PlayerHealth>().SetPlayerHealth(defaultData.health);  
        }
    }
}
