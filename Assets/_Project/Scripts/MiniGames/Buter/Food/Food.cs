using TMPro;
using UnityEngine;

public class Food : MonoBehaviour
{
    [field: SerializeField, Header("�������� SCO Food")]
    public FoodSO FoodSO { get; private set; }

    /// <summary>
    /// ��� �������� �����������
    /// </summary>
    private TextMeshProUGUI tmpFoodName;

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        tmpFoodName = GetComponentInChildren<TextMeshProUGUI>();
        tmpFoodName.text = FoodSO.FoodName;
    }

    public void Choice(string numChoice)
    {
        SFX_Main.Instance.PlayAudio("Select"); //���� ��� ��������� ������
        MG_ButerGameManager.Instance.AddChoice(FoodSO.IsGood);
        MG_ButterPanelManager.Instance.NextPanel(numChoice);
    }
}
