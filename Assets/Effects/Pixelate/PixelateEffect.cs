using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class PixelateEffect : MonoBehaviour {

	public int height = 144;
	int cachedHeight;

	private Camera mainCamera;
	private RenderTexture screen;

	// Use this for initialization
	void OnEnable () {
		height = (int)Mathf.Max(height, 1f);
		cachedHeight = height;

		mainCamera = GetComponent<Camera>();

		screen = new RenderTexture((int)(height * mainCamera.aspect), height, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
		screen.filterMode = FilterMode.Point;
		screen.Create();
	}

	void Update() {
		if (cachedHeight != height) {
			if (screen != null) {
				screen.Release();
			}

			height = (int)Mathf.Max(height, 1f);
			cachedHeight = height;
			screen = new RenderTexture((int)(height * mainCamera.aspect), height, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
			screen.filterMode = FilterMode.Point;
			screen.Create();
		}
	}
	
	void OnRenderImage(RenderTexture src, RenderTexture dest) {
		Graphics.Blit(src, screen);
		Graphics.Blit(screen, dest);
	}

	void OnDestroy() {
		if (screen != null) {
			screen.Release();
		}
	}
}
