using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : UICanva
{
    public Button playButton;

    private void OnEnable()
    {
        LoadSave();
    }

    public void Play()
    {
        UIManager.Ins.CloseUI<UI_MainMenu>(.2f);
        LevelManager.Ins.OnInit();
        Invoke(nameof(ChangeToGamePlay), .2f);

    }

    public void Resume()
    {
        UIManager.Ins.CloseUI<UI_MainMenu>(.2f);
        LevelManager.Ins.LoadCurrentLevel();
        Invoke(nameof(ChangeToGamePlay), .2f);
    }

    void ChangeToGamePlay()
    {
        GameManager.Ins.ChangeState(GameState.GamePlay);
    }

    void LoadSave()
    {
        Save save = SaveData.Ins.LoadData();

        LevelManager.Ins.levelIndex = save.levelIndex;
        LevelManager.Ins.score = save.score;    
    }
}
