using System;

[System.Serializable]
public struct Vector3C
{
    #region FIELDS
    public float x;
    public float y;
    public float z;
    #endregion

    #region PROPIERTIES
    public float r { get => x; set => x = value; }
    public float g { get => y; set => y = value; }
    public float b { get => z; set => z = value; }
    public float magnitude { get => Magnitude(this); }
    public float sqrMagnitude { get => SqrMagnitude(this); }
    public Vector3C normalized { get => Normalize(this); }

    public static Vector3C zero { get { return new Vector3C(0, 0, 0); } }
    public static Vector3C one { get { return new Vector3C(1, 1, 1); } }
    public static Vector3C right { get { return new Vector3C(1, 0, 0); } }
    public static Vector3C up { get { return new Vector3C(0, 1, 0); } }
    public static Vector3C down { get { return new Vector3C(0, -1, 0); } }
    public static Vector3C forward { get { return new Vector3C(0, 0, 1); } }

    public static Vector3C black { get { return new Vector3C(0, 0, 0); } }
    public static Vector3C white { get { return new Vector3C(1, 1, 1); } }
    public static Vector3C red { get { return new Vector3C(1, 0, 0); } }
    public static Vector3C green { get { return new Vector3C(0, 1, 0); } }
    public static Vector3C blue { get { return new Vector3C(0, 0, 1); } }
    #endregion

    #region CONSTRUCTORS
    public Vector3C(float x = 0, float y = 0, float z = 0)
    {
        this.x = x; this.y = y; this.z = z;
    }

    public Vector3C(Vector3C pointA, Vector3C pointB)
    {
        this = pointB - pointA;
    }

    #endregion


    #region OPERATORS
    public static Vector3C operator +(Vector3C a, Vector3C b)
    {
        return new Vector3C(a.x + b.x, a.y + b.y, a.z + b.z);
    }
    public static Vector3C operator +(Vector3C a, float v)
    {
        return new Vector3C(a.x + v, a.y + v, a.z + v);
    }
    public static Vector3C operator +(float v, Vector3C a)
    {
        return new Vector3C(a.x + v, a.y + v, a.z + v);
    }
    public static Vector3C operator -(Vector3C a, Vector3C b)
    {
        return new Vector3C(a.x - b.x, a.y - b.y, a.z - b.z);
    }
    public static Vector3C operator /(Vector3C a, float num)
    {
        return new Vector3C(a.x / num, a.y / num, a.z / num);
    }

    public static Vector3C operator *(Vector3C a, float num)
    {
        return new Vector3C(a.x * num, a.y * num, a.z * num);
    }

    public static Vector3C operator *(Vector3C a, Vector3C b)
    {
        return new Vector3C(a.x * b.x, a.y * b.y, a.z * b.z);
    }

  
    public static Vector3C operator *(float num, Vector3C a)
    {
        return new Vector3C(a.x * num, a.y * num, a.z * num);
    }

    public static bool operator ==(Vector3C a, Vector3C b)
    {
        if (a.x == b.x && a.y == b.y && a.z == b.z)
        {
            return true;
        }
        return false;
    }
    public static bool operator !=(Vector3C a, Vector3C b)
    {
        if (a.x != b.x && a.y != b.y && a.z != b.z)
        {
            return true;
        }
        return false;
    }

    public static Vector3C operator *(MyQuat rotation, Vector3C point)
    {
        float num = rotation.x * 2f;
        float num2 = rotation.y * 2f;
        float num3 = rotation.z * 2f;
        float num4 = rotation.x * num;
        float num5 = rotation.y * num2;
        float num6 = rotation.z * num3;
        float num7 = rotation.x * num2;
        float num8 = rotation.x * num3;
        float num9 = rotation.y * num3;
        float num10 = rotation.w * num;
        float num11 = rotation.w * num2;
        float num12 = rotation.w * num3;
        Vector3C result = default(Vector3C);
        result.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
        result.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
        result.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
        return result;
    }

    #endregion

    #region METHODS

    public override bool Equals(object obj)
    {
        if (obj is Vector3C)
        {
            Vector3C other = (Vector3C)obj;
            return other == this;
        }
        return false;
    }

    public void Normalize()
    {
        float num = Magnitude(this);
        if (num > 1E-05f)
        {
            this /= num;
        }
        else
        {
            this = zero;
        }
    }

    public static Vector3C Normalize(Vector3C vec)
    {
        float magnitude = Magnitude(vec);
        if (magnitude > 0.0f)
        {
            return vec / magnitude;
        }
        return zero;
    }
    public static float Magnitude(Vector3C vec) => (float)Math.Sqrt(vec.x * vec.x + vec.y * vec.y + vec.z * vec.z);
    public float SqrMagnitude(Vector3C vec) => (float)(vec.x * vec.x + vec.y * vec.y + vec.z * vec.z);
    #endregion

    #region FUNCTIONS

    public static float Distance(Vector3C vec1, Vector3C vec2)
    {
        float num = vec1.x - vec2.x;
        float num2 = vec1.y - vec2.y;
        float num3 = vec1.z - vec2.z;
        return (float)Math.Sqrt(num * num + num2 * num2 + num3 * num3);
    }

    public static float Dot(Vector3C v1, Vector3C v2)
    {
        return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
    }
    public static Vector3C Cross(Vector3C v1, Vector3C v2)
    {
        return new Vector3C(v1.y * v2.z - v1.z * v2.y, v1.z * v2.x - v1.x * v2.z, v1.x * v2.y - v1.y * v2.x);
    }

    // - Angle in Degrees Between 2 Vecs
    public static float Angle(Vector3C from, Vector3C to)
    {
        float num = (float)Math.Sqrt(from.sqrMagnitude * to.sqrMagnitude);
        if (num == 0)
        {
            return 0f;
        }
        float num2 = Utils.Clamp(Dot(from, to) / num, -1f, 1f);
        return (float)Math.Acos(num2) * Utils.Rad2Deg;
    }

    public static float AngleTest(Vector3C a, Vector3C b)
    {
        return (float)Math.Atan2(b.y - a.y, b.x - a.x);
    }

    public static Vector3C Project(Vector3C a, Vector3C b)
    {
        float n = a.x * b.x + a.y * b.y + a.z * b.z;
        float t = (float)Math.Sqrt(b.x * b.x + b.y * b.y + b.z * b.z);

        return new Vector3C(n * b.x / (t * t), n * b.y / (t * t), n * b.z / (t * t));
    }

    public static Vector3C Reflect(Vector3C direction, Vector3C normal)
    {
        float dot = Dot(direction, normal);
        return direction - 2f * dot * normal;
    }

    #endregion

}