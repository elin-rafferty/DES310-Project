using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public class VentPath : MonoBehaviour
{
    [SerializeField] private EventHandler eventHandler;
    
    Transform ventTransform;
    Transform playerTransform;
    LineRenderer lineRenderer;
    Path path;
    Seeker seeker;

    [SerializeField] private float timer;
    private bool inLevel;
    private float activateTime = 120;
    private float maxTime = 300;
    private float colourTimer = 1.0f;
    Color redColor = new Color(1, 0, 0, 0.1f);
    Color yellowColor = new Color(1, 1, 0, 0.1f);

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

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        seeker = GetComponent<Seeker>();
        lineRenderer = GetComponent<LineRenderer>();

        timer = maxTime;
    }

    void Update()
    {
        if (inLevel && ventTransform)
        {
            if (timer <= activateTime)
            {
                UpdatePath(playerTransform.position, ventTransform.position);
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
}
