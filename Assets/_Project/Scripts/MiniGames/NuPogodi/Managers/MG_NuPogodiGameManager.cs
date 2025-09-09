using TMPro;
using UnityEngine;

public class MG_NuPogodiGameManager : MonoBehaviour
{
    public static MG_NuPogodiGameManager Instance;
    [field: SerializeField, Header("�������� ������ �� �������� �����")]
    public Wolf Wolf { get; private set; }

    [SerializeField, Header("�������� UIHealth")]
    private UIHealth uIHealth;

    [SerializeField, Header("�������� ��� �������� ���")]
    private TextMeshProUGUI tmpCountEgg;

    [SerializeField, Header("������� ����� ������� ��� ��� ������"), Range(1, 100)]
    private int doneEggCountWin = 6;
    private int doneEggCount;


    [SerializeField, Header("�������� ������ =�����=")]
    private MGNuPogodiButtonBack buttonBack;

    [SerializeField, Header("�������� ������ ������� ����-����")]
    private GameObject goActivatedMG;

    [SerializeField, Header("�������� �����-�������� ����-����")]
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
        else // � ������ ������
        {
            SFX_Main.Instance.PlayAudio("WinGame"); //���� ������
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
            SFX_Main.Instance.PlayAudio("GameOver"); //���� ���������
            buttonBack.Back();
            Destroy(gameObject);
            Destroy(goActivatedMG);
        }
    }

    private void OnValidate()
    {
        if (Wolf == null)
        {
            Debug.LogError($"�� {this} �� �������� Wolf");
        }
        if (MG_NuPogodi_AudioManager == null)
        {
            Debug.LogError($"�� {this} �� �������� MG_NuPogodi_AudioManager");
        }
    }
}
