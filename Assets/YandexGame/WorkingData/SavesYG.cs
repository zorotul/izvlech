
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public int allMoney;
        public int mutedMusic;
        public int mutedUIMusic;
        public int freeSpin;
        public string playerData;
        public string shopData;
    }
}
