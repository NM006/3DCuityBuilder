using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed;

    public float minXRotation;
    public float maxXRotation;

    private float currentXRotation;

    public float minZoom;
    public float maxZoom;

    public float zoomSpeed;
    public float rotateSpeed;

    private float currentZoom;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        currentZoom = cam.transform.localPosition.y;
        currentXRotation = -60;
    }

    private void Update()
    {
        // zooming
        currentZoom = currentZoom + Input.GetAxis("Mouse ScrollWheel") * (-zoomSpeed);
        // Clamp sorgt dafür das wir mit currentZoom nie unter/höher minZoom gehen können
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        cam.transform.localPosition = Vector3.up * currentZoom;

        // camera look
        if (Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            // KameraRotation
            currentXRotation = currentXRotation + (-y) * rotateSpeed;
            currentXRotation = Mathf.Clamp(currentXRotation, minXRotation, maxXRotation);
            transform.eulerAngles = new Vector3(currentXRotation, transform.eulerAngles.y + (x * rotateSpeed), 0.0f);
        }

        // movement
        // vorwärtsbewegung
        Vector3 forward = cam.transform.forward;
        forward.y = 0.0f; // setzt die vorwärtsbewegung auf y = 0
        forward.Normalize(); // Normalize setzt die Größe des Vectors wieder auf 1
        // nach rechts bewegen
        Vector3 right = cam.transform.right;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // konvertiert diese Inputs zu einer lokalen Richtung relativ zu dem wo unsere Kamera hinschaut
        Vector3 direction = forward * moveZ + right * moveX;
        direction.Normalize(); // Normalize setzt die Größe des Vectors wieder auf 1

        direction = direction * moveSpeed * Time.deltaTime;

        transform.position = transform.position + direction;
    }

}
