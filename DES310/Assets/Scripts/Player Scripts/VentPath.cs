using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VentPath : MonoBehaviour
{
    [SerializeField] private EventHandler eventHandler;
    
    Transform ventTransform;
    [SerializeField] LineRenderer lineRenderer;
    Path path;
    Seeker seeker;

    private bool inLevel;
    [SerializeField] private float timer;

    private void Awake()
    {
        inLevel = false;
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
        if (path != null)
        {
            lineRenderer.positionCount = path.vectorPath.Count - 4;

            for (int i = 0; i < path.vectorPath.Count - 4; i++)
            {
                lineRenderer.SetPosition(i, path.vectorPath[i + 3]);
            }
        }
    }

    private void SetLevelStatus()
    {
        inLevel = true;
    }
}
