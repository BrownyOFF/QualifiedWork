using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour{

    public static void SavePlayer(PlayerClass player)
    {
        Debug.Log("SavePlayer");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.json";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveClass data = new SaveClass(player);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveClass LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.json";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveClass data = formatter.Deserialize(stream) as SaveClass;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " +path);
            return null;
        }
    }
}
