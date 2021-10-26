using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaunchTrajectory : MonoBehaviour
{
    private LineRenderer _lineRenderer; // LineRenderer use to draw the direction of the launch

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void DrawTrajectory(Vector3 startPoint, Vector3 endPoint)
    {
        _lineRenderer.positionCount = 2;
        Vector3[] points = { startPoint, endPoint };
        
        _lineRenderer.SetPositions(points);
    }

    public void EraseLine()
    {
        _lineRenderer.positionCount = 0;
    }
}
