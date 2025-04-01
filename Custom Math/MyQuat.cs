using System;

public struct MyQuat
{
    #region FIELDS
    public float w;
    public float x;
    public float y;
    public float z;
    #endregion

    #region CONSTRUCTORS
    public MyQuat(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
    #endregion

    #region PROPIERTIES

    public static MyQuat identity => new MyQuat(0, 0, 0, 1);

    public static MyQuat NullQ
    {
        get
        {
            MyQuat a;
            a.w = 1;
            a.x = 0;
            a.y = 0;
            a.z = 0;
            return a;
        }
    }

    public MyQuat normalized
    {
        get => NormalizarQuat(this);
    }

    public Vector3C eulerAngles
    {
        get => MakePositive(ToEulerAngles() * Utils.Rad2Deg);
        set => FromEulerRad(value * (float)(Math.PI / 180f));

    }


    #endregion

    #region OPERATORS

    public static MyQuat operator *(MyQuat q1, MyQuat q2)
    {
        float x = q1.x * q2.w + q1.y * q2.z - q1.z * q2.y + q1.w * q2.x;
        float y = -q1.x * q2.z + q1.y * q2.w + q1.z * q2.x + q1.w * q2.y;
        float z = q1.x * q2.y - q1.y * q2.x + q1.z * q2.w + q1.w * q2.z;
        float w = -q1.x * q2.x - q1.y * q2.y - q1.z * q2.z + q1.w * q2.w;

        return new MyQuat(x, y, z, w);
    }
    #endregion
    #region METHODS
    //public static MyQuat GetSwing(MyQuat rot3)
    //{
    //    return NormalizarQuat(Multiply(Inverse(_twist), rot3));
    //}
    //public static MyQuat GetTwist(MyQuat rot3)
    //{
    //    return NormalizarQuat(Multiply(rot3, Inverse(_swing)));
    //}
    public static MyQuat Multiply(MyQuat q1, MyQuat q2)
    {
        float x = q1.x * q2.w + q1.y * q2.z - q1.z * q2.y + q1.w * q2.x;
        float y = -q1.x * q2.z + q1.y * q2.w + q1.z * q2.x + q1.w * q2.y;
        float z = q1.x * q2.y - q1.y * q2.x + q1.z * q2.w + q1.w * q2.z;
        float w = -q1.x * q2.x - q1.y * q2.y - q1.z * q2.z + q1.w * q2.w;

        return new MyQuat(x, y, z, w);
    }

    public static MyQuat Rotate(MyQuat currentRotation, Vector3C axis, float angle)
    {
        return NormalizarQuat(Multiply(currentRotation, AngleAxis(angle, axis)));
    }

    public static float Dot(MyQuat q1, MyQuat q2)
    {
        return q1.x * q2.x + q1.y * q2.y + q1.z * q2.z + q1.w * q2.w;
    }

    public static MyQuat Inverse(MyQuat q)
    {
        MyQuat result;
        float num = 1f / ((float)Math.Pow(q.x, 2) + (float)Math.Pow(q.y, 2) + (float)Math.Pow(q.z, 2) + (float)Math.Pow(q.w, 2));

        result.x = -q.x * num;
        result.y = -q.y * num;
        result.z = -q.z * num;
        result.w = q.w * num;

        return result;
    }
    public static MyQuat NormalizarQuat(MyQuat q)
    {
        float num = (float)Math.Sqrt(Dot(q, q));
        return new MyQuat(q.x / num, q.y / num, q.z / num, q.w / num);
    }

    public static MyQuat AngleAxis(float ang, Vector3C axis)
    {
        float sinW = (float)Math.Sin(ang / 2);
        return new MyQuat(axis.x * sinW, axis.y * sinW, axis.z * sinW, (float)Math.Cos(ang / 2));
    }

    public static MyQuat Slerp(MyQuat start, MyQuat end, float percent)
    {
        MyQuat r;
        float t_ = 1 - percent;
        float wStart, wEnd;
        float theta = (float)Math.Acos(Dot(start, end));
        float sn = (float)Math.Sin(theta);
        wStart = (float)(Math.Sin(t_ * theta) / sn);
        wEnd = (float)(Math.Sin(percent * theta) / sn);
        r.x = wStart * start.x + wEnd * end.x;
        r.y = wStart * start.y + wEnd * end.y;
        r.z = wStart * start.z + wEnd * end.z;
        r.w = wStart * start.w + wEnd * end.w;
        r = NormalizarQuat(r);
        return r;
    }

    public static MyQuat LookRotation(Vector3C forward, Vector3C upwards)
    {
        forward = Vector3C.Normalize(forward);

        if (Vector3C.Cross(upwards, forward).sqrMagnitude < 0.0001f)
        {
            upwards = Vector3C.Cross(forward, Vector3C.right);
            if (upwards.sqrMagnitude < 0.0001f)
                upwards = Vector3C.Cross(forward, Vector3C.up);
        }

        Vector3C right = Vector3C.Normalize(Vector3C.Cross(upwards, forward));
        upwards = Vector3C.Cross(forward, right);

        float m00 = right.x;
        float m01 = right.y;
        float m02 = right.z;
        float m10 = upwards.x;
        float m11 = upwards.y;
        float m12 = upwards.z;
        float m20 = forward.x;
        float m21 = forward.y;
        float m22 = forward.z;

        float num8 = (m00 + m11) + m22;
        MyQuat quaternion = new MyQuat();
        if (num8 > 0f)
        {
            float num = (float)Math.Sqrt(num8 + 1f);
            quaternion.w = num * 0.5f;
            num = 0.5f / num;
            quaternion.x = (m12 - m21) * num;
            quaternion.y = (m20 - m02) * num;
            quaternion.z = (m01 - m10) * num;
            return quaternion;
        }
        if ((m00 >= m11) && (m00 >= m22))
        {
            float num7 = (float)Math.Sqrt(((1f + m00) - m11) - m22);
            float num4 = 0.5f / num7;
            quaternion.x = 0.5f * num7;
            quaternion.y = (m01 + m10) * num4;
            quaternion.z = (m02 + m20) * num4;
            quaternion.w = (m12 - m21) * num4;
            return quaternion;
        }
        if (m11 > m22)
        {
            float num6 = (float)Math.Sqrt(((1f + m11) - m00) - m22);
            float num3 = 0.5f / num6;
            quaternion.x = (m10 + m01) * num3;
            quaternion.y = 0.5f * num6;
            quaternion.z = (m21 + m12) * num3;
            quaternion.w = (m20 - m02) * num3;
            return quaternion;
        }
        float num5 = (float)Math.Sqrt(((1f + m22) - m00) - m11);
        float num2 = 0.5f / num5;
        quaternion.x = (m20 + m02) * num2;
        quaternion.y = (m21 + m12) * num2;
        quaternion.z = 0.5f * num5;
        quaternion.w = (m01 - m10) * num2;
        return quaternion;
    }


    public static MyQuat FromToRotation(Vector3C fromDirection, Vector3C toDirection)
    {
        fromDirection = Vector3C.Normalize(fromDirection);
        toDirection = Vector3C.Normalize(toDirection);
        float dot = Vector3C.Dot(fromDirection, toDirection);

        if (dot > 0.999999f)
            return identity;

        if (dot < -0.999999f)
        {
            Vector3C axis = Vector3C.Cross(Vector3C.right, fromDirection);
            if (axis.magnitude < 0.0001f)
                axis = Vector3C.Cross(Vector3C.up, fromDirection);

            return new MyQuat(axis.x, axis.y, axis.z, 0f);
        }

        float rotAngle = (float)(Math.Acos(dot) * Utils.Rad2Deg);
        Vector3C rotAxis = Vector3C.Cross(fromDirection, toDirection);
        rotAxis = Vector3C.Normalize(rotAxis);

        return AngleAxis(rotAngle, rotAxis);
    }

    public static MyQuat Euler(Vector3C euler)
    {
        return FromEulerRad(euler * (float)(Math.PI /180f));
    }

    public static Vector3C QuaternionToEulerAngles(MyQuat q)
    {
        // roll X
    float sinr_cosp = 2 * (q.w * q.x + q.y * q.z);
        float cosr_cosp = 1 - 2 * (q.x * q.x + q.y * q.y);
        float roll = (float)Math.Atan2(sinr_cosp, cosr_cosp);

        // pitch Y
        float sinp = 2 * (q.w * q.y - q.z * q.x);
        float pitch;
        if (Math.Abs(sinp) >= 1)
            pitch = (float)Math.PI / 2 * Math.Sign(sinp); // use 90 degrees if out of range
        else
            pitch = (float)Math.Asin(sinp);

        // yaw Z
        float siny_cosp = 2 * (q.w * q.z + q.x * q.y);
        float cosy_cosp = 1 - 2 * (q.y * q.y + q.z * q.z);
        float yaw = (float)Math.Atan2(siny_cosp, cosy_cosp);

        return new Vector3C(roll, pitch, yaw);
    }

    public Vector3C ToEulerAngles()
    {
        // roll X
        float sinr_cosp = 2 * (w * x + y * z);
        float cosr_cosp = 1 - 2 * (x * x + y * y);
        float roll = (float)Math.Atan2(sinr_cosp, cosr_cosp);

        // pitch Y
        float sinp = 2 * (w * y - z * x);
        float pitch;
        if (Math.Abs(sinp) >= 1)
            pitch = (float)Math.PI / 2 * Math.Sign(sinp);
        else
            pitch = (float)Math.Asin(sinp);

        // yaw Z
        float siny_cosp = 2 * (w * z + x * y);
        float cosy_cosp = 1 - 2 * (y * y + z * z);
        float yaw = (float)Math.Atan2(siny_cosp, cosy_cosp);

        return new Vector3C(roll, pitch, yaw);
    }

    private static MyQuat FromEulerRad(Vector3C euler)
    {
        float cy = (float)Math.Cos(euler.z * 0.5);
        float sy = (float)Math.Sin(euler.z * 0.5);
        float cp = (float)Math.Cos(euler.y * 0.5);
        float sp = (float)Math.Sin(euler.y * 0.5);
        float cr = (float)Math.Cos(euler.x * 0.5);
        float sr = (float)Math.Sin(euler.x * 0.5);

        MyQuat q;
        q.w = cr * cp * cy + sr * sp * sy;
        q.x = sr * cp * cy - cr * sp * sy;
        q.y = cr * sp * cy + sr * cp * sy;
        q.z = cr * cp * sy - sr * sp * cy;

        return q;
    }

    private static Vector3C MakePositive(Vector3C euler)
    {
        euler.x = euler.x % (2 * (float)Math.PI);
        euler.y = euler.y % (2 * (float)Math.PI);
        euler.z = euler.z % (2 * (float)Math.PI);

        // Ensure positive values
        if (euler.x < 0)
            euler.x += 2 * (float)Math.PI;
        if (euler.y < 0)
            euler.y += 2 * (float)Math.PI;
        if (euler.z < 0)
            euler.z += 2 * (float)Math.PI;

        return euler;
    }


    #endregion
    #region FUNCTIONS
    #endregion






}
