using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VentPath : MonoBehaviour
{
    [SerializeField] private EventHandler eventHandler;
    
    Transform ventTransform;
    LineRenderer lineRenderer;
    Path path;
    Seeker seeker;

    private bool inLevel;
    [SerializeField] private float timer;

    void Start()
    {
        inLevel = false;
        eventHandler.LevelEnter.AddListener(setLevelStatus);

        if (GameObject.FindGameObjectWithTag("Vent"))
        {
            ventTransform = GameObject.FindGameObjectWithTag("Vent").transform;
        }

        seeker = GetComponent<Seeker>();
        lineRenderer = GetComponent<LineRenderer>();

        timer = 300;
    }

    void Update()
    {
        if (inLevel && ventTransform)
        {
            if (timer <= 120)
            {
                UpdatePath(transform.position, ventTransform.position);
                UpdateLine();
            }
            else
            {
                timer -= Time.deltaTime;
            }
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
        if (path.vectorPath.Count > 0)
        {
            lineRenderer.positionCount = path.vectorPath.Count;

            for (int i = 0; i < path.vectorPath.Count; i++)
            {
                lineRenderer.SetPosition(i, path.vectorPath[i]);
            }
        }
    }

    private void setLevelStatus()
    {
        inLevel = true;
    }
}
