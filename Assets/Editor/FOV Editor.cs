using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{

	//void OnSceneGUI()
	//{
	//	FieldOfView fow = (FieldOfView)target;
	//	Handles.color = Color.white;
	//	Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.view_radius);
	//	Vector3 viewAngleA = fow.directionFromAngle(-fow.view_angle / 2, false);
	//	Vector3 viewAngleB = fow.directionFromAngle(fow.view_angle / 2, false);

	//	Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.view_radius);
	//	Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.view_radius);

 //       Handles.color = Color.red;
 //       foreach (Transform visible_target in fow.visible_targets)
 //       {
 //           Handles.DrawLine(fow.transform.position, visible_target.position);
 //       }
 //   }

}