using UnityEngine;
using UnityEngine.UI;

public class MG_NuPogodiGameManager : MonoBehaviour
{
    public static MG_NuPogodiGameManager Instance;
    [field: SerializeField, Header("�������� ������ �� �������� �����")]
    public Wolf Wolf { get; private set; }

    [SerializeField, Header("������� ����� ������� ��� ��� ������"), Range(1, 6)]
    private int doneEggCountWin = 6;
    private int doneEggCount;

    [SerializeField, Header("�������� ������ ���������� ����")]
    private Image prefabDoneEgg;
    [SerializeField, Header("�������� ������, ��� ���������� ��������� ����")]
    private Transform panelDoneEgg;

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
        else // � ������ ������
        {
            SFX_Main.Instance.PlayAudio("WinGame"); //���� ������
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
