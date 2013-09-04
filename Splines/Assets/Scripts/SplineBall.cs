using UnityEngine;
using System.Collections.Generic;

public class SplineBall : MonoBehaviour {

    public Bezier MyBezier;
    public Transform Point1;
    public Transform Point2;
    public Transform Point3;
    public Transform Point4;
    public float Speed;
    public bool OnSpline;

    private float _time = 0f;
    private Dictionary<Vector3, float> _splinePointDict;
    private bool _init;

    void Start()
    {
        _splinePointDict = new Dictionary<Vector3, float>();

        MyBezier = new Bezier(Point1.position, Point2.position, Point3.position, Point4.position);
    }

    void Update()
    {
        if(!_init)
        {
            Initialize();
        }

        if (OnSpline)
        {
            Vector3 vec = MyBezier.GetPointAtTime(_time);
            transform.position = vec;

            _time += 0.01f*(UnityEngine.Time.deltaTime*Speed);
            if (_time > 1f)
            {
                _time = 0f;
            }
        }

        else
        {
            SplineDistanceCheck();

            //We have not yet joined the spline and should still move forward
            this.transform.Translate(0, -UnityEngine.Time.deltaTime * Speed, 0);
        }
    }

    void Initialize()
    {
        _init = true; //Run Once

        DrawLine drawLineCache = GameObject.Find("DrawLine").GetComponent<DrawLine>();
        this._splinePointDict = drawLineCache.GetSplineDict();
    }

    void SplineDistanceCheck()
    {
        foreach (var pair in _splinePointDict)
        {
            //If we are very near the spline join it.
            if (Vector3.Distance(this.transform.position, pair.Key) <= 0.5f)
            {
                //Set the position and time for nearest Spline Point
                this.transform.position = pair.Key;
                this._time = pair.Value;

                //Finish joining Spline
                OnSpline = true;
                Debug.Log("Spline Hooked!");
                return;
            }
        }
    }
}