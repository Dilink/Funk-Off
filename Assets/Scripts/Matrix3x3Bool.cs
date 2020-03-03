using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
[StructLayout(LayoutKind.Sequential)]
public class Matrix3x3Bool : IEquatable<Matrix3x3Bool>
{
    // memory layout:
    //
    //                row no (=vertical)
    //               |  0   1   2
    //            ---+------------
    //            0  | m00 m10 m20
    // column no  1  | m01 m11 m21
    // (=horiz)   2  | m02 m12 m22
    public bool m00, m10, m20,
                m01, m11, m21,
                m02, m12, m22;

    // used to allow Matrix3x3Bools to be used as keys in hash tables
    public override int GetHashCode()
    {
        int c0 = m00.GetHashCode() ^ (m01.GetHashCode() << 2) ^ (m02.GetHashCode() >> 2);
        int c1 = m10.GetHashCode() ^ (m11.GetHashCode() << 2) ^ (m12.GetHashCode() >> 2);
        int c2 = m20.GetHashCode() ^ (m21.GetHashCode() << 2) ^ (m22.GetHashCode() >> 2);
        return c0 ^ (c1 << 2) ^ (c2 >> 2);
    }

    // also required for being able to use Matrix3x3Bools as keys in hash tables
    public override bool Equals(object other)
    {
        if (!(other is Matrix3x3Bool)) return false;

        return Equals((Matrix3x3Bool)other);
    }

    public bool Equals(Matrix3x3Bool other)
    {
        bool c0 = m00 == other.m00 && m01 == other.m01 && m02 == other.m02;
        bool c1 = m10 == other.m10 && m11 == other.m11 && m12 == other.m12;
        bool c2 = m20 == other.m20 && m21 == other.m21 && m22 == other.m22;
        return c0 && c1 && c2;
    }

    public static bool operator ==(Matrix3x3Bool lhs, Matrix3x3Bool rhs)
    {
        bool c0 = lhs.m00 == rhs.m00 && lhs.m01 == rhs.m01 && lhs.m02 == rhs.m02;
        bool c1 = lhs.m10 == rhs.m10 && lhs.m11 == rhs.m11 && lhs.m12 == rhs.m12;
        bool c2 = lhs.m20 == rhs.m20 && lhs.m21 == rhs.m21 && lhs.m22 == rhs.m22;
        // Returns false in the presence of NaN values.
        return c0 && c1 && c2;
    }

    public static bool operator !=(Matrix3x3Bool lhs, Matrix3x3Bool rhs)
    {
        // Returns true in the presence of NaN values.
        return !(lhs == rhs);
    }

    // Access element at [row, column].
    public bool this[int row, int column]
    {
        get
        {
            return this[row + column * 3];
        }

        set
        {
            this[row + column * 3] = value;
        }
    }

    // Access element at sequential index (0..15 inclusive).
    public bool this[int index]
    {
        get
        {
            switch (index)
            {
                case 0: return m00;
                case 1: return m10;
                case 2: return m20;
                case 3: return m01;
                case 4: return m11;
                case 5: return m21;
                case 6: return m02;
                case 7: return m12;
                case 8: return m22;
                default:
                    throw new IndexOutOfRangeException("Invalid matrix index!");
            }
        }

        set
        {
            switch (index)
            {
                case 0: m00 = value; break;
                case 1: m10 = value; break;
                case 2: m20 = value; break;
                case 3: m01 = value; break;
                case 4: m11 = value; break;
                case 5: m21 = value; break;
                case 6: m02 = value; break;
                case 7: m12 = value; break;
                case 8: m22 = value; break;
                default:
                    throw new IndexOutOfRangeException("Invalid matrix index!");
            }
        }
    }

    public Vector2Int GetLocation(int index)
    {
        switch (index)
        {
            case 0: return new Vector2Int(0, 0); // m00
            case 1: return new Vector2Int(1, 0); // m10
            case 2: return new Vector2Int(2, 0); // m20
            case 3: return new Vector2Int(0, 1); // m01
            case 4: return new Vector2Int(1, 1); // m11
            case 5: return new Vector2Int(2, 1); // m21
            case 6: return new Vector2Int(0, 2); // m02
            case 7: return new Vector2Int(1, 2); // m12
            case 8: return new Vector2Int(2, 2); // m22
            default:
                throw new IndexOutOfRangeException("Invalid matrix index!");
        }
    }

    public int[] GetTrueValuesIndices()
    {
        List<int> indices = new List<int>();
        
        for (int i = 0; i < 9; i++)
        {
            if (this[i])
            {
                indices.Add(i);
            }
        }
       
        return indices.ToArray();
    }
}