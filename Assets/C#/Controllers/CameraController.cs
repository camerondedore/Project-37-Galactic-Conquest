﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraController : MonoBehaviour
{
	#region Fields
	[SerializeField] bool startAtMyPlanet = true;

    Transform mainCam;
    Vector3 cameraDir;
    int moveAreaWidth = 50;
    float moveSpeed = 20,
        zoomSpeed = 200,
        maxZoom = 30,
        minZoom = 4,
        zoom = 10,
        multiplier = 3;
    #endregion

    #region Properties
    #endregion



    #region Methods
    void Start()
    {
		mainCam = Camera.main.transform;
        cameraDir = new Vector3(0, 0.5f, -1).normalized;

		if (Planet.GetCountOfMyPlanets(1) > 0 && startAtMyPlanet)
		{
			var startingPos = Planet.GetMyPlanets(1)[0].transform.position;
			startingPos.y = 0;
			transform.position = startingPos;
		}
		mainCam.forward = -cameraDir;
    }



    void Update()
    {
        MoveCamera();
        ZoomCamera();
    }



    void ZoomCamera()
    {
        if (Input.GetAxisRaw("Zoom") != 0)
        {
            zoom -= Input.GetAxisRaw("Zoom") * zoomSpeed * Time.deltaTime;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        }

        mainCam.position = Vector3.Lerp(mainCam.position, transform.position + cameraDir * zoom, 10 * Time.deltaTime);
    }



    void MoveCamera()
    {
        // mouse
        var mouse2dCoord = Input.mousePosition;
        var x = mouse2dCoord.x < moveAreaWidth ? -1 : (mouse2dCoord.x > (Screen.width - moveAreaWidth) ? 1 : 0);
        var z = mouse2dCoord.y < moveAreaWidth ? -1 : (mouse2dCoord.y > (Screen.height - moveAreaWidth) ? 1 : 0);

        // keyboard
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            x = Mathf.CeilToInt(Input.GetAxisRaw("Horizontal"));
            z = Mathf.CeilToInt(Input.GetAxisRaw("Vertical"));
        }

        var mult = Mathf.Clamp(Input.GetAxisRaw("Fast Move") * multiplier, 1, multiplier);

        transform.position += new Vector3(x, 0, z).normalized * Time.deltaTime * moveSpeed * mult;
    }
    #endregion
}
