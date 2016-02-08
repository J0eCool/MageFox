using UnityEngine;

public static class VectorUtil {
	public static Vector2 Unit(float radians) {
		return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
	}

    public static Vector2 Mult(Vector2 a, Vector2 b) {
        return new Vector2(a.x * b.x, a.y * b.y);
    }

	public static Vector3 Clamp(Vector3 vec, Vector3 lo, Vector3 hi) {
		return new Vector3(
			Mathf.Clamp(vec.x, lo.x, hi.x),
			Mathf.Clamp(vec.y, lo.y, hi.y),
			Mathf.Clamp(vec.z, lo.z, hi.z)
			);
	}

	public static Vector3 ClampXY(Vector3 vec, Vector2 lo, Vector2 hi) {
		Vector3 v = Clamp(vec, lo, hi);
		v.z = vec.z;
		return v;
	}

	public static Vector3 WithX(this Vector3 vec, float x) {
		vec.x = x;
		return vec;
	}

	public static Vector3 WithY(this Vector3 vec, float y) {
		vec.y = y;
		return vec;
	}

	public static Vector3 WithZ(this Vector3 vec, float z) {
		vec.z = z;
		return vec;
	}
}
