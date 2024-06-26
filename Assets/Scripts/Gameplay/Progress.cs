using blocks;
using UnityEngine;

public class Progress : MonoBehaviour
{
    public static Progress Instance;
    
    private void Awake()
    {
        Instance = this;
        GameEvents.ResetLevelEvent.AddListener(ResetDatas);
    }

    private void ResetDatas()
    {
        var _playerData = GameDataManager.GetPlayerData();
        _playerData.activeHeight = 0;
        _playerData.activeWidth = 0;
    }
}
