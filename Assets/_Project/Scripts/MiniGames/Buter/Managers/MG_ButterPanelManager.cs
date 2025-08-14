using UnityEngine;

public class MG_ButterPanelManager : MonoBehaviour
{
    public static MG_ButterPanelManager Instance;

    [SerializeField, Header("Вставить все панели готовки (Main)")]
    private GameObject[] panelsMain;

    [Header("Вставьте объект со всеми финалами")]
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
    /// Вставьте номер панели или напишите "final" для финала
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
                Debug.LogError("Установлена неверная панель!Если хотите больше, то добавьте в скрипте MG_ButterPanelManager новый кейс!");
                break;
        }
    }
}
