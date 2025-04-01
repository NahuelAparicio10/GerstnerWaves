using System;

[System.Serializable]
public struct LineC
{
    #region FIELDS
    public Vector3C origin;
    public Vector3C direction;
    #endregion

    #region PROPIERTIES
    #endregion

    #region CONSTRUCTORS
    public LineC(Vector3C origin, Vector3C direction)
    {
        this.origin = origin;
        this.direction = direction - origin;
        direction.Normalize();
    }
    #endregion

    #region OPERATORS

    public static bool operator ==(LineC lhs, LineC rhs)
    {
        if (lhs.origin == rhs.origin && lhs.direction == rhs.direction)
        {
            return true;
        }
        return false;
    }

    public static bool operator !=(LineC lhs, LineC rhs)
    {
        if (lhs.origin != rhs.origin && lhs.direction != rhs.direction)
        {
            return true;
        }
        return false;
    }

    #endregion

    #region METHODS

    public override bool Equals(object obj)
    {
        if(obj is LineC)
        {
            LineC other = (LineC)obj;
            return other == this;
        }
        return false;  
    }
    public Vector3C PointToPoint(Vector3C extPoint, LineC myLine)
    {
        Vector3C vectorAB = myLine.direction;
        Vector3C vectorAP = extPoint - myLine.origin;

        float t = Vector3C.Dot(vectorAP, vectorAB) / Vector3C.Dot(vectorAB, vectorAB);

        t = Utils.Clamp01(t);

        Vector3C closestPoint = myLine.origin + t * vectorAB;
        return closestPoint;


    }
    public Vector3C PointToLine(LineC myExternLine, LineC myLine)
    {
        Vector3C vectorBetweenOrigins = myExternLine.origin - myLine.origin;

        float t = Vector3C.Dot(vectorBetweenOrigins, myLine.direction) / Vector3C.Dot(myLine.direction, myLine.direction);

        t = Utils.Clamp01(t);

        Vector3C closestPoint = myLine.origin + t * myLine.direction;
        return closestPoint;

    }
    #endregion

    #region FUNCTIONS
    public static LineC LineFromPointAPointB(Vector3C pointA, Vector3C pointB)
    {
        Vector3C lineOrigin = pointA;

        Vector3C lineDirection = pointB - pointA;
        lineDirection.Normalize();

        LineC newLine = new LineC(lineOrigin, lineDirection);
        return newLine;

    }

    public static Vector3C IntersectionPoint(PlaneC myPlane, LineC myLine)
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

    #endregion

}