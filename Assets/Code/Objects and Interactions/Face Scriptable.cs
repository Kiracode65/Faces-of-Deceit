using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFaceScriptable", menuName = "Scriptable Objects/FaceScriptable")]
public class FaceScriptable : ScriptableObject
{
    public Sprite sprite;
    public string description;
    public List<FaceAttribute> attributes;
}

[Serializable]
public struct storedFace
{
    public FaceScriptable faceScriptable;
    public int assignedValue;
}
