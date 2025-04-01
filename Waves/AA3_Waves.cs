using System;

[System.Serializable]
public class AA3_Waves
{
    [System.Serializable]
    public struct Settings
    {
        public Vector3C gravity;
        public float fluidDensity;
    }
    public Settings settings;
    [System.Serializable]
    public struct WavesSettings
    {
        public float maxA;
        public float maxLanda;
        public float maxOmega;
        public float minA;
        public float minLanda;
        public float minOmega;
        public int numWaves;
    }
    public WavesSettings wavesSettings;
    public struct Vertex
    {
        public Vector3C originalposition;
        public Vector3C position;
        public Vertex(Vector3C _position)
        {
            position = _position;
            originalposition = _position;
        }
    }
     
    
    public Vertex[] points;
    public Wave[] waves;

    public Buoy buoy = new Buoy(new Vector3C(0, 0, 0), 1);

    public bool start = true;
    public bool enableInspectorValues = false;
    public float currentTime = 0f;
    public void Update(float dt)
    {
        currentTime += dt;

        if(start)
        {
            GenerateWaves();
            start = false;
        }

        for (int i = 0; i < points.Length; i++)
        {
            Vector3C newPosition = points[i].originalposition;
            Vector3C gerstnerOffset = new Vector3C(0, 0, 0);

            // -- Calculates with gerstner formula the displacement of each wave in the current point
            foreach (var wave in waves)
            {
                gerstnerOffset += GerstnerWave(newPosition, wave, currentTime);
            }

            newPosition += gerstnerOffset;
            points[i].position = newPosition;
        }   
        buoy.Update(dt, settings, points);
    }

    private void GenerateWaves()
    {
        // -- Initialize values for first time
        // if you want to set custom values in Inspector active enableInspectorValues
        if(!enableInspectorValues)
        {
            wavesSettings.numWaves = 3;
            wavesSettings.minA = 0.05f; wavesSettings.maxA = 0.3f;
            wavesSettings.minLanda = 3; wavesSettings.maxLanda = 5;
            wavesSettings.minOmega = 2.5f; wavesSettings.maxOmega = 4;
            settings.gravity.y = -9.81f;
            settings.fluidDensity = 2;
        }

        // -- Initialize Waves, each time different, because there are randoms when generating each wave
        // First three waves direction initalized with values to create a more realistic waves simulating a sea
        // If you add more in numWaves, the direction of them will be Random
        waves = new Wave[wavesSettings.numWaves];

        waves[0] = new Wave(wavesSettings);
        waves[0].direction = new Vector3C(1, 0, 0);
        waves[1] = new Wave(wavesSettings);
        waves[1].direction = new Vector3C(1, 0, 1);
        waves[2] = new Wave(wavesSettings);
        waves[2].direction = new Vector3C(1, 0, 1);

        for (int i = 3; i < waves.Length; i++)
        {
            waves[i] = new Wave(wavesSettings);
            waves[i].direction = new Vector3C(Utils.RandomRange(0, 1), 0, Utils.RandomRange(0, 1));
        }
    }

    // -- Calculates the displacement using Gerstner
    public Vector3C GerstnerWave(Vector3C point, Wave wave, float time)
    {
        Vector3C dirNormalized = Vector3C.Normalize(wave.direction);
        float phase = wave.K * (Vector3C.Dot(dirNormalized, point) - wave.Omega * time);

        Vector3C displacement = new Vector3C(
            dirNormalized.x * (wave.Amplitude * (float)Math.Cos(phase)),
            wave.Amplitude * (float)Math.Sin(phase),
            dirNormalized.z * (wave.Amplitude * (float)Math.Cos(phase)));

        return displacement;
    }

    public void Debug()
    {
        if(points != null)
        foreach (var item in points)
        {
            item.originalposition.Print(0.05f);
            item.position.Print(0.05f);
        }
        buoy.Debug();
    }

}
