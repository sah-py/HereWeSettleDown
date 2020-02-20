﻿using UnityEngine;

namespace World.Chunks
{
    public class Chunk
    {
        public ChunkObject chunkObject;
        public ChunkMeshData meshData;

        public bool visible { get; private set; }

        MeshFilter meshFilter;

        public Chunk(ChunkObject terrain, Transform parent, bool visible, ChunkMeshData meshData)
        {
            this.meshData = meshData;

            CreateChunkObject(terrain, parent);
            DrawMesh();
            UpdateMeshCollider();
            SetVisible(visible);
        }

        void CreateChunkObject(ChunkObject terrain, Transform parent)
        {
            chunkObject = Object.Instantiate(terrain, parent);
            chunkObject.name = "Chunk" + meshData.chunkX + " " + meshData.chunkY;
            meshFilter = chunkObject.GetComponent<MeshFilter>();
        }

        void UpdateMeshCollider()
        {
            MeshCollider collider = chunkObject.GetComponent<MeshCollider>();
            if (!collider)
                collider = chunkObject.gameObject.AddComponent<MeshCollider>();

            if (meshFilter)
            {
                collider.sharedMesh = meshFilter.sharedMesh;
            }
        }

        public void DrawMesh()
        {
            if (meshFilter)
                meshFilter.sharedMesh = meshData.CreateMesh();
        }

        public void SetVisible(bool state)
        {
            if (chunkObject)
                chunkObject.gameObject.SetActive(state);
            visible = state;
        }
    }
}
