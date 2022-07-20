using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCameraController : MonoBehaviour
{
    public Transform target;
    public float angle = -90;
    public float rotateSpeed = 90f;
    public float followDistance = 5;
    public float followHeight = 3;
    public float lerpSpeed = 10;
    private Vector3 targetPos;
    public float mouseRotateSpeed = 10;

    void Start()
    {

    }

    void Update()
    {
        RotateAngle();
        Vector3 offset = GetOffSet();
        targetPos = Vector3.Lerp(targetPos, target.position + offset, Time.deltaTime * lerpSpeed);
        this.transform.position = targetPos;
    }

    void RotateAngle()
    {

        if (Input.GetKey(KeyCode.Q))
        {
            angle -= rotateSpeed * Time.deltaTime;
            KeepAngleInRange();

        }

        if (Input.GetKey(KeyCode.E))
        {
            angle += rotateSpeed * Time.deltaTime;
            KeepAngleInRange();
        }

        if (Input.GetMouseButton(1))
        {
            float mouseX = -Input.GetAxis("Mouse X") * mouseRotateSpeed;
            angle += mouseX;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    Vector3 GetOffSet()
    {
        float posX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float posZ = Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector3(posX * followDistance, followHeight, posZ * followDistance);

    }

    void KeepAngleInRange()
    {
        if (angle >= 360f)
        {
            angle = angle - 360f;
        }
        else if (angle <= 0)
        {
            angle = angle + 360f;
        }

    }
}
