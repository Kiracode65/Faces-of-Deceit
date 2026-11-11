using System;
using System.Collections.Generic;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    public FaceObject currentlyHeldFace;
    public static InventoryManager Instance;
    public List<storedFace> storedFaces;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PickupFace(FaceObject face)
    {
        storedFaces.Remove(face.FaceDetails);

        if (currentlyHeldFace != null)
        {
            currentlyHeldFace.StoreFace();
            //currentlyHeldFace.StoreFace();
        }

        currentlyHeldFace = face;
        face.transform.SetParent(transform);
    }

    public void StoreFace(FaceObject face)
    {
        storedFaces.Add(face.FaceDetails);
    }

  
}

[Serializable]
public struct storedFace
{
    public FaceScriptable faceScriptable;
    public int assignedValue;
}
