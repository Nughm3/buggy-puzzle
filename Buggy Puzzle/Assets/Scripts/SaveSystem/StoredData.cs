[System.Serializable]
public class StoredData
{
    public int level;

    public StoredData(PlayerData player)
    {
        level = player.level;
    }

}