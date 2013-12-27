using UnityEngine;
using System.Collections;

public class SetParticleSortingLayer : MonoBehaviour {

    public string sortingLayerName;

	// Use this for initialization
	void Start () {
	    particleSystem.renderer.sortingLayerName = sortingLayerName;
	}
}
