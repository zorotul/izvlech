using blocks;
using TMPro;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _finishPosition;
    [SerializeField] private GameObject _finishPrefab;
    [SerializeField] private Transform _startPlayerPosition;
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private GameObject _finishWindowWin;
    [SerializeField] private GameObject _finishWindowDefeat;
    [SerializeField] private CoinManager _coinManager;
    [SerializeField] private GameplayShop _shop;

    private GameObject _activeFinishObject;

    public static GameManager Instance;

    private void Awake()
    {
        Application.targetFrameRate = 30;
        QualitySettings.vSyncCount = 2;
        Instance = this;
        GameEvents.ResetLevelEvent.AddListener(Init);
    }

    public void StartGame()
    {
        LevelsControl.Instance.LoadFirstLevel();
        GameEvents.ResetLevelEvent.Invoke();
    }

    private void Init()
    {
        _finishWindowDefeat.SetActive(false);
        _finishWindowWin.SetActive(false);
        _startMenu.SetActive(true);
        if (_activeFinishObject != null) Destroy(_activeFinishObject);
        _activeFinishObject = Instantiate(_finishPrefab, _finishPosition.position, Quaternion.identity);
        PlayerBehaviour.Instance.transform.position = _startPlayerPosition.position;
        _levelText.text = (GameDataManager.GetLevel() + 1).ToString();
        LevelsControl.Instance.RestartLevel();
    }

    public void Play()
    {
        GameEvents.StartGameEvent.Invoke();
        _startMenu.SetActive(false);
        PlayerBehaviour.Instance.Play();
    }

    public void ShowFinishWindowWin()
    {
        _finishWindowWin.SetActive(true);
    }

    public void ShowFinishWindowDefeat()
    {
        _finishWindowDefeat.SetActive(true);
    }

    public void NextLevel()
    {
        LevelsControl.Instance.LoadNextLevel();
        GameEvents.ResetLevelEvent.Invoke();
    }

    public void ReLevel()
    {
        LevelsControl.Instance.RestartLevel();
        GameEvents.ResetLevelEvent.Invoke();
    }

    public void HomeButtonEvent()
    {
        _startMenu.SetActive(true);
    }
}