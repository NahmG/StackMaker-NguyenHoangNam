using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : UICanva
{
    public int levelIndex;
    [SerializeField] Text levelIndexText;
    [SerializeField] Text scoreText;


    private void OnEnable()
    {
        levelIndex = LevelManager.Ins.currentLevel.index + 1;
    }

    private void Update()
    {
        levelIndexText.text = levelIndex.ToString();

        scoreText.text = LevelManager.Ins.score.ToString();
    }

    public void MainMenu()
    {
        UIManager.Ins.CloseUI<UI_InGame>(.2f);
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }

    public void RePlay()
    {
        Invoke(nameof(LoadLevelDelay), .2f);
    }

    public void Setting()
    {

    }

    void LoadLevelDelay()
    {
        LevelManager.Ins.LoadCurrentLevel();
    }
}
