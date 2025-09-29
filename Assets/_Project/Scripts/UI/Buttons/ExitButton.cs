using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExitButton : MonoBehaviour
{
    public void Exit()
    {
        SFX_Main.Instance.PlayAudio("Select"); //Звук при нажатии на эту клавишу
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
}
