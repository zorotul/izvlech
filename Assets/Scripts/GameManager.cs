using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _startMenu;
    [SerializeField] TextMeshProUGUI _levelText;
    [SerializeField] GameObject _finishWindowWin;
    [SerializeField] GameObject _finishWindowDefeat;
    [SerializeField] CoinManager _coinManager;
    [SerializeField] Shop _shop;



    private void Start()
    {
        _levelText.text = SceneManager.GetActiveScene().name;


    }

    public void Play()
    {
        _startMenu.SetActive(false);
        FindObjectOfType<PlayerBehaviourScript>().Play();
    }
    public void ShowfinishWindowWin()
    {
        _finishWindowWin.SetActive(true);
    }
    public void ShowfinishWindowDefeat()
    {
        _finishWindowDefeat.SetActive(true);
    }
    public void NextLevel()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        if (next  < SceneManager.sceneCountInBuildSettings)
        {
            _shop.SaveToPrice();
            _coinManager.SaveToCoins();
            SceneManager.LoadScene(next);

        }

    }
    public void ReLevel()
    {
        _shop.SaveToPrice();
        _coinManager.SaveToCoins(); 
        int next = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(next);


    }

}

