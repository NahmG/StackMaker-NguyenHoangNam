using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Finish : UICanva
{
    [SerializeField] Text scoreText;
    Player player;
    int score;

    private void OnEnable()
    {
        player = FindObjectOfType<Player>();
        score = LevelManager.Ins.score;
        score += player.blockCollected;
        UpdateScore(score);

        Save();
    }

    public void Replay()
    {
        score = LevelManager.Ins.score;
        UIManager.Ins.CloseUI<UI_Finish>(.2f);
        LevelManager.Ins.LoadCurrentLevel();
        Invoke(nameof(ChangeGamePlay), .2f);
    }

    public void NextLevel()
    {
        Level nextLevel = LevelManager.Ins.LoadNextLevel();
        if(nextLevel != null)
        {
            LevelManager.Ins.score = score;
            UIManager.Ins.CloseUI<UI_Finish>(.2f);
            Invoke(nameof(ChangeGamePlay), .2f);
        }
        else { return; }
    }

    public void MainMenu()
    {
        UIManager.Ins.CloseUI<UI_Finish>(.2f);
        Invoke(nameof(ChangeMainMenu), .2f);

        if(LevelManager.Ins.levelIndex <4)
        {
            LevelManager.Ins.levelIndex += 1;
        }
        LevelManager.Ins.score = score;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    void ChangeGamePlay()
    {
        GameManager.Ins.ChangeState(GameState.GamePlay);
    }
    void ChangeMainMenu()
    {
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }

    void Save()
    {
        SaveData.Ins.SaveDataVal(LevelManager.Ins.levelIndex, LevelManager.Ins.score);
    }
}
