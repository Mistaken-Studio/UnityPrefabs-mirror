using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class CameraLogicScript : MonoBehaviour
{
    public GameObject toFollow;
    public GameObject FollowObject;
    public Light statusLight;
    public MeshRenderer statusLightBody;

    private void SetLightColor(Color color)
    {
        statusLight.color = color;
        statusLightBody.material.color = color;
    }

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

    private void ResetAnim()
    {
        rotAnim = 0;
        rev = false;
        toFollow = null;
        FollowObject.transform.localRotation = startRot;
    }


    private float rotAnim = 0;
    private bool rev = false;
    private bool isLockedOn = false;

    private float uselessDebug = 0;
    private void FixedUpdate()
    {
        if (toFollow != null)
        {
            var rotation = Quaternion.LookRotation(this.toFollow.transform.position - this.FollowObject.transform.position + Vector3.up * .5f);
            var target = Quaternion.Slerp(this.FollowObject.transform.rotation, rotation, Time.deltaTime * 2.0f);
            debug = target.eulerAngles;
            float x = target.eulerAngles.x;
            //if (x > 180)
            //    x = x - 360;
            if (x > 40 && x < 180)
            {
                ResetAnim();
                return;
            }
            else if (x < 320 && x > 180)
            {
                ResetAnim();
                return;
            }

            float y = target.eulerAngles.y;
            //UnityEngine.Debug.Log(y - startRot.eulerAngles.y);
            if (y - startRot.eulerAngles.y > 110)
            {
                ResetAnim();
                return;
            }
            else if (y - startRot.eulerAngles.y < 0)
            {
                ResetAnim();
                return;
            }

            target = Quaternion.Euler(new Vector3(x, y, 0));

            if (lastRot == target)
            {
                isLockedOn = true;
                SetLightColor(Color.red);
                return;
            }

            isLockedOn = false;

            lastRot = target;

            //this.FollowObject.transform.LookAt(toFollow.transform.position);

            SetLightColor(Color.yellow);
            this.FollowObject.transform.rotation = target;
        }
        else
        {
            SetLightColor(Color.green);
            isLockedOn = false;
            // * (360 / 252f) / 4f
            var tmp = Vector3.up * MovementCurve.Evaluate(rotAnim) * 90;

            uselessDebug += tmp.y * Time.fixedDeltaTime * 100f;

            this.FollowObject.transform.eulerAngles = tmp;
            rotAnim += Time.fixedDeltaTime / 5 * (rev ? -1 : 1);
            float dumb = this.FollowObject.transform.localEulerAngles.y;

            /*if (Mathf.FloorToInt(dumb) > 45f)
            {
                rotAnim = 0;
                rev = true;
                this.FollowObject.transform.localEulerAngles -= Vector3.up * (this.FollowObject.transform.localEulerAngles.y - 45);
            }
            else if (Mathf.FloorToInt(dumb) < -45)
            {
                rotAnim = 0;
                rev = false;
                this.FollowObject.transform.localEulerAngles -= Vector3.up * (this.FollowObject.transform.localEulerAngles.y + 45);
            }*/

            if(rotAnim > 1)
            {
                //rotAnim = 0;
                rev = true;
            }

            else if (rotAnim <= 0)
            {
                //rotAnim = 0;
                rev = false;
            }
        }
    }

    private IEnumerator<float> DoAnim()
    {
        yield break;
    }
}
