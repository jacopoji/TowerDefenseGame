using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    public Color hoverColor;
    private Renderer rend;
    private Color startColor;

    private GameObject turret;
    private BuildManager buildManager;

    public Vector3 buildOffset=new Vector3(0.5f,0.5f,0.5f);

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (buildManager.turretToBuild == null)
            return;
        if (buildManager.hasMoney)
        {

        rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = Color.red;
        }
    }

   public Vector3 GetBuildPosition()
    {
        return transform.position + buildOffset;
    }
    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!buildManager.canBuild)
            return;
        if(turret == null)
        {
            //build turret
            turret = buildManager.BuildTurret(this);
        }
        else
        {
            Debug.Log("Cannot build turret TODO: display on game scene");
        }
    }
}
