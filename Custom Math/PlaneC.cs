using System;

[System.Serializable]
public struct PlaneC
{
    #region FIELDS
    public Vector3C position;
    public Vector3C normal;
    public Vector3C size;

    public float _distance;
    #endregion

    #region PROPIERTIES

    //  public Vector3C normal { get { return normal; } set { normal = value; } }
    //public float distance { get { return _distance; } set { _distance = value; } }

    //public PlaneC flipped { get { return new PlaneC(-_normal, 0f - _distance); } }

    public static Vector3C right { get { return new Vector3C(1, 0, 0); } }
    public static Vector3C up { get { return new Vector3C(0, 1, 0); } }
    public static Vector3C forward { get { return new Vector3C(0, 0, 1); } }
    #endregion

    #region CONSTRUCTORS
    //public PlaneC(Vector3C nNormal, Vector3C nPoint)
    //{
    //    _normal = nNormal;
    //    _distance = 0f - Vector3C.Dot(_normal, nPoint);
    //}

    //public Plane(Vector3C nNormal, float d)
    //{
    //    _normal = Vector3C.Normalize(nNormal);
    //    _distance = d;
    //}

    //public Plane(Vector3 a, Vector3 b, Vector3 c)
    //{
    //    m_Normal = Vector3.Normalize(Vector3.Cross(b - a, c - a));
    //    m_Distance = 0f - Vector3.Dot(m_Normal, a);
    //}
    public PlaneC(Vector3C position, Vector3C normal, int size)
    {
        this.position = position;
        this.normal = Vector3C.Normalize(normal);
        this.size = new Vector3C(size, size, size);
        this._distance = 0f - Vector3C.Dot(normal, position);

    }
    public PlaneC(Vector3C position, Vector3C normal, float size)
    {
        this.position = position;
        this.normal = Vector3C.Normalize(normal);
        this.size = new Vector3C(size, size, size);
        this._distance = 0f - Vector3C.Dot(normal, position);
    }

    public PlaneC(Vector3C normal, float distance) : this()
    {
        this.normal = Vector3C.Normalize(normal);
        this._distance = distance;
    }

    // A
    #endregion

    #region OPERATORS
    public static bool operator ==(PlaneC lhs, PlaneC rhs)
    {
        if (lhs.position == rhs.position && lhs.normal == rhs.normal)
        {
            return true;
        }
        return false;
    }

    public static bool operator !=(PlaneC lhs, PlaneC rhs)
    {
        if (lhs.position != rhs.position && lhs.normal != rhs.normal)
        {
            return true;
        }
        return false;
    }
    #endregion

    #region METHODS
    public void ToEquation()
    {
        
    }

    public float CalculateDistance()
    {
        return -Vector3C.Dot(normal, position);
    }

    public Vector3C NearestPoint(PlaneC myPlane, Vector3C extPoint)
    {
        Vector3C vectorToExtPoint = extPoint - myPlane.position;

        float distance = Vector3C.Dot(vectorToExtPoint, myPlane.normal);

        distance = Math.Abs(distance);

        Vector3C nearestPoint = extPoint - distance * myPlane.normal;

        return nearestPoint;
    }
    public Vector3C IntersectionPoint(PlaneC myPlane, LineC myLine)
    {
        float numerator = Vector3C.Dot(myPlane.normal, (myPlane.position - myLine.origin));

        float denominator = Vector3C.Dot(myPlane.normal, myLine.direction);

        if (Math.Abs(denominator) < Utils.Epsilon)
        {
            throw new InvalidOperationException("La línea es paralela al plano.");
        }

        float t = numerator / denominator;

        Vector3C intersectionPoint = myLine.origin + t * myLine.direction;

        return intersectionPoint;


    }
    public override bool Equals(object obj)
    {
        if (obj is PlaneC)
        {
            PlaneC other = (PlaneC)obj;
            return other == this;
        }
        return false;
    }
    #endregion

    #region FUNCTIONS
    #endregion

}