using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] CoinManager _coinManager;
    [SerializeField] TextMeshProUGUI _textWidth;
    [SerializeField] TextMeshProUGUI _textHeigth;
    PLayerDeformation _playerDeformation;
    int priceWidth ;
    int priceHeigth;

    private void Start()
    {
        _playerDeformation = FindObjectOfType<PLayerDeformation>();
        priceWidth = Progress.Instance.PriceW;
        priceHeigth = Progress.Instance.PriceH;
        _textWidth.text = Progress.Instance.PriceW.ToString();
        _textHeigth.text = Progress.Instance.PriceH.ToString();

    }

    public void BuyWidth()
    {
        if (_coinManager.NumberOfCoins >= priceWidth)
        {
            _coinManager.SpendMoney(priceWidth);
            Progress.Instance.Coins = _coinManager.NumberOfCoins;
            Progress.Instance.Width += 25;
            _playerDeformation.SetWidth(Progress.Instance.Width);
            UpToPriceWidth();

        }
    }
    public void BuyHeigth()
    {
        if (_coinManager.NumberOfCoins >= priceHeigth)
        {
            _coinManager.SpendMoney(priceHeigth);
            Progress.Instance.Coins = _coinManager.NumberOfCoins;
            Progress.Instance.Height += 25;
            _playerDeformation.SetHeigth(Progress.Instance.Height);
            UpToPriceHeigth();
        }
    }
    private void UpToPriceWidth()
    {
        priceWidth += 2;
        _textWidth.text = priceWidth.ToString();
        Progress.Instance.PriceW = priceWidth;
    }
    public void SaveToPrice()
    {
        Progress.Instance.PriceW = priceWidth;
        Progress.Instance.PriceH = priceHeigth;
    }

    private void UpToPriceHeigth()
    {
        priceHeigth += 2;
        _textHeigth.text = priceHeigth.ToString();
        Progress.Instance.PriceH = priceHeigth;
    }

}
