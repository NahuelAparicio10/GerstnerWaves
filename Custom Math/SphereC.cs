using System;

[System.Serializable]
public struct SphereC
{
    #region FIELDS
    public Vector3C position;
    public float radius;
    #endregion

    #region PROPIERTIES
    #endregion

    #region CONSTRUCTORS

    public SphereC(Vector3C _pos, float _radius)
    {
        position = _pos;
        radius = _radius;
    }

    public SphereC(float _radius = 0.5f)
    {
        position = new Vector3C(0,0,0);
        radius = _radius;
    }

    #endregion

    #region OPERATORS

    public static bool operator ==(SphereC lhs, SphereC rhs)
    {
        if (lhs.position == rhs.position && lhs.radius == rhs.radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool operator !=(SphereC lhs, SphereC rhs)
    {
        if (lhs.position != rhs.position && lhs.radius != rhs.radius)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    #endregion

    #region METHODS
    public override bool Equals(object obj)
    {
        if (obj is SphereC)
        {
            SphereC other = (SphereC)obj;
            return other == this;
        }
        return false;
    }
    public bool IsInside(SphereC sphere)
    {
        return true;
       
    }
    #endregion

    #region FUNCTIONS
    #endregion

}