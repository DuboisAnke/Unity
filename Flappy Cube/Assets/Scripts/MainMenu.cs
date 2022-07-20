using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public List<LevelScriptableObject> levels = null;
    public Button levelButtonTemp;

    void Start()
    {

        CreateButtons();
    }

    void CreateButtons()
    {
        levelButtonTemp.gameObject.SetActive(true);
        foreach (var level in levels)
        {
            Button generatedButton = Instantiate<Button>(levelButtonTemp, levelButtonTemp.transform.parent);
            generatedButton.GetComponentInChildren<Text>().text = level.name;
            generatedButton.onClick.AddListener(() => LevelLoader.LoadLevel(level));
        }
        levelButtonTemp.gameObject.SetActive(false);
    }
}
