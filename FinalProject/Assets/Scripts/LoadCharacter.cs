using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color[] colors;

    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
        spriteRenderer = GetComponent<SpriteRenderer>();
        colors = new Color[5];
        colors[0] = Color.white;
        colors[1] = Color.cyan;
        colors[2] = Color.magenta;
        colors[3] = Color.black;
        colors[4] = Color.yellow;
        spriteRenderer.color = colors[selectedCharacter];
    }
}
