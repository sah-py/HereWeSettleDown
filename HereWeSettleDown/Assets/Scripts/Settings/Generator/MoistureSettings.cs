﻿using UnityEngine;

namespace Settings.Generator
{
    [CreateAssetMenu(menuName = "Settings/Generator/MoistureSettings")]
    public class MoistureSettings : SettingsObject
    {
        public bool SetMoistureFromOcean = false;
        public float OceanMoistureMultiplier = 1f;
        public bool SetMoistureFromLakes = true;
        public float LakesMoistureMultiplier = 0.9f;
        public bool SetMoistureFromRivers = true;
        public float RiversMoistureMultiplier = 0.9f;
    }
}
