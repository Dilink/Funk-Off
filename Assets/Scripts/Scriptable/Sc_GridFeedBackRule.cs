using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGridFeedbackRule", menuName = "Custom/GridFeedbackRule")]
public class Sc_GridFeedBackRule : ScriptableObject
{
    public Material iceMaterial;
    public Material slowMaterial;
    public Material damagingMaterial;

    public Material PreviewTileMaterial;
    public Material excitedGrid;
    public Material calmGrid;

}
