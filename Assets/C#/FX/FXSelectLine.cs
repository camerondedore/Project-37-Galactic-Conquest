﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXSelectLine : MonoBehaviour
{
    #region Fields
    LineRenderer lineRend;
    #endregion

    #region Properties
    #endregion



    #region Methods
    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
    }



    public void DrawLine(Planet attackPlanet, Planet targetPlanet)
    {
        lineRend.positionCount = 2;
        Vector3[] points = new Vector3[2];

        points[0] = attackPlanet.transform.position;
        points[1] = targetPlanet.transform.position;

        lineRend.SetPositions(points);
    }



    public void ClearLine()
    {
        lineRend.positionCount = 0;
    }
    #endregion
}
