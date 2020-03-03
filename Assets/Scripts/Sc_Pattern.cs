using System;
using UnityEngine;
using System.Runtime.InteropServices;

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
    public bool m00, m01, m02,
                m10, m11, m12,
                m20, m21, m22;

    // used to allow Matrix3x3Bools to be used as keys in hash tables
    public override int GetHashCode()
    {
        int c0 = m00.GetHashCode() ^ (m10.GetHashCode() << 2) ^ (m20.GetHashCode() >> 2);
        int c1 = m01.GetHashCode() ^ (m11.GetHashCode() << 2) ^ (m21.GetHashCode() >> 2);
        int c2 = m02.GetHashCode() ^ (m12.GetHashCode() << 2) ^ (m22.GetHashCode() >> 2);
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
        bool c0 = m00 == other.m00 && m10 == other.m10 && m20 == other.m20;
        bool c1 = m01 == other.m01 && m11 == other.m11 && m21 == other.m21;
        bool c2 = m02 == other.m02 && m12 == other.m12 && m22 == other.m22;
        return c0 && c1 && c2;
    }

    public static bool operator ==(Matrix3x3Bool lhs, Matrix3x3Bool rhs)
    {
        bool c0 = lhs.m00 == rhs.m00 && lhs.m10 == rhs.m10 && lhs.m20 == rhs.m20;
        bool c1 = lhs.m01 == rhs.m01 && lhs.m11 == rhs.m11 && lhs.m21 == rhs.m21;
        bool c2 = lhs.m02 == rhs.m02 && lhs.m12 == rhs.m12 && lhs.m22 == rhs.m22;
        // Returns false in the presence of NaN values.
        return c0 && c1 && c2;
    }

    public static bool operator !=(Matrix3x3Bool lhs, Matrix3x3Bool rhs)
    {
        // Returns true in the presence of NaN values.
        return !(lhs == rhs);
    }
}



public class Sc_Pattern : ScriptableObject
{
    public Matrix3x3Bool matrix;
}
