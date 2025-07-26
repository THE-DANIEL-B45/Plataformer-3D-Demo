using Cloth;
using Ebac.Core.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private SaveSetup _saveSetup;
    private string path = Application.streamingAssetsPath + "/save.txt";

    public int lastLevel;

    public Action<SaveSetup> FileLoaded; 

    public SaveSetup Setup
    {
        get { return _saveSetup; }
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
    }

    private void Start()
    {
        Invoke(nameof(Load), .1f);
    }

    #region Save

    public void Save()
    {
        SaveSetup setup = _saveSetup;

        string setupToJason = JsonUtility.ToJson(setup);
        Debug.Log(setupToJason);
        SaveFile(setupToJason);
    }
    
    public void SaveItems()
    {
        _saveSetup.coins = ItemManager.Instance.GetItemByType(Itens.ItemType.COIN).soInt.value;
        _saveSetup.lifePacks = ItemManager.Instance.GetItemByType(Itens.ItemType.LIFE_PACK).soInt.value;
        _saveSetup.health = Player.Instance.healthBase._currentLife;
        _saveSetup.clothSetup = Player.Instance.clothChanger.currentClothSetup;
        Save();
    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        SaveItems();
        Save();
    }

    public void SaveName(string name)
    {
        _saveSetup.playerName = name;
        Save();
    }

    private void SaveFile(string json)
    {
        File.WriteAllText(path, json);
    }

    public void Load()
    {
        string fileLoaded = "";

        if(File.Exists(path))
        {
            fileLoaded = File.ReadAllText(path);

            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);

            lastLevel = _saveSetup.lastLevel;
        }
        else
        {
            CreateNewSave();
            Save();
        }

        FileLoaded?.Invoke(_saveSetup);
    }

    #endregion
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel = 0;
    public int coins;
    public int lifePacks;
    public float health = 50;
    public ClothSetup clothSetup = new ClothSetup { clothType = ClothType.Base};
    public string playerName = "Daniel";
}

