using blocks;
using TMPro;
using UnityEngine;

public class GameplayShop : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textWidth;
    [SerializeField] TextMeshProUGUI _textHeigth;
    
    private PlayerDeformation _playerDeformation;
    private CoinManager _coinManager;
    private PlayerData _playerData;

    public static GameplayShop Instance {  get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _coinManager = CoinManager.Instance;
        _playerData = GameDataManager.GetPlayerData();
        _playerDeformation = PlayerDeformation.Instance;
    }

    public void Init()
    {
        _playerData = GameDataManager.GetPlayerData();
        _textHeigth.text = _playerData.heightPrice.ToString();
        _textWidth.text = _playerData.widthPrice.ToString();
    }

    public void BuyWidth()
    {
        if (GameDataManager.GetCoin() >= _playerData.widthPrice)
        {
            _coinManager.SpendMoney(_playerData.widthPrice);
            _playerData.activeWidth += 25;
            _playerDeformation.SetWidth(_playerData.activeWidth);
            UpToPriceWidth();
        }
    }

    public void BuyHeigth()
    {
        if (GameDataManager.GetCoin() >= _playerData.heightPrice)
        {
            _coinManager.SpendMoney(_playerData.heightPrice);
            _playerData.activeHeight += 25;
            GameDataManager.SavePlayerData();
            _playerDeformation.SetHeigth(_playerData.activeHeight);
            UpToPriceHeigth();
        }
    }

    private void UpToPriceWidth()
    {
        _playerData.widthPrice += 2;
        _textWidth.text = _playerData.widthPrice.ToString();
        GameDataManager.SavePlayerData();
    }

    private void UpToPriceHeigth()
    {
        _playerData.heightPrice += 2;
        _textHeigth.text = _playerData.heightPrice.ToString();
        GameDataManager.SavePlayerData();
    }
}
