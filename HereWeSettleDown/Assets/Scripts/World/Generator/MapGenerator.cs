﻿// Test

using System.Collections.Generic;
using UnityEngine;
using World.Map;
using Nodes.HeightMapGeneration;
using Debuger;
using csDelaunay;

namespace World.Generator
{
    public class MapGenerator : MonoBehaviour
    {
        public int seed = 0;
        public int cellsCount = 124;

        public int worldWidth = 124;
        public int worldHeight = 124;

        public int chunkWidth = 124 / 2;
        public int chunkHeight = 124 / 2;

        public ChunkObject chunkObject;

        public static Voronoi voronoi;
        public static Region[] regions;

        public HeightMapGenerationGraph heightMapGraph;
        private System.Random prng;

        private void Awake()
        {
            prng = new System.Random(seed);
        }

        public void GenerateMap()
        {
            WorldMesh.CreateWorldMesh(worldWidth, worldHeight, chunkWidth, chunkHeight);
            WorldChunkMap.CreateMap(worldWidth, worldHeight, chunkWidth, chunkHeight, chunkObject.transform.localScale);
            WorldChunkMap.CreateChunks(chunkObject, transform, true);

            SetVoronoi();
            CreateRegions();
            SetCoastline();
        }

        private void SetVoronoi()
        {
            List<Vector2> points = new List<Vector2>();
            for (int i = 0; i < cellsCount; i++)
            {
                points.Add(new Vector2(prng.Next(0, worldWidth), prng.Next(0, worldHeight)));
            }
            voronoi = new Voronoi(points, new Rectf(0, 0, worldWidth, worldHeight), 2, prng);
        }

        private void CreateRegions()
        {
            List<Region> regions = new List<Region>();
            foreach (Vector2 site in voronoi.SiteCoords())
            {
                Vector2Int[] edges = ToVector2Int(voronoi.Region(site).ToArray());
                regions.Add(new Region(edges, site));
            }
            MapGenerator.regions = regions.ToArray();
        }

        private Vector2Int[] ToVector2Int(Vector2[] ps)
        {
            Vector2Int[] points = new Vector2Int[ps.Length];
            for (int i = 0; i < ps.Length; i++)
                points[i] = new Vector2Int((int)ps[i].x, (int)ps[i].y);
            return points;
        }

        private void SetCoastline()
        {
            heightMapGraph.mapWidth = worldWidth;
            heightMapGraph.mapHeight = worldHeight;
            heightMapGraph.setPrng = prng;

            float[,] heightMap = heightMapGraph.requester.GetHeightMap().map;
            foreach (Region region in regions)
            {
                Vector2Int site = new Vector2Int((int)region.site.x, (int)region.site.y);
                if (heightMap[site.x, site.y] < 1)
                {
                    region.Fill(new Color(0.27f, 0.64f, 0.75f));
                    region.isWater = true;
                }
            }
        }
    }
}
