using UnityEngine;
using UnityEngine.UI;

public class MG_NuPogodiGameManager : MonoBehaviour
{
    public static MG_NuPogodiGameManager Instance;
    [field: SerializeField, Header("Вставить объект со скриптом волка")]
    public Wolf Wolf { get; private set; }

    [SerializeField, Header("Сколько нужно собрать яиц для победы"), Range(1, 6)]
    private int doneEggCountWin = 6;
    private int doneEggCount;

    [SerializeField, Header("Вставить префаб собранного яйца")]
    private Image prefabDoneEgg;
    [SerializeField, Header("Вставить панель, где появляются собранные яйца")]
    private Transform panelDoneEgg;

    [SerializeField, Header("Вставить кнопку =назад=")]
    private MGNuPogodiButtonBack buttonBack;

    [SerializeField, Header("Вставить кнопку запуска мини-игры")]
    private GameObject goActivatedMG;

    [SerializeField, Header("Вставить аудио-менеджер мини-игры")]
    private MG_NuPogodi_AudioManager MG_NuPogodi_AudioManager;

    private void Awake()
    {
        Instance = this;
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }


    private void Update()
    {
        GameOver();
    }

    public void NewGame()
    {
        Wolf.NewWolf();
        UIHealth.Instance.NewHealth();
        doneEggCount = 0;
        Image[] doneEggs = panelDoneEgg.GetComponentsInChildren<Image>();
        foreach (Image image in doneEggs)
        {
            if (image.gameObject.tag != "UIPanel")
            {
                Destroy(image.gameObject);
            }
        }
    }

    public void CollectEgg()
    {
        if (doneEggCount < doneEggCountWin - 1)
        {
            doneEggCount += 1;
            Instantiate(prefabDoneEgg, panelDoneEgg);
        }
        else // В случае победы
        {
            SFX_Main.Instance.PlayAudio("WinGame"); //Звук победы
            doneEggCount = 0;
            Image[] doneEggs = panelDoneEgg.GetComponentsInChildren<Image>();
            foreach (Image image in doneEggs)
            {
                if (image.gameObject.tag != "UIPanel")
                {
                    Destroy(image.gameObject);
                }
            }
            buttonBack.Back();
            Destroy(gameObject);
            Destroy(goActivatedMG);
        }
    }

    private void GameOver()
    {
        if (Wolf.Health == 0)
        {
            SFX_Main.Instance.PlayAudio("GameOver"); //Звук проигрыша
            buttonBack.Back();
            Destroy(gameObject);
            Destroy(goActivatedMG);
        }
    }

    private void OnValidate()
    {
        if (Wolf == null)
        {
            Debug.LogError($"На {this} не назначен Wolf");
        }
        if (MG_NuPogodi_AudioManager == null)
        {
            Debug.LogError($"На {this} не назначен MG_NuPogodi_AudioManager");
        }
    }
}
