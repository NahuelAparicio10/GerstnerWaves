using System;
using static AA1_ParticleSystem;
using static AA2_Rigidbody;

public struct CollisionResult
{    public bool IsColliding { get; set; }
    public Vector3C DirectionToBounce { get; set; }
    public PlaneC PlaneCollision { get; set; }

    public float PenetrationDepth { get; set; }
}

public static class CollisionHelper
{
    #region AA1
    public static CollisionResult IsCollidingWithSpheres(Particle particle, SphereC[] spheres)
    {
        CollisionResult result = new CollisionResult();
        foreach (var sphere in spheres)
        {
            float distance = Vector3C.Distance(particle.position, sphere.position);
            float distToCollide = particle.size + sphere.radius;

            if (distance < distToCollide)
            {
                result.DirectionToBounce = Vector3C.Normalize(particle.position - sphere.position);
                result.IsColliding = true;
                return result;
            }
        }
        result.IsColliding = false;
        return result;
    }
    public static CollisionResult IsCollidingWithCapsule(Particle particle, CapsuleC[] capsules)
    {
        CollisionResult result = new CollisionResult();

        foreach (var capsule in capsules)
        {
            Vector3C nearestAxisPoint = capsule.ClosestPointOnLine(particle.position);

            float distanceAxisToParticle = Vector3C.Distance(particle.position, nearestAxisPoint);

            float distToCollide = particle.size + capsule.radius;

            float distanceToEnds = (float)Math.Sqrt((distToCollide) * (distToCollide) - distanceAxisToParticle * distanceAxisToParticle);

            float totalDistance = (capsule.positionB - capsule.positionA).magnitude * 0.5f + distanceToEnds;

            if (totalDistance <= (capsule.positionB - capsule.positionA).magnitude * .5f)
            {
                result.DirectionToBounce = Vector3C.Normalize(particle.position - nearestAxisPoint);
                result.IsColliding = true;
                return result;
            }
        }
        result.IsColliding = false;
        return result;
    }
    public static CollisionResult IsCollidingWithPlanes(Particle particle, PlaneC[] planes)
    {
        CollisionResult result = new CollisionResult();

        foreach (PlaneC plane in planes)
        {
            float distanceToPlane = Vector3C.Dot(plane.normal, particle.position) - plane.CalculateDistance();

            if (distanceToPlane <= particle.size)
            {
                result.DirectionToBounce = plane.normal;
                result.IsColliding = true;
                result.PlaneCollision = plane;
                return result;
            }
        }

        result.IsColliding = false;
        result.DirectionToBounce = Vector3C.zero;
        return result;
    }
    public static Vector3C ReactToCollisionWithPlane(Particle particle, PlaneC plane)
    {
        float normalProjection = Vector3C.Dot(particle.velocity, plane.normal);

        Vector3C normalComponent = -1 * normalProjection * plane.normal;

        Vector3C tangentialComponent = particle.velocity - normalComponent;

        Vector3C newSpeed = normalComponent + tangentialComponent;
        return newSpeed;
    }
    #endregion

    public static CollisionResult IsCollidingWithPlanes(CubeRigidbody cube, PlaneC plane)
    {
        CollisionResult result = new CollisionResult();
        Vector3C[] vertices = GetCubeVertices(cube);

        bool isColliding = false;
        float minDist = float.MaxValue;
        Vector3C penetrationNormal = Vector3C.zero;

        foreach (var vertex in vertices)
        {
            float dist = Vector3C.Dot(plane.normal, vertex - plane.position);
            if (dist > 0) 
            {
                isColliding = true;
                if (Math.Abs(dist) < Math.Abs(minDist))
                {
                    minDist = dist;
                    penetrationNormal = plane.normal;
                }
            }
        }

        if (isColliding)
        {
            result.IsColliding = true;
            result.DirectionToBounce = -1 * penetrationNormal;
            result.PenetrationDepth = minDist;
            result.PlaneCollision = plane;
        }
        else
        {
            result.IsColliding = false;
        }

        return result;
    }

    private static Vector3C[] GetCubeVertices(CubeRigidbody cube)
    {
        Vector3C halfSize = cube.size * 0.5f;
        Vector3C[] localVertices = new Vector3C[8]
        {
        new Vector3C(-halfSize.x, -halfSize.y, -halfSize.z),
        new Vector3C(halfSize.x, -halfSize.y, -halfSize.z),
        new Vector3C(halfSize.x, halfSize.y, -halfSize.z),
        new Vector3C(-halfSize.x, halfSize.y, -halfSize.z),
        new Vector3C(-halfSize.x, -halfSize.y, halfSize.z),
        new Vector3C(halfSize.x, -halfSize.y, halfSize.z),
        new Vector3C(halfSize.x, halfSize.y, halfSize.z),
        new Vector3C(-halfSize.x, halfSize.y, halfSize.z)
        };

        Vector3C[] worldVertices = new Vector3C[8];
        for (int i = 0; i < 8; i++)
        {
            worldVertices[i] = cube.position + cube.rotation * localVertices[i];
        }
        return worldVertices;
    }


    // -- Collision Sphere / Vertex for Cloth System
    public static bool SphericalCollision(SphereC sphere, Vector3C vertexPosition)
    {
        float distance = Vector3C.Distance(vertexPosition, sphere.position);

        if (distance == sphere.radius)
        {
            //Contact
            return true;
        }
        else if (distance < sphere.radius)
        {
            //Interpenetration, true??
            return true;
        }
        else
        {
            //Separated
            return false;
        }

    }


    public static bool BoxCollision(CubeRigidbody cube1, CubeRigidbody cube2)
    {
        Vector3C min1 = cube1.position - cube1.size / 2;
        Vector3C max1 = cube1.position + cube1.size / 2;
        Vector3C min2 = cube2.position - cube2.size / 2;
        Vector3C max2 = cube2.position + cube2.size / 2;

        // Check intersection
        bool xOverlap = min1.x <= max2.x && max1.x >= min2.x;
        bool yOverlap = min1.y <= max2.y && max1.y >= min2.y;
        bool zOverlap = min1.z <= max2.z && max1.z >= min2.z;

        return xOverlap && yOverlap && zOverlap;
    }
}

