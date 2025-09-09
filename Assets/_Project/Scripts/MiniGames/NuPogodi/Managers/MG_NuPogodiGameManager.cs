using TMPro;
using UnityEngine;

public class MG_NuPogodiGameManager : MonoBehaviour
{
    public static MG_NuPogodiGameManager Instance;
    [field: SerializeField, Header("Вставить объект со скриптом волка")]
    public Wolf Wolf { get; private set; }

    [SerializeField, Header("Вставить UIHealth")]
    private UIHealth uIHealth;

    [SerializeField, Header("Вставить ТМП счетчика яиц")]
    private TextMeshProUGUI tmpCountEgg;

    [SerializeField, Header("Сколько нужно собрать яиц для победы"), Range(1, 100)]
    private int doneEggCountWin = 6;
    private int doneEggCount;


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
        uIHealth.NewHealth();
        doneEggCount = 0;
        tmpCountEgg.text = $"{doneEggCount}/{doneEggCountWin}";
    }

    public void CollectEgg()
    {
        if (doneEggCount < doneEggCountWin - 1)
        {
            doneEggCount += 1;
            tmpCountEgg.text = $"{doneEggCount}/{doneEggCountWin}";
        }
        else // В случае победы
        {
            SFX_Main.Instance.PlayAudio("WinGame"); //Звук победы
            doneEggCount = 0;
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
