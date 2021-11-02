using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int level;

    public void Save() {
        SaveSystem.Save(this);
    }

    public void Load() {
        StoredData data = SaveSystem.Load();
        level = data.level;
    }

    public void CreateSave() {
        level = 1;
        Save();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) Save();
        if (Input.GetKeyDown(KeyCode.R)) Load();
    }
}