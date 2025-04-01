using System;
using static AA3_Waves;

[System.Serializable]
public class Wave
{
    public Vector3C direction;
    public float Amplitude => _amplitude;
    public float Omega => _omega;
    public float K => _k;

    float _amplitude;
    float _omega;
    float _waveLength;
    float _k;

    public Wave(WavesSettings settings)
    {
        float ka = 0;
        do
        {
            _amplitude = Utils.RandomRange(settings.minA, settings.maxA);
            _omega = Utils.RandomRange(settings.minOmega, settings.maxOmega);
            _waveLength = Utils.RandomRange(settings.minLanda, settings.maxLanda);
            _k = (float)(2f * Math.PI / _waveLength);
            ka = _k * _amplitude;
        } while (ka < 1 && ka < 0);
    }

}
