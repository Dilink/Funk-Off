#if UNITY_EDITOR
using System;
using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEngine;
using UnityEditor;

public class CustomMatrix3x3Bitwise : OdinValueDrawer<Matrix3x3Int>
{
    private float validSize = 0;

    protected override void DrawPropertyLayout(GUIContent label)
    {
        Matrix3x3Int value = this.ValueEntry.SmartValue;

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
                //Color color = value[i, j] ? Color.black : Color.white;
                Color color = Color.white;
                GUI.color = color;
                Rect rect = new Rect();

                rect.width = rect.height = iniRect.width / 3.0f;
                rect.x = iniRect.x + i * rect.width;
                rect.y = iniRect.y + j * rect.height;
                rect = rect.Padding(rect.width / 3.0f * 0.1f);

                if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
                {
                    var menu = new GenericMenu();
                    var callback = new GenericMenu.MenuFunction2((v) => {
                        var e = v as Tuple<int, int, int>;
                        value[e.Item2, e.Item3] ^= e.Item1;
                    });

                    menu.AddDisabledItem(new GUIContent("Tile Modifier"));
                    menu.AddSeparator("");
                    var enumValues = System.Enum.GetValues(typeof(TileModifier)).Cast<TileModifier>();
                    foreach (var item in enumValues)
                        menu.AddItem(new GUIContent(item.ToString()), (value[i, j] & ((int) item)) != 0, callback, new Tuple<int, int, int>((int) item, i, j));
                    menu.AddSeparator("");
                    menu.AddItem(new GUIContent("Reset"), false, (v) => {
                        var e = v as Tuple<int, int>;
                        value[e.Item1, e.Item2] = 0;
                    }, new Tuple<int, int>(i, j));
                    menu.ShowAsContext();

                    /*if (value[i, j])
                    {
                        value[i, j] = false;
                    }
                    else if (!hasMoreThan3Values(value))
                    {
                        value[i, j] = true;
                    }*/
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
