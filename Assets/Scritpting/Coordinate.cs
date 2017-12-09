using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinate {

    public static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;

    public static float size = 1.01f;

    public static Vector3 Axial2Cube(Vector2 axial)
    {
        Vector3 cube = new Vector3();
        cube.x = axial.x;
        cube.z = axial.y;
        cube.y = -axial.x - axial.y;
        return cube;
    }

    public static Vector3 Offset2Cube(Vector2 off)
    {
        Vector3 cube = new Vector3();
        cube.x = off.x - (off.y - ((int)off.y & 1)) / 2;
        cube.z = off.y;
        cube.y = -cube.x - cube.z;
        return cube;
    }

    public static Vector2 Cube2Axial(Vector3 cube)
    {
        return new Vector2(cube.x, cube.z);
    }

    public static Vector2 Cube2Offset(Vector3 cube)
    {
        Vector2 off = new Vector2();
        off.x = cube.x + (cube.z - ((int)cube.z & 1)) / 2;
        off.y = cube.z;
        return off;
    }

    public static Vector3 Offset2Real(Vector2 off)
    {
        float height = size * 2;
        float width = height * WIDTH_MULTIPLIER;

        float vert = height * 0.75f;
        float horiz = width;

        Vector3 real = new Vector3(
            horiz * (off.x + ((int)off.y & 1) / 2f),
            0,
            vert * off.y
        );

        return real;
    }

    public static Vector3 Cube2Real(Vector3 cube)
    {
        Vector3 baseX = new Vector3(Mathf.Cos(30 * Mathf.Deg2Rad), 0, Mathf.Sin(30 * Mathf.Deg2Rad)) * size;
        Vector3 baseY = new Vector3(Mathf.Cos(150 * Mathf.Deg2Rad), 0, Mathf.Sin(150 * Mathf.Deg2Rad)) * size;
        Vector3 baseZ = new Vector3(Mathf.Cos(-90 * Mathf.Deg2Rad), 0, Mathf.Sin(-90 * Mathf.Deg2Rad)) * size;

        Matrix4x4 trans = new Matrix4x4(
            baseX, baseY, baseZ, new Vector4(0, 0, 0, 1));

        return trans * cube;
    }

    public static Vector2 Real2Offset(Vector2 real)
    {
        Vector2 off = new Vector2();
        off.x = (real.x * Mathf.Sqrt(3) / 3 - real.y / 3) / size;
        off.y = real.y * 2 / 3 / size;
        return off;
    }
    public static Vector3 RoundReal2Cube(Vector2 real)
    {
        return RoundCube(Offset2Cube(Real2Offset(real)));
    }

    public static Vector3 RoundCube(Vector3 cube)
    {
        float rx = Mathf.Round(cube.x);
        float ry = Mathf.Round(cube.y);
        float rz = Mathf.Round(cube.z);

        float x_diff = Mathf.Abs(rx - cube.x);
        float y_diff = Mathf.Abs(ry - cube.y);
        float z_diff = Mathf.Abs(rz - cube.z);

        if (x_diff > y_diff && x_diff > z_diff)
            rx = -ry - rz;
        else if (y_diff > z_diff)
            ry = -rx - rz;
        else
            rz = -rx - ry;

        return new Vector3(rx, ry, rz);
    }
}
