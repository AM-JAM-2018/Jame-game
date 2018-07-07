using NPCs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Race = NPCs.NPC.RaceEnum;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "ImageDatabase.asset", menuName = "ImageDatabase")]
public class ImageDatabase : ScriptableObject
{
    private static ImageDatabase _instance = null;
    public static ImageDatabase Instance
    {
        get
        {
            if (_instance == null)
                _instance = (ImageDatabase)Resources.Load(typeof(ImageDatabase).Name);

            return _instance;
        }
    }

    [Serializable]
    public class Layer
    {
        [SerializeField] private List<Sprite> texturesList = new List<Sprite>();

        public Sprite GetTexture()
        {
            return texturesList[Random.Range(0, texturesList.Count)];
        }
    }

    [Serializable]
    public class RaceLayers
    {
        [SerializeField] private Race _race = Race.Man;
        public Race Race { get { return _race; } }

        [SerializeField] private Layer[] _layers;
        
        public Sprite[] GetInage()
        {
            List<Sprite> image = new List<Sprite>();

            for (int i = 0; i < _layers.Length; i++)
                image.Add(_layers[i].GetTexture());

            return image.ToArray();
        }
    }

    [SerializeField] private RaceLayers[] _layers;
    private Dictionary<Race, RaceLayers> _layersDictionary = null;

    public Sprite[] GetImage(Race race)
    {
        if (_layersDictionary == null)
        {
            _layersDictionary = new Dictionary<Race, RaceLayers>();
            for (int i = 0; i < _layers.Length; i++)
                _layersDictionary.Add(_layers[i].Race, _layers[i]);
        }

        RaceLayers layers = null;
        if(_layersDictionary.TryGetValue(race, out layers))
            return layers.GetInage();

        return null;
    }
}
