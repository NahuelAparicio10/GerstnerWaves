using System;
using static AA3_Waves;

public class Buoy
{
    private SphereC _sphere;
    private Vector3C _position;
    private Vector3C _force;
    private Vector3C _velocity;
    private float _volume;
    private float _mass;
    private float _buoyancyCoeff;

    private float _maxSpeed = 1.5f;

    public Buoy(Vector3C position, float radius)
    {
        _position = position;
        _sphere = new SphereC(_position, radius);
        _volume = 1.25f * (float)Math.PI * _sphere.radius * _sphere.radius * _sphere.radius;
        _mass = 1f;
        _buoyancyCoeff = 0.4f;
    }
    public void Update(float dt, Settings settings, Vertex[] points)
    { 
        _force = Vector3C.zero;

        _force += settings.gravity * _mass;

        float height = GetWaveHeight(points);

        float submergedVolume = (Math.Max(0, height - _position.y) * _volume);

        _force += (-1 * settings.gravity) * submergedVolume * settings.fluidDensity * _buoyancyCoeff;

        _velocity += (_force / _mass) * dt;

        //Clamping the speed gives a realistic look to the buoy,
        //if not it seems that bounces like a basket ball :')

        if (Math.Abs(_velocity.magnitude) > _maxSpeed)
        {
            _velocity = _velocity.normalized * _maxSpeed;
        }

        _position += _velocity * dt;

        _sphere.position = _position;
    }

    // -- Given a mesh, returns the waveHeight. By the buoy position calculates the highst vertex inside
    private float GetWaveHeight(Vertex[] points)
    {
        Vector3C positionXZ = new Vector3C(_position.x, 0, _position.y);

        float waveHeight = float.MinValue;

        foreach (var point in points)
        {
            Vector3C pointPositionXZ = new Vector3C(point.originalposition.x, 0, point.originalposition.z);

            float dist = (positionXZ - pointPositionXZ).magnitude;

            if(dist < _sphere.radius && point.position.y > waveHeight)
            {
                waveHeight = point.position.y;
            }
        }
        return waveHeight;
    }

    public void Debug()
    {
        _sphere.Print(Vector3C.blue);
    }
}
