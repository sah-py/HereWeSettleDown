﻿using XNode;

namespace World.Generator.Nodes.HeightMap.Maps
{
    public class FlatMap : Node
    {
        [Output] public HeightMap outMap;

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == "outMap")
            {
                HeightMapGenerationGraph ghp = (HeightMapGenerationGraph)graph;
                return new HeightMap(new float[ghp.mapWidth, ghp.mapHeight]);
            }
            return null;
        }
    }
}
