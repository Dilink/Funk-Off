#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEngine;
using UnityEditor;

public class Matrix5x5BoolDrawer : OdinValueDrawer<Matrix5x5Bool>
{
    private float validSize = 0;

    private bool hasMoreThan3Values(Matrix5x5Bool value)
    {
        uint count = 0;
        for (int i = 0; i < Matrix5x5Bool.LENGTH; i++)
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
        Matrix5x5Bool value = this.ValueEntry.SmartValue;

        Color defaultColor = GUI.color;
        Rect iniRect = EditorGUILayout.GetControlRect(GUILayout.Height(0));

        if (Event.current.type == EventType.Repaint)
        {
            validSize = iniRect.width;
        }

        EditorGUILayout.GetControlRect(GUILayout.Height(validSize));

        for (int i = 0; i < Matrix5x5Bool.ROW_LENGTH; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < Matrix5x5Bool.COL_LENGTH; j++)
            {
                Color color = value[i, j] ? Color.black : Color.white;
                GUI.color = color;
                Rect rect = new Rect();

                rect.width = rect.height = iniRect.width / Matrix5x5Bool.COL_LENGTH;
                rect.x = iniRect.x + i * rect.width;
                rect.y = iniRect.y + j * rect.height;
                rect = rect.Padding(rect.width / Matrix5x5Bool.COL_LENGTH * 0.1f);

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
