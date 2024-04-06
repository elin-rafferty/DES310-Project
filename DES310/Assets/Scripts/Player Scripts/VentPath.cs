using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentPath : MonoBehaviour
{
    [SerializeField] private EventHandler eventHandler;
    
    Transform ventTransform;
    LineRenderer lineRenderer;
    Path path;
    Seeker seeker;

    private bool inLevel;
    private bool isVisible;
    private float colourTimer = 1.0f;
    Color redColor = new Color(1, 0, 0, 0.1f);
    Color yellowColor = new Color(1, 1, 0, 0.1f);

    private void Awake()
    {
        inLevel = false;
        isVisible = false;
        eventHandler.LevelEnter.AddListener(SetLevelStatus);
    }

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Vent"))
        {
            ventTransform = GameObject.FindGameObjectWithTag("Vent").transform;
        }

        seeker = GetComponent<Seeker>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (inLevel && ventTransform && isVisible)
        {
            UpdatePath(transform.position, ventTransform.position);
            UpdateLine();
        }
        else
        {
            lineRenderer.startColor = Color.clear;
            lineRenderer.endColor = Color.clear;
        }
    }

    private void UpdatePath(Vector3 start, Vector3 end)
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(start, end, OnPathComplete);
        }
    }

    private void OnPathComplete(Path newPath)
    {
        if (!newPath.error)
        {
            path = newPath;
        }
    }

    private void UpdateLine()
    {
        if (path != null)
        {
            lineRenderer.positionCount = path.vectorPath.Count - 1;

            for (int i = 0; i < path.vectorPath.Count - 1; i++)
            {
                lineRenderer.SetPosition(i, path.vectorPath[i] + Vector3.forward * 2);
            }

            // Set line colour
            if (colourTimer > 0 && colourTimer < 1)
            {
                lineRenderer.startColor = Color.Lerp(yellowColor, redColor, colourTimer);
                lineRenderer.endColor = Color.Lerp(yellowColor, redColor, colourTimer);
                colourTimer += Time.deltaTime;
            }
            else if (colourTimer >= 1)
            {
                colourTimer = -0.001f;
            }
            else if (colourTimer < 0 && colourTimer > -1)
            {
                lineRenderer.startColor = Color.Lerp(redColor, yellowColor, Mathf.Abs(colourTimer));
                lineRenderer.endColor = Color.Lerp(redColor, yellowColor, Mathf.Abs(colourTimer));
                colourTimer -= Time.deltaTime;
            }
            else if (colourTimer < -1)
            {
                colourTimer = 0.001f;
            }
        }
    }

    private void SetLevelStatus()
    {
        inLevel = true;
    }

    public void SetVisible(bool visible) 
    {
        isVisible = visible;
    }
}
