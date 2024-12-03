using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadStatsInScroll : MonoBehaviour
{
    [SerializeField] private GameObject _statsPrefab;
    

    private SaveManager _saveManager;

    private List<SaveManager.GameTimeData> _gameTimeDataList = new List<SaveManager.GameTimeData>();
    private void OnEnable()
    {
        _saveManager = new SaveManager();
        _gameTimeDataList = _saveManager.LoadGameTimeData();
        SetupStats();
    }



    private void SetupStats()
    {
        for (int i = _gameTimeDataList.Count - 1; i >= 0; i--)
        {
            GameObject stats = Instantiate(_statsPrefab, transform);
            stats.GetComponent<StatPanel>().SetupPanel(_gameTimeDataList[i]);
        }
    }
}
