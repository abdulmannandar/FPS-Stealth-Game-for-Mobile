using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public FixedTouchField _FixedTouchField;
    public CameraLook _CameraLook; 
    public RifleGun _GunButn;
    public RifleContinous _Cont;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _CameraLook.LockAxis = _FixedTouchField.TouchDist;
        _GunButn.Pressed = _Cont.Pressed;  

    }
}
