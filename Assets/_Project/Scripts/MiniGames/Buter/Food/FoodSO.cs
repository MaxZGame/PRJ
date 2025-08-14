using UnityEngine;
[CreateAssetMenu(fileName = "NewFood", menuName = "CreateFood", order = 1)]
public class FoodSO : ScriptableObject
{
    [field:SerializeField,Header("Ќазвание ингредиента")]
    public string FoodName { get; private set; }

    [field: SerializeField, Header("Ёффект положительный?")]
    public bool IsGood { get; private set; }
}
