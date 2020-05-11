using JetBrains.Annotations;

namespace AssetStudio.Extended.CompositeModels {
    public sealed class SubMesh {

        internal SubMesh(uint firstIndex, uint indexCount, PrimitiveType topology, uint triangleCount, uint firstVertex, uint vertexCount, AABB boundingBox) {
            FirstIndex = firstIndex;
            IndexCount = indexCount;
            Topology = topology;
            TriangleCount = triangleCount;
            FirstVertex = firstVertex;
            VertexCount = vertexCount;
            BoundingBox = boundingBox;
        }

        internal SubMesh(uint firstIndex, [NotNull] AssetStudio.SubMesh mesh) {
            FirstIndex = firstIndex;
            IndexCount = mesh.indexCount;
            Topology = (PrimitiveType)mesh.topology;
            TriangleCount = mesh.triangleCount;
            FirstVertex = mesh.firstVertex;
            VertexCount = mesh.vertexCount;
            BoundingBox = new AABB(mesh.localAABB);
        }

        public uint FirstIndex { get; }

        public uint IndexCount { get; }

        public PrimitiveType Topology { get; }

        public uint TriangleCount { get; }

        public uint FirstVertex { get; }

        public uint VertexCount { get; }

        public AABB BoundingBox { get; }

    }
}