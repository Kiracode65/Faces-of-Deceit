using UnityEngine;

public class ShelfLayout : MonoBehaviour
{
    [SerializeField] private Transform[] slots;
    [SerializeField] private FaceObject facePrefab;

    private void Start()
    {
        InstantiateInventory(InventoryManager.Instance.storedFaces.ToArray());
    }
    public void InstantiateInventory(storedFace[] faces)
    {
        if(faces.Length > slots.Length)
        {
            Debug.LogError("Too many faces, no space");
            return;
        }

        for (int i = 0; i < faces.Length; i++)
        {
            FaceObject newface = Instantiate(facePrefab, slots[i]);
            newface.initialise(faces[i]);
        }
    }

    public void RestoreFace(FaceObject face)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].childCount == 0) 
            {
                face.transform.SetParent(slots[i]);
                break;
            }
        }
    }
}
