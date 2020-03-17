#if UNITY_EDITOR
using System;
using System.Linq;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEngine;
using UnityEditor;

public class CustomMatrix5x5Bitwise : OdinValueDrawer<Matrix5x5Int>
{
    private float validSize = 0;

    private Dictionary<int, Color> colorMap = new Dictionary<int, Color>()
    {
        { 1 << 0, Color.red },
        { 1 << 1, Color.blue },
        { 1 << 2, Color.green },
        { 1 << 3, Color.yellow },
        { 1 << 4, Color.gray },
        { 1 << 5, Color.gray },
        { 1 << 6, Color.gray },
        { 1 << 7, Color.gray },

    };

    private int GetMaxFlagValue()
    {
        int result = 0;
        var enumValues = Enum.GetValues(typeof(TileModifier)).Cast<TileModifier>();
        foreach (var item in enumValues)
            result |= (int)item;
        return result;
    }

    void CalculateValue(int xCoord, int yCoord, Matrix5x5Int value)
    {
        int patternValue = 0;

        for (int y = 0; y < Matrix5x5Bool.ROW_LENGTH; y++)
        {
            for (int x = 0; x < Matrix5x5Bool.COL_LENGTH; x++)
            {
                int tileValue = 0;

                if ((value[x,y] & (int)TileModifier.Ice) != 0)
                {
                    tileValue += 2;
                }

                if ((value[x, y] & (int)TileModifier.Slow) != 0)
                {
                    tileValue += 2;
                }

                if ((value[x, y] & (int)TileModifier.Destroyer) != 0)
                {
                    tileValue += 1;
                }

                if ((value[x, y] & (int)TileModifier.WalledDown) != 0)
                {
                    tileValue += 1;
                }
                if ((value[x, y] & (int)TileModifier.WalledUp) !=0)               
                {
                    tileValue += 1;
                }
                if ((value[x, y] & (int)TileModifier.WalledRight) != 0)
                {
                    tileValue += 1;
                }
                if ((value[x, y] & (int)TileModifier.WalledLeft) != 0)
                {
                    tileValue += 1;
                }

                int coordAddition = Mathf.Abs(x - 2) + Mathf.Abs(y - 2);

                if ((value[x, y] & (int)TileModifier.WalledUp) != 0 || 
                    (value[x, y] & (int)TileModifier.WalledDown) != 0 ||
                    (value[x, y] & (int)TileModifier.WalledLeft) != 0 ||
                    (value[x, y] & (int)TileModifier.WalledRight) != 0 ||
                    (value[x, y] & (int)TileModifier.Destroyer) != 0 ||
                    (value[x, y] & (int)TileModifier.Ice) != 0 ||
                    (value[x, y] & (int)TileModifier.Slow) != 0)
                    switch (coordAddition)
                {
                    case 0:
                        tileValue += 6;
                        break;
                    case 1:
                        tileValue += 4;
                        break;
                    case 2:
                        tileValue += 2;
                        break;
                
                }

                patternValue += tileValue;
            }
        }
        value.difficultyLevel = patternValue;
    }

    protected override void DrawPropertyLayout(GUIContent label)
    {
        Matrix5x5Int value = this.ValueEntry.SmartValue;

        Color defaultColor = GUI.color;
        Rect iniRect = EditorGUILayout.GetControlRect(GUILayout.Height(0));

        var menuItemClickCallback = new GenericMenu.MenuFunction2((v) => {
            var e = v as Tuple<int, int, int>;
            value[e.Item2, e.Item3] ^= e.Item1;
            CalculateValue(e.Item2, e.Item3, value);

        });

        var resetAllClickCallback = new GenericMenu.MenuFunction2((v) => {
            var e = v as Tuple<int, int>;
            value[e.Item1, e.Item2] = 0;
            CalculateValue(e.Item1, e.Item2, value);
        });

        var checkAllClickCallback = new GenericMenu.MenuFunction2((v) => {
            var e = v as Tuple<int, int>;
            value[e.Item1, e.Item2] = GetMaxFlagValue();
            CalculateValue(e.Item1, e.Item2, value);
        });

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
                int activatedCount = 0;
                float r = 0, g = 0, b = 0;
                Rect rect = new Rect();

                rect.width = rect.height = iniRect.width / Matrix5x5Bool.COL_LENGTH;
                rect.x = iniRect.x + i * rect.width;
                rect.y = iniRect.y + j * rect.height;
                rect = rect.Padding(rect.width / Matrix5x5Bool.COL_LENGTH * 0.1f);

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
