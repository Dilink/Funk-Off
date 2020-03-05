#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEngine;
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

    protected override void DrawPropertyLayout(GUIContent label)
    {
        Matrix3x3Bool value = this.ValueEntry.SmartValue;

        Color defaultColor = GUI.color;
        Rect iniRect = EditorGUILayout.GetControlRect(GUILayout.Height(0));

        if (Event.current.type == EventType.Repaint)
        {
            validSize = iniRect.width;
        }

        EditorGUILayout.GetControlRect(GUILayout.Height(validSize));

        for (int i = 0; i < 3; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < 3; j++)
            {
                Color color = value[i, j] ? Color.black : Color.white;
                GUI.color = color;
                Rect rect = new Rect();

                rect.width = rect.height = iniRect.width / 3.0f;
                rect.x = iniRect.x + i * rect.width;
                rect.y = iniRect.y + j * rect.height;
                rect = rect.Padding(rect.width / 3.0f * 0.1f);

                if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
                {
                    if (value[i, j])
                    {
                        value[i, j] = false;
                    }
                    else if (!hasMoreThan3Values(value))
                    {
                        value[i, j] = true;
                    }
                    GUI.changed = true;
                    Event.current.Use();
                }
                EditorGUI.DrawRect(rect, color);

                GUI.color = defaultColor;
            }
            EditorGUILayout.EndHorizontal();
        }
        this.ValueEntry.SmartValue = value;
    }
}
#endif
