using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Vector3Overloads {
	public static Vector2 vector2(this Vector3 v3) {
		return new Vector2(v3.x, v3.y);
	}
}

public static class Vector2Overloads {
	public static Vector3 vector3(this Vector2 v2) {
		return new Vector3(v2.x, v2.y, 0);
	}

	public static float ClockAngle(this Vector2 v2) {
	/* Returns the trig angle of the vector from the vertical position, from 0 at 12 to 90 at 3 and 180 at 6, etc */
		if (v2.x > 0) {
			return Vector2.Angle(v2, Vector2.up);
		} else {
			return 360f - Vector2.Angle(v2, Vector2.up);
		}
	}
}

public static class ParticleSystemExtension
{
    public static void EnableEmission(this ParticleSystem particleSystem, bool enabled)
    {
        var emission = particleSystem.emission;
        emission.enabled = enabled;
    }
 
    public static float GetEmissionRate(this ParticleSystem particleSystem)
    {
        return particleSystem.emission.rate.constantMax;
    }
 
    public static void SetEmissionRate(this ParticleSystem particleSystem, float emissionRate)
    {
        var emission = particleSystem.emission;
        var rate = emission.rate;
        rate.constantMax = emissionRate;
        emission.rate = rate;
    }
}