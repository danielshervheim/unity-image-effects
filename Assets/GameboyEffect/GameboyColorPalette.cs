using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gameboy Color Palette", menuName = "Gameboy Color Pallete")]
public class GameboyColorPalette : ScriptableObject {
    public Color color1;
    public float transition12 = 0.25f;
    public Color color2;
    public float transition23 = 0.5f;
    public Color color3;
    public float transition34 = 0.75f;
    public Color color4;
}
