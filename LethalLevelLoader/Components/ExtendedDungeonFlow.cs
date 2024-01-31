﻿using DunGen.Graph;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace LethalLevelLoader
{
    [CreateAssetMenu(menuName = "LethalLevelLoader/ExtendedDungeonFlow")]
    public class ExtendedDungeonFlow : ScriptableObject
    {
        [Header("Extended DungeonFlow Settings")]
        [Space(5)]
        public string contentSourceName = "Lethal Company";
        public string dungeonDisplayName;
        [Space(5)]
        public DungeonFlow dungeonFlow;
        public AudioClip dungeonFirstTimeAudio;

        [HideInInspector] public ContentType dungeonType;
        [HideInInspector] public int dungeonID;
        [HideInInspector] public int dungeonDefaultRarity;

        [Space(10)]
        [Header("Dynamic DungeonFlow Injections Settings")]

        [Space(5)] public List<StringWithRarity> dynamicLevelTagsList = new List<StringWithRarity>();
        [Space(5)] public List<Vector2WithRarity> dynamicRoutePricesList = new List<Vector2WithRarity>();
        [Space(5)] public List<StringWithRarity> dynamicCurrentWeatherList = new List<StringWithRarity>();
        [Space(5)] public List<StringWithRarity> manualPlanetNameReferenceList = new List<StringWithRarity>();
        [Space(5)] public List<StringWithRarity> manualContentSourceNameReferenceList = new List<StringWithRarity>();

        [Space(10)]
        [Header("Dynamic Dungeon Size Multiplier Lerp Settings")]
        [Space(5)]
        public float dungeonSizeMin = 1;
        public float dungeonSizeMax = 1;
        [Range(0, 1)] public float dungeonSizeLerpPercentage = 1;


        [Space(10)]
        [Header("Dynamic DungeonFlow Modification Settings")]

        [Space(5)] public List<GlobalPropCountOverride> globalPropCountOverridesList = new List<GlobalPropCountOverride>();

        [Space(10)]
        [Header("Misc. Settings")]
        [Space(5)] public bool generateAutomaticConfigurationOptions = true;

        [Space(10)]
        [Header("Experimental Settings (Currently Unused As Of LethalLevelLoader 1.1.0")]
        [Space(5)] public GameObject mainEntrancePropPrefab;
        [Space(5)] public GameObject fireExitPropPrefab;
        [Space(5)] public Animator mainEntrancePropAnimator;
        [Space(5)] public Animator fireExitPropAnimator;

        [HideInInspector]
        public UnityEventDungeonGenerator onBeforeExtendedDungeonGenerate;

        [HideInInspector]
        public UnityEventSpawnMapObjects onSpawnMapHazardsSpawn;

        internal void Initialize(ContentType newDungeonType)
        {
            dungeonType = newDungeonType;

            dungeonID = PatchedContent.ExtendedDungeonFlows.Count;

            if (dungeonDisplayName == null || dungeonDisplayName == string.Empty)
                dungeonDisplayName = dungeonFlow.name;

            if (newDungeonType == ContentType.Custom)
                this.name = dungeonDisplayName + "ExtendedDungeonFlow";
        }
    }

    [Serializable]
    public class StringWithRarity
    {
        [SerializeField]
        private string _name;

        [SerializeField]
        [Range(0, 300)]
        private int _rarity;

        [HideInInspector] public string Name { get { return (_name); } set { _name = value; } }
        [HideInInspector] public int Rarity { get { return (_rarity); } set { _rarity = value; } }
        [HideInInspector] public StringWithRarity(string newName, int newRarity) { _name = newName; _rarity = newRarity; }
    }

    [Serializable]
    public class Vector2WithRarity
    {
        [SerializeField] private Vector2 _minMax;
        [SerializeField] private int _rarity;

        [HideInInspector] public float Min { get { return (_minMax.x); } set { _minMax.x = value; } }
        [HideInInspector] public float Max { get { return (_minMax.y); } set { _minMax.y = value; } }
        [HideInInspector] public int Rarity { get { return (_rarity); } set { _rarity = value; } }

        public Vector2WithRarity(Vector2 vector2, int newRarity)
        {
            _minMax.x = vector2.x;
            _minMax.y = vector2.y;
            _rarity = newRarity;
        }

        public Vector2WithRarity(float newMin, float newMax, int newRarity)
        {
            _minMax.x = newMin;
            _minMax.y = newMax;
            _rarity = newRarity;
        }
    }

    [Serializable]
    public class GlobalPropCountOverride
    {
        public int globalPropID;
        [Range(0,1)] public float globalPropCountScaleRate = 0;
    }

}