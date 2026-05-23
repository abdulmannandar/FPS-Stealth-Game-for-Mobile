using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    private float XMove;
    private float YMove;
    private float XRotation;
    [SerializeField] private Transform Player;
    public Vector2 LockAxis;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float Sensitivity = SensitivityManager.Instance.Sensitivity;
        XMove = LockAxis.x * Sensitivity * Time.deltaTime;
        YMove = LockAxis.y * Sensitivity * Time.deltaTime;
        XRotation -= YMove;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(XRotation,0,0);
        Player.Rotate(Vector3.up * XMove);
    }
}
