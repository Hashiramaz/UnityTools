#if UNITY_EDITOR || DEVELOPMENT_BUILD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawColliderDebugLines : MonoBehaviour {

	BoxCollider2D boxCollider2D;
	Vector2 boxSize;

	
	LineDrawer lineDrawerBottom;
	LineDrawer lineDrawerRight;
	LineDrawer lineDrawerTop;
	LineDrawer lineDrawerLeft;

	public Color lineColor = Color.blue;

	private void Awake() {
			boxCollider2D = GetComponent<BoxCollider2D> ();
			boxSize = boxCollider2D.size;
	}
	void Start()
	{
		lineDrawerBottom = new LineDrawer();
		lineDrawerRight = new LineDrawer();
		lineDrawerTop = new LineDrawer();
		lineDrawerLeft = new LineDrawer();

		
	}

	void Update()
	{		
		DrawDebugLines();
	}


	
	public void DrawDebugLines(){
		
		float minx = -boxSize.x/2;
		float miny = -boxSize.y/2;
		float maxx = boxSize.x/2;
		float maxy = boxSize.y/2;

		Vector2 bl = transform.TransformPoint(new Vector2 (minx, miny));
		Vector2 br = transform.TransformPoint(new Vector2 (maxx, miny));
		Vector2 tl = transform.TransformPoint(new Vector2 (minx, maxy));
		Vector2 tr = transform.TransformPoint(new Vector2 (maxx, maxy));

		lineDrawerBottom.DrawLineInGameView (bl,br, lineColor);
		lineDrawerRight.DrawLineInGameView (br,tr, lineColor);
		lineDrawerTop.DrawLineInGameView (tr,tl, lineColor);
		lineDrawerLeft.DrawLineInGameView (tl,bl, lineColor);

		
	}
}

public struct LineDrawer
{
    private LineRenderer lineRenderer;
    private float lineSize;

    public LineDrawer(float lineSize = 0.1f)
    {
        GameObject lineObj = new GameObject("LineObj");
        lineRenderer = lineObj.AddComponent<LineRenderer>();
        //Particles/Additive
        lineRenderer.material = new Material(Shader.Find("Hidden/Internal-Colored"));
		
        this.lineSize = lineSize;
    }

    private void init(float lineSize = 0.1f)
    {
        if (lineRenderer == null)
        {
            GameObject lineObj = new GameObject("LineObj");
            lineRenderer = lineObj.AddComponent<LineRenderer>();
            //Particles/Additive
            lineRenderer.material = new Material(Shader.Find("Hidden/Internal-Colored"));
			lineRenderer.numCapVertices = 15;
            this.lineSize = lineSize;
        }
    }

    //Draws lines through the provided vertices
    public void DrawLineInGameView(Vector3 start, Vector3 end, Color color)
    {
        if (lineRenderer == null)
        {
            init(0.1f);
        }

        //Set color
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;

        //Set width
        lineRenderer.startWidth = lineSize;
        lineRenderer.endWidth = lineSize;

        //Set line count which is 2
        lineRenderer.positionCount = 2;

        //Set the postion of both two lines
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

		
    }

    public void Destroy()
    {
        if (lineRenderer != null)
        {
            UnityEngine.Object.Destroy(lineRenderer.gameObject);
        }
    }
}

#endif