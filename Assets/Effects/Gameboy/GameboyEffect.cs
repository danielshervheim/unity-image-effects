using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class GameboyEffect : MonoBehaviour {

	public GameboyColorPalette colorPalette;

	private Camera mainCamera;
	private RenderTexture screen;
	private Material material;

	// Use this for initialization
	void OnEnable () {
		mainCamera = GetComponent<Camera>();

		screen = new RenderTexture((int)(144 * mainCamera.aspect), 144, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
		screen.filterMode = FilterMode.Point;
		screen.Create();

		material = new Material(Shader.Find("Hidden/GameboyEffect"));
	}

	void Update() {
		material.SetColor("_Color1", colorPalette.color1);
		material.SetFloat("_Transition12", colorPalette.transition12);
		material.SetColor("_Color2", colorPalette.color2);
		material.SetFloat("_Transition23", colorPalette.transition23);
		material.SetColor("_Color3", colorPalette.color3);
		material.SetFloat("_Transition34", colorPalette.transition34);
		material.SetColor("_Color4", colorPalette.color4);
	}
	
	void OnRenderImage(RenderTexture src, RenderTexture dest) {
		if (colorPalette != null) {
			Graphics.Blit(src, screen);
			Graphics.Blit(screen, dest, material);
		}
	}

	void OnDestroy() {
		if (screen != null) {
			screen.Release();
		}
	}
}
