using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Create Item", order = 2)]
public class ItemSO : ScriptableObject
{
    [field: SerializeField, Header("Введите название предмета")]
    public string NameItem { get; private set; }

    [field: SerializeField, Header("Вставьте начальный спрайт предмета")]
    public Sprite SpriteItem { get; private set; }
    [field: SerializeField, Header("Вставьте спрайт предмета при наведении на него мышью")]
    public Sprite SpriteItemOnMouse { get; private set; }

    [field: SerializeField, Header("Это интерактивный объект?")]
    public bool IsInteractive { get; private set; }

    private void OnValidate()
    {
        if (NameItem == null)
        {
            Debug.LogWarning($"Не заплонено имя itemSO {this}");
        }
        if (SpriteItem == null)
        {
            Debug.LogWarning($"Не назначен спрайт itemSO {this}");
        }
    }
}
