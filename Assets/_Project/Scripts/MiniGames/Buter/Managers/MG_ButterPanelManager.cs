using UnityEngine;

public class MG_ButterPanelManager : MonoBehaviour
{
    public static MG_ButterPanelManager Instance;

    [SerializeField, Header("�������� ��� ������ ������� (Main)")]
    private GameObject[] panelsMain;

    [Header("�������� ������ �� ����� ��������")]
    public GameObject goAllFinals;


    public void Init()
    {
        Instance = this;
        if (goAllFinals != null)
        {
            if (goAllFinals.activeSelf)
            {
                goAllFinals.SetActive(false);
            }
        }
        for (int i = 0; i < panelsMain.Length; i++)
        {
            panelsMain[i].SetActive(false);
            if (!panelsMain[0].activeSelf)
            {
                panelsMain[0].SetActive(true);
            }
        }
    }

    /// <summary>
    /// �������� ����� ������ ��� �������� "final" ��� ������
    /// </summary>
    /// <param name="numPanel"></param>
    public void NextPanel(string numPanel)
    {
        switch (numPanel)
        {
            case "2":
                panelsMain[1].SetActive(true);
                panelsMain[1 - 1].SetActive(false);
                break;
            case "3":
                panelsMain[2].SetActive(true);
                panelsMain[2 - 1].SetActive(false);
                break;
            case "final":
                MG_ButerGameManager.Instance.IsFinal();
                break;
            default:
                Debug.LogError("����������� �������� ������!���� ������ ������, �� �������� � ������� MG_ButterPanelManager ����� ����!");
                break;
        }
    }
}
