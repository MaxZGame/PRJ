using System.Collections;
using UnityEngine;

public class MG_ButerGameManager : MonoBehaviour
{
    public static MG_ButerGameManager Instance;

    //��� �������� ������������ ������� ������ ����-����
    private MG_ButterPanelManager butterPanelManager;

    //��� ������� ������ �������
    private int badChoices = 0;

    [SerializeField, Header("������� ���������� ������� ����� ��� ���������?"), Range(1, 10)]
    private int badChoicesForBadFinal = 2;

    [SerializeField, Header("������� c����� ������������� ��������?"), Range(1, 10)]
    private float timeAnimation = 5f;

    [SerializeField, Header("�������� ������ � ������� �������")]
    private GameObject goGoodFinal;
    [SerializeField, Header("�������� ������ � ������ �������")]
    private GameObject goBadFinal;
    [SerializeField, Header("�������� ������ ������� ���� ����� �������� ��� ������ ��������")]
    private GameObject goGoToRoom;
    [SerializeField, Header("�������� �������, ������� ����� ��������� ��� ������ ��������")]
    private GameObject[] activatedGo;
    [SerializeField, Header("�������� �������, ������� ����� �������������� ��� ������ ��������")]
    private GameObject[] deActivatedGo;

    [SerializeField, Header("�������� ������ =�����=")]
    private MGNuPogodiButtonBack buttonBack;




    //����� �� ��������
    private bool isGoodFinal = false;
    private bool isBadFinal = false;

    private void Awake()
    {
        Instance = this;
        if (gameObject.activeSelf)
        {
            InitPanelManager();
            InitFinals();
            gameObject.SetActive(false);
        }
    }

    public void AddChoice(bool isGood)
    {
        if (!isGood)
        {
            badChoices++;
            Debug.Log($"����� ���������� ������� - {badChoices}");
        }
        else
        {
            Debug.Log("������ ������� �����");
        }
    }

    private void InitPanelManager()
    {
        butterPanelManager = GetComponent<MG_ButterPanelManager>();
        butterPanelManager.Init();
    }

    public void IsFinal()
    {
        //������ ��������
        if (badChoices >= badChoicesForBadFinal)
        {
            isBadFinal = true;
            badChoices = 0;
            StartCoroutine(FinalAnimation(goBadFinal));
        }
        //������� ��������
        else
        {
            isGoodFinal = true;
            badChoices = 0;
            StartCoroutine(FinalAnimation(goGoodFinal));
        }
    }

    private IEnumerator FinalAnimation(GameObject final)
    {
        MG_ButterPanelManager.Instance.goAllFinals.SetActive(true);
        final.SetActive(true);
        yield return new WaitForSeconds(timeAnimation);
        final.SetActive(false);
        if (isBadFinal)
        {
            goGoToRoom.SetActive(true);
            foreach (GameObject go in activatedGo)
            {
                go.SetActive(true);
            }
            foreach (GameObject go in deActivatedGo)
            {
                go.SetActive(false);
            }
            isBadFinal = false;
        }
        if (isGoodFinal)
        {
            buttonBack.Back();
            isGoodFinal = false;
        }
    }

    private void InitFinals()
    {
        if (goGoodFinal != null)
        {
            if (goGoodFinal.activeSelf)
            {
                goGoodFinal.SetActive(false);
            }
        }
        if (goBadFinal != null)
        {
            if (goBadFinal.activeSelf)
            {
                goBadFinal.SetActive(false);
            }
        }
    }
}
