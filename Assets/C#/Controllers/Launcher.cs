﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    #region Fields
    [SerializeField] GameObject[] payloads = null;

    Planet myPlanet;
    float timeBetweenLaunches = 0.2f;
    int maxLaunchScale = 10;
    #endregion

    #region Properties
    #endregion



    #region Methods
    void Start()
    {
        myPlanet = transform.root.GetComponent<Planet>();
    }



    public void Fire(int amt, Planet targetPlanet)
    {
        StartCoroutine(Launch(amt, targetPlanet, myPlanet.Faction));
    }



    IEnumerator Launch(int amt, Planet targetPlanet, int faction)
    {
        var indexToLaunch = 0;

        var scale = Mathf.Clamp(Mathf.FloorToInt(amt / 50) + 1, 1, maxLaunchScale);
        var myScale = 1;

        while (amt > 0 && faction == myPlanet.Faction)
        {
            myScale = Mathf.Clamp(scale, 1, amt);

            // detect type to launch
            if (targetPlanet.Population > 0 && targetPlanet.Faction != myPlanet.Faction)
            {
                indexToLaunch = 0;
            }
            else
            {
                indexToLaunch = 1;
            }

            var launchPad = Random.onUnitSphere * myPlanet.Radius;

            var payload = Instantiate(payloads[indexToLaunch], transform.position + launchPad, Quaternion.LookRotation(launchPad));
            ILaunch m = payload.GetComponent<ILaunch>();
            m.Launch(targetPlanet, myPlanet.Faction, myScale);

            amt -= myScale;

            yield return new WaitForSeconds(timeBetweenLaunches / (Mathf.Clamp(amt, 1, 200)));
        }
    }
    #endregion
}
