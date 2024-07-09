using blocks;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    internal int levelsIncome;
    
    [SerializeField] private TMP_Text[] _texts;
    
    public static CoinManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameEvents.ResetLevelEvent.AddListener(ResetLevelsIncome);
        GameEvents.UpdateMoneyCount.AddListener(UpdateTexts);
    }

    public void AddOne()
    {
        AddCoins(1);
    }

    public void AddCoins(int amount)
    {
        GameDataManager.AddCoin(amount);
        UpdateTexts();
    }

    public void UpdateTexts()
    {
        foreach (var text in _texts) 
        {
            text.text = GameDataManager.GetCoin().ToString();
        }
    }
    
    public void SpendMoney (int value)
    {
        GameDataManager.AddCoin(-value);
        UpdateTexts();
    }

    private void ResetLevelsIncome()
    {
        levelsIncome = 0;
    }
}
