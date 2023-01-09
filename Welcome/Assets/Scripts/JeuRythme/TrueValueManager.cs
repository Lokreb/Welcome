using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrueValueManager : MonoBehaviour
{
    [Header("Deposition area")]
    [SerializeField] private List<Image> _zoneList;

    [Header("Sprites")]
    [SerializeField] private List<Sprite> _spritesList;

    [Header("Temporary List")]
    public List<int> _trueValueList = new List<int>();
    public List<Sprite> _trueSpritesList = new List<Sprite>();

    //A first function to generate different IDs to identify the right shapes.
    public void GenerateTrueValue()
    {
        int _trueValue;
        //There will be 4 different shapes generated on the right
        for (int i = 0; i < 4; i++)
        {
            _trueValue = Random.Range(0, _spritesList.Capacity);
            while (_trueValueList.Contains(_trueValue))
            {
                _trueValue = Random.Range(0, _spritesList.Capacity);
            }
            _trueValueList.Add(_trueValue);
        }
    }

    //A second function to assign the right sprites and redefine the dimensions of the object according to its sprite
    public void AssignTrueValue()
    {
        GenerateTrueValue();
        for (int i = 0; i < _trueValueList.Capacity; i++)
        {
            _zoneList[i].sprite = _spritesList[_trueValueList[i]];
            _trueSpritesList.Add(_spritesList[_trueValueList[i]]);
            if (_trueValueList[i] == 0 || _trueValueList[i] == 1 || _trueValueList[i] == 2 || _trueValueList[i] == 3)
                _zoneList[i].rectTransform.sizeDelta = new Vector2(100,150);
            else
                _zoneList[i].rectTransform.sizeDelta = new Vector2(150, 150);
        }
    }
}
