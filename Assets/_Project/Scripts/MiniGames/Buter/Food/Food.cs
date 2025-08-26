using TMPro;
using UnityEngine;

public class Food : MonoBehaviour
{
    [field: SerializeField, Header("Вставить SCO Food")]
    public FoodSO FoodSO { get; private set; }

    /// <summary>
    /// Тмп названия ингредиента
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
        SFX_Main.Instance.PlayAudio("Select"); //Звук при активации выбора
        MG_ButerGameManager.Instance.AddChoice(FoodSO.IsGood);
        MG_ButterPanelManager.Instance.NextPanel(numChoice);
    }
}
