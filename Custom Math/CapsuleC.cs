using System;

[System.Serializable]
public struct CapsuleC
{
    #region FIELDS

    public Vector3C center;
    public float height;
    public float radius;
    #endregion

    #region PROPIERTIES
    public Vector3C positionA
    {
        get { return center + Vector3C.up * (height * 0.5f - radius); }
    }

    public Vector3C positionB
    {
        get { return center + Vector3C.down * (height * 0.5f - radius); }
    }

    #endregion

    #region CONSTRUCTORS
    public CapsuleC(Vector3C center, float height, float _radius)
    {
        this.center = center;
        this.height = height;
        this.radius = _radius;
    }
    #endregion

    #region OPERATORS
    public static bool operator ==(CapsuleC lhs, CapsuleC rhs)
    {
        return lhs.center == rhs.center && lhs.height == rhs.height && lhs.radius == rhs.radius;
    }

    public static bool operator !=(CapsuleC lhs, CapsuleC rhs)
    {
        return !(lhs == rhs);
    }
    #endregion

    #region METHODS
    public override bool Equals(object obj)
    {
        if (obj is CapsuleC)
        {
            CapsuleC other = (CapsuleC)obj;
            return other == this;
        }
        return false;
    }
    public bool IsCapsuleInside(CapsuleC otherCapsule)
    {
        Vector3C centerDifference = otherCapsule.center - center;
        float totalHeight = (height + otherCapsule.height) * 0.5f;
        float minDistance = radius + otherCapsule.radius;

        if (centerDifference.magnitude <= minDistance)
        {
            float heightDifference = Math.Abs(Vector3C.Dot(centerDifference, Vector3C.up));
            if (heightDifference <= totalHeight)
            {
                return true;
            }
        }

        return false;
    }

    public Vector3C ClosestPointOnLine(Vector3C point)
    {
        Vector3C pointA = positionA;
        Vector3C pointB = positionB;

        Vector3C dir = pointB - pointA;

        Vector3C v = point - pointA;

        float t = Vector3C.Dot(v, dir) / dir.sqrMagnitude;

        t = Utils.Clamp01(t);

        Vector3C closestPoint = pointA + dir * t;

        return closestPoint;
    }

    #endregion

    #region FUNCTIONS
    #endregion

}