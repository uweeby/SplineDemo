using UnityEngine;
using System.Collections.Generic;

public class DrawLine : MonoBehaviour {

    public Bezier MyBezier;

    public Transform Point1;
    public Transform Point2;
    public Transform Point3;
    public Transform Point4;

    private Dictionary<Vector3, float> _splinePointDict = new Dictionary<Vector3, float>();

    private LineRenderer _lineRenderer;

	// Use this for initialization
	void Start ()
	{
	    _lineRenderer = this.GetComponent<LineRenderer>();

        MyBezier = new Bezier(Point1.position, Point2.position, Point3.position, Point4.position);

	    GenerateSpline();
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    void GenerateSpline()
    {
        List<Vector3> linePositions = new List<Vector3>();
        Vector3 currentPosition = Point1.position;

        for(float time = 0.0f; time < 1.0f; time = time + 0.01f)
        {
            time += 0.01f; //Timestep
            Vector3 nextPosition = MyBezier.GetPointAtTime(time);

            //Add new data to Dict and List
            _splinePointDict.Add(nextPosition, time);
            linePositions.Add(nextPosition);

            //Move for next loop
            currentPosition = nextPosition;
        }

        _lineRenderer.SetVertexCount(linePositions.Count);
        for (var i = 0; i < linePositions.Count; i++)
        {
            _lineRenderer.SetPosition(i, linePositions[i]);
        }
        
    }

    public Dictionary<Vector3, float> GetSplineDict()
    {
        return this._splinePointDict;
    }
}
