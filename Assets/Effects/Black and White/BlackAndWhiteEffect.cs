using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class BlackAndWhiteEffect : MonoBehaviour {
	
	public enum Mode {PerceivedBrightness, AverageValue};

	public Mode conversionMode = Mode.PerceivedBrightness;
	Mode cachedConversionMode;
	
	Material blackAndWhiteMaterial;

	void OnEnable () {
		// Assign the material and save the conversion mode.
		blackAndWhiteMaterial = new Material(Shader.Find("Hidden/BlackAndWhite"));
		cachedConversionMode = conversionMode;
		blackAndWhiteMaterial.SetInt("_ComputeLuminance", conversionMode==Mode.PerceivedBrightness ? 1 : 0);
	}

	void Update () {
		// Update the material if the conversion mode has changed.
		if (cachedConversionMode != conversionMode) {
			blackAndWhiteMaterial.SetInt("_ComputeLuminance", conversionMode==Mode.PerceivedBrightness ? 1 : 0);
			cachedConversionMode = conversionMode;
		}
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest) {
		Graphics.Blit(src, dest, blackAndWhiteMaterial);
	}
}
