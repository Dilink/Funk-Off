#if UNITY_EDITOR
using System;
using System.Linq;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEngine;
using UnityEditor;

public class CustomMatrix3x3Bitwise : OdinValueDrawer<Matrix3x3Int>
{
    private float validSize = 0;

    private Dictionary<int, Color> colorMap = new Dictionary<int, Color>()
    {
        { 1 << 0, Color.red },
        { 1 << 1, Color.blue },
        { 1 << 2, Color.green },
        { 1 << 3, Color.yellow },
        { 1 << 4, Color.gray },
    };

    private int GetMaxFlagValue()
    {
        int result = 0;
        var enumValues = Enum.GetValues(typeof(TileModifier)).Cast<TileModifier>();
        foreach (var item in enumValues)
            result |= (int)item;
        return result;
    }

    protected override void DrawPropertyLayout(GUIContent label)
    {
        Matrix3x3Int value = this.ValueEntry.SmartValue;

        Color defaultColor = GUI.color;
        Rect iniRect = EditorGUILayout.GetControlRect(GUILayout.Height(0));

        var menuItemClickCallback = new GenericMenu.MenuFunction2((v) => {
            var e = v as Tuple<int, int, int>;
            value[e.Item2, e.Item3] ^= e.Item1;
        });

        var resetAllClickCallback = new GenericMenu.MenuFunction2((v) => {
            var e = v as Tuple<int, int>;
            value[e.Item1, e.Item2] = 0;
        });

        var checkAllClickCallback = new GenericMenu.MenuFunction2((v) => {
            var e = v as Tuple<int, int>;
            value[e.Item1, e.Item2] = GetMaxFlagValue();
        });

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
                int activatedCount = 0;
                float r = 0, g = 0, b = 0;
                Rect rect = new Rect();

                rect.width = rect.height = iniRect.width / 3.0f;
                rect.x = iniRect.x + i * rect.width;
                rect.y = iniRect.y + j * rect.height;
                rect = rect.Padding(rect.width / 3.0f * 0.1f);

                var menu = new GenericMenu();
                menu.AddDisabledItem(new GUIContent("Tile Modifier"));
                menu.AddSeparator("");
                var enumValues = Enum.GetValues(typeof(TileModifier)).Cast<TileModifier>();
                foreach (var item in enumValues)
                {
                    bool isActivated = (value[i, j] & ((int)item)) != 0;
                    if (isActivated)
                    {
                        activatedCount++;
                        r += colorMap[(int)item].r;
                        g += colorMap[(int)item].g;
                        b += colorMap[(int)item].b;
                    }
                    menu.AddItem(new GUIContent(item.ToString()), isActivated, menuItemClickCallback, new Tuple<int, int, int>((int)item, i, j));
                }
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Check all"), false, checkAllClickCallback, new Tuple<int, int>(i, j));
                menu.AddItem(new GUIContent("Reset all"), false, resetAllClickCallback, new Tuple<int, int>(i, j));

                if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
                {
                    menu.ShowAsContext();
                    GUI.changed = true;
                    Event.current.Use();
                }
                EditorGUI.DrawRect(rect, new Color(r / (float)activatedCount, g / (float)activatedCount, b / (float)activatedCount));
            }
            EditorGUILayout.EndHorizontal();
        }
        this.ValueEntry.SmartValue = value;
    }
}
#endif
