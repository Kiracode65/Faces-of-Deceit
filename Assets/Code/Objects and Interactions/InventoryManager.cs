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
        if (currentlyHeldFace != null)
        {
            if(currentlyHeldFace != face)
            {
                storedFaces.Remove(face.FaceDetails);

                currentlyHeldFace.StoreFace();
            }
        }

        currentlyHeldFace = face;
        face.transform.SetParent(transform);
    }

    public void StoreFace(storedFace face)
    {
        storedFaces.Add(face);
        if (SaveData.instance != null && SaveData.instance.hasLoadedData) SaveData.instance.currentData.storedFaces = storedFaces.ToArray();
    } 
}
