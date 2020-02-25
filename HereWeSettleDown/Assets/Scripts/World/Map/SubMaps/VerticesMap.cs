﻿using UnityEngine;

namespace World.Map
{
    public class VerticesMap
    {
        public Vector3[,] map;
        public Vector3 this[int x, int y]
        {
            get
            {
                if (IsValid(x, y))
                    return map[x, y];
                return Vector3.zero;
            }
            set
            {
                if (IsValid(x, y))
                {
                    map[x, y] = value;
                    WorldMeshMap.SetEditedPosition(x, y);
                }
            }
        }

        public int width
        {
            get
            {
                return map.GetLength(0);
            }
        }
        public int height
        {
            get
            {
                return map.GetLength(1);
            }
        }

        public VerticesMap(Vector3[,] map)
        {
            this.map = map;
        }

        public bool IsValid(int x, int y)
        {
            return (x >= 0 && x < width && y >= 0 && y < height);
        }
    }
}
