using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EveryDayRewardItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _image;
    [SerializeField] private Button _getRewardedButton;

    public void Init(bool canBeCatched, string text, Sprite sprite)
    {
        _image.sprite = sprite;
        _text.text = text;
        _getRewardedButton.gameObject.SetActive(canBeCatched);
    }

    public void GetRewardButtonEvent()
    {

    }
}