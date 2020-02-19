﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Generator
{
    public class SubGenerator : MonoBehaviour
    {
        public static Dictionary<string, object> values = new Dictionary<string, object>();
        public System.Random ownPrng;

        public virtual void OnRegistrate() { }
        public virtual void OnGenerate() { }

        protected void GenerationCompleted()
        {
            MasterGenerator.SetGeneratorState(this, true);
        }

        public static T GetValue<T>(string name)
        {
            if (values.ContainsKey(name))
            {
                return (T)values[name];
            }
            else
            {
                Debug.LogError("Something is trying to get a nonexistent variable " + name);
            }
            return default;
        }

        public static bool TryGetValue<T>(string name, out T value)
        {
            if (values.ContainsKey(name))
            {
                value = (T)values[name];
                return true;
            }

            value = default;
            return false;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class WorldGenerator : Attribute
    {
        public GeneratorRegData data;

        public WorldGenerator(int priority = 0, bool useOwnThread = false, bool useOwnPRNG = false, params string[] requireValues)
        {
            data = new GeneratorRegData()
            {
                priority = priority,
                useThread = useOwnThread,
                useOwnPRNG = useOwnPRNG,
                requireValues = requireValues
            };
        }
    }

    public struct GeneratorRegData
    {
        public int priority;
        public bool useThread;
        public bool useOwnPRNG;
        public string[] requireValues;
    }
}
