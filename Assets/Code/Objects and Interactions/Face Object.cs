using System;
using System.Collections.Generic;
using UnityEngine;

public enum FaceAttribute
{
    normal,
    damaged,
    young,
    old,
    pale,
    dark,
}
public class FaceObject : MonoBehaviour
{
    public List<FaceAttribute> attributes;
    public int Value;

    public void ReturnToShelf() // havent figured out how to do this yet, probably needs a shelf manager
    {
        throw new NotImplementedException();
        Destroy(gameObject);
    }

    public void SellFace() // function here just in case we want the faces to do anything before dying, like an animation or smthn
    {
        Destroy(gameObject);
    }
}
