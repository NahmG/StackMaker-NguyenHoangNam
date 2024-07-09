using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Level[] levels;
    [SerializeField] Player player;

    public Level currentLevel;

    public int levelIndex;

    public int score;

    private void Start()
    {
        //OnLoadLevel(0);
    }

    public void OnInit()
    {
        OnLoadLevel(0);
        score = 0;
    }

    public void OnLoadLevel(int level)
    {
        if(currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levels[level]);
        levelIndex = currentLevel.index;

        player.OnDespawn();
        player.OnInit();
    }

    public Level LoadNextLevel()
    {
        int nextLevel = levelIndex + 1;
        if (nextLevel < levels.Length)
        {
            OnLoadLevel(levelIndex + 1);
            return levels[nextLevel];
        }
        else { return null; }

    }
    public void LoadCurrentLevel()
    {
        OnLoadLevel(levelIndex);
    }
}
