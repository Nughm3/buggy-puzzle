using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int level;

    public void SavePlayer() {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer() {
        StoredData data = SaveSystem.LoadPlayer();
        level = data.level;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) SavePlayer();
        if (Input.GetKeyDown(KeyCode.R)) LoadPlayer();
    }
}