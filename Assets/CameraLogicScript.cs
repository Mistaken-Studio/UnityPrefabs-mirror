using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using static UnityEditor.LightmapEditorSettings;
using static UnityEngine.GraphicsBuffer;

public class CameraLogicScript : MonoBehaviour
{
    public GameObject toFollow;
    public GameObject FollowObject;

    private Quaternion lastRot;
    private Vector3 debug;
    private Quaternion startRot;

    public AnimationCurve MovementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    private AnimationCurve internalMovementCurve;

    private void Start()
    {
        internalMovementCurve = Derivative(MovementCurve);
        startRot = FollowObject.transform.localRotation;
    }

    public static AnimationCurve Derivative(AnimationCurve curve, int resolution = 100, float smoothing = .05f)
    {
        var slopes = GetPointSlopes(curve, resolution).ToArray();

        var curvePoints = slopes
            .Select((s, i) => new Vector2((float)i / resolution, s))
            .ToList();

        var simplifiedCurvePoints = new List<Vector2>();

        if (smoothing > 0)
        {
            LineUtility.Simplify(curvePoints, smoothing, simplifiedCurvePoints);
        }
        else
        {
            simplifiedCurvePoints = curvePoints;
        }

        var derivative = new AnimationCurve(
            simplifiedCurvePoints.Select(v => new Keyframe(v.x, v.y)).ToArray());

        for (int i = 0, len = derivative.keys.Length; i < len; i++)
        {
            derivative.SmoothTangents(i, 0);
        }

        return derivative;
    }

    public static float Differentiate(AnimationCurve curve, float x)
    {
        return Differentiate(curve, x, curve.keys.First().time, curve.keys.Last().time);
    }

    const float Delta = .000001f;
    public static float Differentiate(AnimationCurve curve, float x, float xMin, float xMax)
    {
        var x1 = Mathf.Max(xMin, x - Delta);
        var x2 = Mathf.Min(xMax, x + Delta);
        var y1 = curve.Evaluate(x1);
        var y2 = curve.Evaluate(x2);

        return (y2 - y1) / (x2 - x1);
    }


    static IEnumerable<float> GetPointSlopes(AnimationCurve curve, int resolution)
    {
        for (var i = 0; i < resolution; i++)
        {
            yield return Differentiate(curve, (float)i / resolution);
        }
    }

    private float rotAnim = 0;
    private bool rev = false;
    private bool isLockedOn = false;
    private void FixedUpdate()
    {
        if (toFollow != null)
        {
            var rotation = Quaternion.LookRotation(this.toFollow.transform.position - this.FollowObject.transform.position);
            var target = Quaternion.Slerp(this.FollowObject.transform.rotation, rotation, Time.deltaTime * 2.0f);
            debug = target.eulerAngles;
            float x = target.eulerAngles.x;
            //if (x > 180)
            //    x = x - 360;
            if (x > 30 && x < 180)
            {
                x = 30;
                toFollow = null;
                FollowObject.transform.localRotation = startRot;
                return;
            }
            else if (x < 330 && x > 180)
            {
                x = 330;
                toFollow = null;
                FollowObject.transform.localRotation = startRot;
                return;
            }

            target = Quaternion.Euler(new Vector3(x, Mathf.Clamp(target.eulerAngles.y, 0, 90), 0));

            if (lastRot == target)
            {
                isLockedOn = true;
                return;
            }

            isLockedOn = false;

            lastRot = target;

            //this.FollowObject.transform.LookAt(toFollow.transform.position);

            this.FollowObject.transform.rotation = target;
        }
        else
        {
            isLockedOn = false;

            this.FollowObject.transform.eulerAngles += Vector3.up * internalMovementCurve.Evaluate(rotAnim) * (rev ? -1 : 1) * (360 / 252f) / 4f;
            rotAnim += Time.fixedDeltaTime / 5;
            if (rotAnim > 1)
            {
                rev = !rev;
                rotAnim = 0;
            }
        }
    }
}
