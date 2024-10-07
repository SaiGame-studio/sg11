using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinesDrawer : SaiMonoBehaviour
{
    [SerializeField] protected LineRenderer lineRenderer;
    [SerializeField] protected float lineWidth = 0.1f;

    protected override void LoadComponents()
    {
        this.LoadLineRender();
    }

    protected virtual void LoadLineRender()
    {
        if (this.lineRenderer != null) return;
        this.lineRenderer = GetComponent<LineRenderer>();
        Debug.LogWarning(transform.name + " LoadLineRender", gameObject);
    }

    public virtual void Drawing(List<Node> nodes, float cleanDelay)
    {
        List<Vector3> points = new List<Vector3>();
        foreach(Node node in nodes)
        {
            Vector3 point = node.nodeObj.transform.position;
            points.Add(point);
        }

        this.DrawingPoints(points);
        Invoke(nameof(this.Clean), cleanDelay);
    }

    protected virtual void DrawingPoints(List<Vector3> points)
    {
        this.lineRenderer.positionCount = points.Count;

        Vector3 point;
        for (int i = 0; i < points.Count; i++)
        {
            point = points[i];
            this.lineRenderer.SetPosition(i, point);
        }
    }

    protected virtual void Clean()
    {
        this.lineRenderer.positionCount = 0;
    }
}
