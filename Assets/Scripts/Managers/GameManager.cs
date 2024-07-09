using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private static GameState _state;

    //public static event Action<GameState> OnGameStateChanged;

    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    public static bool IsState(GameState state) => _state == state;

    public void ChangeState(GameState newState)
    {
        _state = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                MainMenu();
                break;
            case GameState.GamePlay:
                GamePlay();
                break;
            case GameState.Finish:
                Finish();
                break;
        }

        //OnGameStateChanged?.Invoke(newState);
    }

    private void MainMenu()
    {
        UIManager.Ins.OpenUI<UI_MainMenu>();
    }

    private void GamePlay()
    {
        UIManager.Ins.OpenUI<UI_InGame>();   
    }

    private void Finish()
    {
        UIManager.Ins.CloseUI<UI_InGame>(0f);
        UIManager.Ins.OpenUI<UI_Finish>();
    }
}



public enum GameState
{
    MainMenu,
    GamePlay,
    Finish,
    //Revive,
    //Setting
}
