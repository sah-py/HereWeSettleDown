﻿using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Helper.Threading
{
    public class MainThreadInvoker : MonoBehaviour
    {
        public static Thread mainThread { get; private set; }

        private static readonly object locker = new object();
        private static readonly List<Action> registratedActions = new List<Action>();

        private void Awake()
        {
            mainThread = Thread.CurrentThread;
        }

        public static void InvokeAction(Action action)
        {
            lock (locker)
            {
                registratedActions.Add(action);
            }
        }

        private void Update()
        {
            lock (locker)
            {
                if (registratedActions.Count > 0)
                {
                    foreach (Action action in registratedActions)
                    {
                        action();
                    }
                    registratedActions.Clear();
                }
            }
        }

        public static bool CheckForMainThread()
        {
            return Thread.CurrentThread == mainThread;
        }
    }
}
