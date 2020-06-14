using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void Save(DataManager gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.bin";
        FileStream stream = new FileStream(path, FileMode.Create);
        GameData data = new GameData(gameData.player.GetComponent<PlayerHealth>(),gameData.player.GetComponent<PlayerInventory>(),gameData.door.GetComponent<DoorScripts>());
        formatter.Serialize(stream, data);
        
        stream.Close();
    }
    public static GameData Load()
    {
        string path = Application.persistentDataPath + "/data.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GameData data =  formatter.Deserialize(stream) as GameData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("File not find" + path);
            return null;
        }
    }
}
