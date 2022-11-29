using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public int selectedCharacter = 0;
    private SpriteRenderer spriteRenderer;
    private Color[] colors;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colors = new Color[5];
        colors[0] = Color.white;
        colors[1] = Color.cyan;
        colors[2] = Color.magenta;
        colors[3] = Color.black;
        colors[4] = Color.yellow;
    }

    public void NextCharacter()
    {
        selectedCharacter = (selectedCharacter + 1) % colors.Length;
        spriteRenderer.color = colors[selectedCharacter];
    }

    public void PreviousCharacter()
    {
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += colors.Length;
        }
        spriteRenderer.color = colors[selectedCharacter];
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
