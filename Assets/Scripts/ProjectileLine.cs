using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class ProjectileLine : MonoBehaviour
{
    static List<ProjectileLine> PROJECTILE_LINES = new List<ProjectileLine>();
    private const float DIM_MULT = 0.50f;

    private LineRenderer _line;
    public bool drawing = true;
    private Projectile _projectile;

    // Start is called before the first frame update
    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 1;
        _line.SetPosition(0, transform.position);

        _projectile = GetComponentInParent<Projectile>();

        ADD_LINE(this);
    }

    void FixedUpdate()
    {
        if (drawing)
        {
            _line.positionCount++;
            _line.SetPosition(_line.positionCount - 1, transform.position);

            if (_projectile == null || _projectile.GetComponent<Rigidbody>().velocity.magnitude <= 0.1f)
            {
                drawing = false;
            }
        }
    }

    private void OnDestroy()
    {
        PROJECTILE_LINES.Remove(this);
    }

    static void ADD_LINE(ProjectileLine newLine)
    {
        Color color;
        foreach (ProjectileLine p in PROJECTILE_LINES)
        {
            color = p._line.startColor;
            color = color * DIM_MULT;
            p._line.startColor = p._line.endColor = color;
        }
        PROJECTILE_LINES.Add(newLine);
    }
}
