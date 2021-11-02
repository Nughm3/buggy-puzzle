using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static void Save(PlayerData player) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saveFile.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        StoredData data = new StoredData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static StoredData Load() {
        string path = Application.persistentDataPath + "/saveFile.dat";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            StoredData data = formatter.Deserialize(stream) as StoredData;
            stream.Close();

            return data;
        }
        else {
            return null;
        }
    }
}
