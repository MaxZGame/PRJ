using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log("������� �� �������");
        Destroy(gameObject);
    }

}
