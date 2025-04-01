
public struct TransformC
{
    public Vector3C position;
    public MyQuat rotation;
    public Vector3C eulerAngles
    {
        get => rotation.eulerAngles;
        set => MyQuat.Euler(value);
    }

    //public Vector3C right
    //{
    //    get => rotation * Vector3C.right;
    //    set => rotation = MyQuat.FromToRotation(Vector3C.right, value);
    //}

    //public Vector3C up
    //{
    //    get => rotation * Vector3C.up;
    //    set => MyQuat.FromToRotation(Vector3C.up, value);
    //}

    //public Vector3C forward
    //{
    //    get => rotation * Vector3C.forward;
    //    set => MyQuat.LookRotation(value, Vector3C.up);
    //}

}