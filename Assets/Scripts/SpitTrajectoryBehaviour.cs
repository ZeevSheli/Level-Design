using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitTrajectoryBehaviour : MonoBehaviour
{
    private LineRenderer trajectory_visualization;
    private Vector3 projectile_velocity;

    public Transform look_direction_object;
    private float spit_speed;

    private int line_segmentation = 20;
    private const int LINE_LENGHT = 50;

    // Start is called before the first frame update
    void Start()
    {
        trajectory_visualization = gameObject.GetComponent<LineRenderer>();

        spit_speed = HeadBehaviour.Constants.SPIT_PROJECTILE_SPEED;

        trajectory_visualization.positionCount = LINE_LENGHT;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (look_direction_object.position - transform.position);
        lookDirection.Normalize();
        projectile_velocity = lookDirection * spit_speed;

        visualise(projectile_velocity);
    }

    void visualise(Vector3 original_velocity)
    {
        for (int i = 0; i < trajectory_visualization.positionCount; i++)
        {
            Vector3 pos = calculatePosInTime(original_velocity, (i / (float)line_segmentation));
            trajectory_visualization.SetPosition(i, pos);
        }
    }

    Vector3 calculatePosInTime(Vector3 original_velocity, float time)
    {
        Vector3 horizontal_velocity = original_velocity;
        horizontal_velocity.y = 0.0f;

        Vector3 result = transform.position + original_velocity * time;
        float y_in_time = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (original_velocity.y * time) + transform.position.y;

        result.y = y_in_time;

        return result;
    }
}
