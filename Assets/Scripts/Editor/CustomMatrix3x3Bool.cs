#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;

public class Matrix3x3BoolDrawer : OdinValueDrawer<Matrix3x3Bool>
{
    private float validSize = 0;

    private bool hasMoreThan3Values(Matrix3x3Bool value)
    {
        uint count = 0;
        for (int i = 0; i < 9; i++)
        {
            if (value[i])
            {
                count++;
            }
        }
        return count >= 3;
    }

    protected override void DrawPropertyLayout(UnityEngine.GUIContent label)
    {
        Matrix3x3Bool value = this.ValueEntry.SmartValue;

        UnityEngine.Color defaultColor = UnityEngine.GUI.color;
        UnityEngine.Rect iniRect = EditorGUILayout.GetControlRect(UnityEngine.GUILayout.Height(0));

        if (UnityEngine.Event.current.type == UnityEngine.EventType.Repaint)
        {
            validSize = iniRect.width;
        }

        EditorGUILayout.GetControlRect(UnityEngine.GUILayout.Height(validSize));

        for (int i = 0; i < 3; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < 3; j++)
            {
                UnityEngine.Color color = value[i, j] ? UnityEngine.Color.black : UnityEngine.Color.white;
                UnityEngine.GUI.color = color;
                UnityEngine.Rect rect = new UnityEngine.Rect();

                rect.width = rect.height = iniRect.width / 3.0f;
                rect.x = iniRect.x + i * rect.width;
                rect.y = iniRect.y + j * rect.height;
                
                if (UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown && rect.Contains(UnityEngine.Event.current.mousePosition))
                {
                    if (value[i, j])
                    {
                        value[i, j] = false;
                    }
                    else if (!hasMoreThan3Values(value))
                    {
                        value[i, j] = true;
                    }
                    UnityEngine.GUI.changed = true;
                    UnityEngine.Event.current.Use();
                }
                EditorGUI.DrawRect(rect.Padding(rect.width / 3.0f * 0.1f), color);

                UnityEngine.GUI.color = defaultColor;
            }
            EditorGUILayout.EndHorizontal();
        }
        this.ValueEntry.SmartValue = value;
    }
}
#endif
