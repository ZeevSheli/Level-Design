using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float view_radius;
    [Range(0,360)]
    public float view_angle;

    public LayerMask player_mask;
    public LayerMask obstacle_mask;

    [HideInInspector]
    public List<Transform> visible_targets = new List<Transform>();

	public Transform target;
	public float angular_speed = 1.0f;
    public Vector3 targetPosition;

    private void FixedUpdate()
    {
        findVisibleTargets();

  //      if(FindObjectOfType<TeacherBehaviour>().mostInterestingSound != null)
  //      {
  //          targetPosition = FindObjectOfType<TeacherBehaviour>().mostInterestingSound.point;
  //      }
		//Vector3 target_direction = targetPosition - transform.position;
		//float speed = angular_speed * Time.deltaTime;
		//Vector3 new_direction = Vector3.RotateTowards(transform.forward, target_direction, speed, 0.0f);
		//transform.rotation = Quaternion.LookRotation(new_direction);
    }

    void findVisibleTargets()
    {
        visible_targets.Clear();

        Collider[] targets_in_view_radius = Physics.OverlapSphere(transform.position, view_radius, player_mask);
        {
            for(int i = 0; i < targets_in_view_radius.Length; i++)
            {
                Transform target = targets_in_view_radius[i].transform;
                Vector3 direction_to_target = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, direction_to_target) < view_angle / 2)
                {
                    float distance_to_target = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast (transform.position, direction_to_target, distance_to_target, obstacle_mask))
                    {
                        visible_targets.Add(target);
                        HealthRespawnBehaviour.increaseSeen(0.08f);
                    }

                }
            }
        }
    }

    public Vector3 directionFromAngle(float angle_in_degrees, bool angle_is_global)
    {
        if(!angle_is_global)
        {
            angle_in_degrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angle_in_degrees * Mathf.Deg2Rad), 0, Mathf.Cos(angle_in_degrees * Mathf.Deg2Rad));
    }
}
