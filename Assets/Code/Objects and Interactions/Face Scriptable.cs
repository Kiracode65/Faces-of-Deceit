using UnityEngine;

[CreateAssetMenu(fileName = "NewFaceScriptable", menuName = "Scriptable Objects/FaceScriptable")]
public class FaceScriptable : ScriptableObject
{
    public Sprite sprite;
    public FaceAttribute[] attributes;
}
