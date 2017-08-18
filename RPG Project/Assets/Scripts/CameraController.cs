using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;                // цель камеры
    public Vector3 offset;                  // отступ от цели
    public float zoomSpeed = 4f;            // скорость зума
    public float minZoom = 5f;              // минимальный зум
    public float maxZoom = 15f;             // максимальный зум
    public float yawSpeed = 100f;           // скорость поворота камеры
    public float pitch = 2f;                // наклон камеры

    private float currentZoom = 10f;        // текущий зум
    private float currentYaw = 0f;          // текущий поврот камеры

    void Update()
    {
        // рассчитываем зум
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // рассчитываем поврот учитывая значения горизонтальной оси ввода (клавиши стрелки влево/вправо, A, D)
        currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }

    void LateUpdate () {
        // применяем перемещение камеры и зум
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);

        // применяем поворот
        transform.RotateAround(target.position, Vector3.up, currentYaw);
	}
}
