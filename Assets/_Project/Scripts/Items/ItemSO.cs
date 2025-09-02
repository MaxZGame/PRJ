using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Create Item", order = 2)]
public class ItemSO : ScriptableObject
{
    [field: SerializeField, Header("������� �������� ��������")]
    public string NameItem { get; private set; }

    [field: SerializeField, Header("�������� ��������� ������ ��������")]
    public Sprite SpriteItem { get; private set; }
    [field: SerializeField, Header("�������� ������ �������� ��� ��������� �� ���� �����")]
    public Sprite SpriteItemOnMouse { get; private set; }

    [field: SerializeField, Header("��� ������������� ������?")]
    public bool IsInteractive { get; private set; }

    private void OnValidate()
    {
        if (NameItem == null)
        {
            Debug.LogWarning($"�� ��������� ��� itemSO {this}");
        }
        if (SpriteItem == null)
        {
            Debug.LogWarning($"�� �������� ������ itemSO {this}");
        }
    }
}
