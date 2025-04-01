using System;

[System.Serializable]
public struct CubeC
{
    #region FIELDS
    public Vector3C position;
    public Vector3C scale;
    public Vector3C rotation;
    public float radius;
    #endregion

    #region PROPIERTIES

    #endregion
    // Position by default 0 Radius by default 1, rotation by default 0, scale by default 1
    #region CONSTRUCTORS
    //public CubeC()
    //{
    //    position = Vector3C.zero;
    //    scale = Vector3C.one;
    //    rotation = Vector3C.zero;
    //    radius = 1;
    //}
    public CubeC(Vector3C pos)
    {
        position = pos;
        scale = Vector3C.one;
        rotation = Vector3C.zero;
        radius = 1;
    }
    public CubeC(Vector3C pos, Vector3C _scale)
    {
        position = pos;
        scale = _scale;
        rotation = Vector3C.zero;
        radius = 1;
    }
    public CubeC(Vector3C pos, Vector3C _scale, Vector3C rot)
    {
        position = pos;
        scale = _scale;
        rotation = rot;
        radius = 1;
    }

    public CubeC(Vector3C pos, Vector3C _scale, Vector3C rot, float _radius)
    {
        position = pos;
        scale = _scale;
        rotation = rot;
        radius = _radius;
    }

    public CubeC(Vector3C pos, Vector3C _scale, float _radius)
    {
        position = pos;
        scale = _scale;
        rotation = Vector3C.zero;
        radius = _radius;
    }

    #endregion

    #region OPERATORS
    public static bool operator ==(CubeC a, CubeC b)
    {
        if (a.position == b.position && a.scale == b.scale && a.rotation == b.rotation)
        {
            return true;
        }
        return false;
    }
    public static bool operator !=(CubeC a, CubeC b)
    {
        if (a.position != b.position && a.scale != b.scale && a.rotation != b.rotation)
        {
            return false;
        }
        return true;
    }


    #endregion

    #region METHODS

    public override bool Equals(object obj)
    {
        if (obj is CubeC)
        {
            CubeC other = (CubeC)obj;
            return other == this;
        }
        return false;
    }
    //public bool IsInside(object obj)
    //{
    //    if ()
    //    {

    //    }

    //}
    #endregion

    #region FUNCTIONS
    #endregion

}