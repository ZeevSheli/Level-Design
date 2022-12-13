using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CameraBehaviour : MonoBehaviour
{
    public float maxSensitivity = 1000.0f;

    public Cinemachine.AxisState yAxis;
    public Cinemachine.AxisState xAxis;
    public Transform camera_look_at_transform;

    public Cinemachine.CinemachineVirtualCamera headCamera;
    private CinemachineComponentBase head_component_base;

    public Cinemachine.CinemachineVirtualCamera aimCamera;

    private float head_camera_max_distance = 6.0f;
    private float head_camera_min_distance = 3.0f;

    //private float zoom_lerp_timer;
    //private const float ZOOM_LERP_INTERVAL = 1.0f;

    //private float aim_camera_original_cam_distance = 2.0f;

    //public Transform camera_look_at_reverse_transform;

    public Slider sensetivity_slider;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //sensetivity_slider.value = xAxis.m_MaxSpeed / maxSensitivity;
        sensetivity_slider.value = PlayerPrefs.GetFloat("SensitivityValue", xAxis.m_MaxSpeed / maxSensitivity);

        head_component_base = headCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
    }

    // Update is called once per frame
    void Update()
    {
        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);

        camera_look_at_transform.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0.0f);
        //camera_look_at_reverse_transform.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0.0f);

        //if (Input.GetKey(KeyCode.T))
        //{
        //    camera_look_at_reverse_transform.eulerAngles = new Vector3(yAxis.Value, xAxis.Value + 180.0f, 0.0f);
        //}  

        (head_component_base as Cinemachine3rdPersonFollow).CameraDistance -= Input.mouseScrollDelta.y;
        (head_component_base as Cinemachine3rdPersonFollow).CameraDistance = Mathf.Clamp((head_component_base as Cinemachine3rdPersonFollow).CameraDistance, head_camera_min_distance, head_camera_max_distance);
    }

    public void SetSensitivity()
    {
        PlayerPrefs.SetFloat("SensitivityValue", sensetivity_slider.value);

        xAxis.m_MaxSpeed = maxSensitivity * sensetivity_slider.value;
        yAxis.m_MaxSpeed = maxSensitivity * sensetivity_slider.value;
    }
}
