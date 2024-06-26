using System;
using DG.Tweening;
using Flatformer.GameData;
using System.Collections;
using blocks;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using YG;
using Button = UnityEngine.UI.Button;


namespace ShopMechanics
{
    public class ShopManager : MonoBehaviour
    {
        [Header("reference")]
        [SerializeField] private CharacterShopData _activeShopData;
        [SerializeField] private AudioClip _purcharAudio;
        [Header("UI elements")] 
        [SerializeField] private ShopItemsGenerator _shopItemsGenerator;
        [SerializeField] private ShopSkinControl _shopSkinControl;
        [SerializeField] private GameObject _shopUI;
        [SerializeField] private Button _closeShopButton;
        [SerializeField] private Button _rewardAdsButton;
        [SerializeField] private TextMeshProUGUI _noEnoughCoinsText;
        
        private CharacterItem[] _shopItems; 
        private int _newItemIndex;
        private int _preousItemIndex;
        private int _purchaseItemIndex;
        
        public static readonly MultiText UnlockLevelText = new ("Разблокируется на уровне ", "Unlock At Level ");

        public static ShopManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            YandexGame.RewardVideoEvent += OnCompleteShopAds;
            YandexGame.RewardVideoEvent += OnCompleteAds;
            CharacterItem.BuySkinEvent.AddListener(OnPurchaseItem);
            CharacterItem.SelectSkinEvent.AddListener(OnSelectItem);
        }
        
        private void OnDisable()
        {
            YandexGame.RewardVideoEvent -= OnCompleteShopAds;
            YandexGame.RewardVideoEvent -= OnCompleteAds;
        }

        private void OnCompleteShopAds(int obj)
        {
            if(obj != (int) VideoAdsId.ShopReward) return;
            _shopItems[_purchaseItemIndex].OnCompleteAds();
        }

        public void Init()
        {
            _shopItemsGenerator.Init();
            _shopItems = _shopItemsGenerator.Items.ToArray();
            _shopSkinControl.Init();
            AddEvents();
            _shopSkinControl.ChangeSkin(GameDataManager.GetCharacterIndex());
            SelectItem(GameDataManager.GetCharacterIndex());
            CloseShop();
        }

        public void ReselectItem()
        {
            SelectItem(GameDataManager.GetCharacterIndex());
        }

        public void DeselectActiveSelectItem()
        {
            _shopItems[_newItemIndex].DeSelectItem();
        }
        
        private void OnSelectItem(int index)
        {
            SelectItem(index);
            GameDataManager.SetCharacterIndex(index);
        }
        
        private void SelectItem(int newIndex)
        {
            _preousItemIndex = _newItemIndex;
            _newItemIndex = newIndex;
            
            CharacterItem preCharacter = _shopItems[_preousItemIndex];
            CharacterItem newCharacter = _shopItems[_newItemIndex];

            preCharacter.DeSelectItem();
            newCharacter.SelectItem();
            _shopSkinControl.ChangeSkin(newIndex);
        }
        private void OnPurchaseItem(int index)
        {
            Debug.Log("Purchase: " + index);
            if(_activeShopData.GetCharacter(index).isNeedAds)
            {
                _purchaseItemIndex = index;
                YandexGame.RewVideoShow((int)VideoAdsId.ShopReward);
            }
            else
            {
                Character character = _activeShopData.GetCharacter(index);
                if(GameDataManager.CanSpenCoin(character.price))
                {
                    _shopItems[index].SetPurchaseAsCharacter();
                    _shopItems[index].OnSelectItem();

                    GameDataManager.SpendCoin(character.price);
                    GameDataManager.AddPurchaseCharacter(index);

                    // SoundManager.instance.PlayAudioSound(purcharAudio); TODO audio
                    GameSharedUI.instance.UpdateCoinsTextUI();
                }
                else
                {
                    AnimationNoMoreCoinsText();
                    AnimationShakeItem(_shopItems[index].transform);
                }
            }
        }

        private void AnimationNoMoreCoinsText()
        {
            _noEnoughCoinsText.transform.DOComplete();
            _noEnoughCoinsText.DOComplete();

            _noEnoughCoinsText.transform.DOShakePosition(3f, new Vector3(5f, 0, 0), 10, 0);
            _noEnoughCoinsText.DOFade(1f, 3f).From(0f).OnComplete(() =>
            {
                _noEnoughCoinsText.DOFade(0f, 1f);
            });
        }

        private static void AnimationShakeItem(Transform transform)
        {
            transform.DOComplete();
            transform.DOShakePosition(1f, new Vector3(10f, 0, 0), 10, 0).SetEase(Ease.Linear);
        }

        private void AddEvents()
        {
            _closeShopButton.onClick.RemoveAllListeners();
            _closeShopButton.onClick.AddListener(() =>
            {
                // SoundManager.instance.PlayAudioSound(SoundManager.instance.buttonAudio); TODO audio
                // GameManager.instance.ReplayGame();
                // this.PostEvent(EventID.IsPlayGame, true);
                // this.PostEvent(EventID.Home);
                CloseShop();
            });

            _rewardAdsButton.onClick.RemoveAllListeners();
            _rewardAdsButton.onClick.AddListener(() =>
            {
                // SoundManager.instance.PlayAudioSound(SoundManager.instance.buttonAudio); TODO audio
                YandexGame.RewVideoShow((int) VideoAdsId.Reward2);
            });
        }

        private void OnCompleteAds(int id)
        {
            if(id != (int) VideoAdsId.Reward2) return;
            StartCoroutine(DelayCompleteAds(5));
        }
        private IEnumerator DelayCompleteAds(float time)
        {
            var t = time;
            while (t > 0)
            {
                yield return new WaitForEndOfFrame();
                t--;
                if (t == 0)
                    GameDataManager.AddCoin(150);
            }
            GameSharedUI.instance.UpdateCoinsTextUI();
        }

        private void CloseShop() 
            => _shopUI.SetActive(false);
    }
}
