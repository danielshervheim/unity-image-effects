using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class PosterizeEffect : MonoBehaviour {
	
	public int numberOfBins = 10;
	int cachedNumberOfBins;
	
	Material posterizeMaterial;

	void OnEnable () {
		// Assign the material and save the number of bins.
		posterizeMaterial = new Material(Shader.Find("Hidden/Posterize"));
		cachedNumberOfBins = numberOfBins-1;
	}

	void OnValidate() {
		numberOfBins = (int)Mathf.Max(numberOfBins, 1f);
	}

	void Update () {
		// Update the material if the conversion mode has changed.
		if (cachedNumberOfBins != numberOfBins) {
			posterizeMaterial.SetInt("_NumberOfBins", numberOfBins);
			cachedNumberOfBins = numberOfBins;
		}
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest) {
		Graphics.Blit(src, dest, posterizeMaterial);
	}
}
