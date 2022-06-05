using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Aiv.Fast2D;
using OpenTK;

namespace BillyPassepartout
{
    class TmxMap : IDrawable
    {
        // Tileset
        private string tmxFilePath;
        private TmxTileset tileset;
        // MultiLayers
        private TmxTileLayer[] tileLayers;

        public Map PathfindingMap { get; private set; }
        public TmxObjectLayer ObjectsLayer { get; private set; }
        public DrawLayer Layer { get; }

        public TmxMap(string filePath)
        {
            // Map Drawing Settings
            Layer = DrawLayer.BACKGROUND;
            DrawManager.AddItem(this);

            // CREATE AND LOAD XML DOCUMENT FROM TMX MAP FILE
            tmxFilePath = filePath;

            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load(tmxFilePath);
            }
            catch (XmlException e)
            {
                Console.WriteLine("XML Exception: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Generic Exception: " + e.Message);
            }

            // PROCEED TO XML DOCUMENT NODES PARSING
            // Map Node and Attributes
            XmlNode mapNode = xmlDoc.SelectSingleNode("map");
            int mapCols = GetIntAttribute(mapNode, "width");
            int mapRows = GetIntAttribute(mapNode, "height");
            int mapTileW = GetIntAttribute(mapNode, "tilewidth");
            int mapTileH = GetIntAttribute(mapNode, "tileheight");

            // Tileset Node and Attributes
            XmlNode tilesetNode = mapNode.SelectSingleNode("tileset");
            int tilesetTileW = GetIntAttribute(tilesetNode, "tilewidth");
            int tilesetTileH = GetIntAttribute(tilesetNode, "tileheight");
            int spacing = GetIntAttribute(tilesetNode, "spacing");
            int margin = GetIntAttribute(tilesetNode, "margin");
            int tileCount = GetIntAttribute(tilesetNode, "tilecount");
            int tilesetCols = GetIntAttribute(tilesetNode, "columns");
            int tilesetRows = tileCount / tilesetCols;
            // Create Tileset from collected data
            tileset = new TmxTileset("tileset", tilesetCols, tilesetRows, tilesetTileW, tilesetTileH, spacing, margin);

            // ObjectLayer Node and Attributes
            XmlNode objectLayerNode = mapNode.SelectSingleNode("objectgroup");
            // Create objectLayer from collected data
            ObjectsLayer = new TmxObjectLayer(objectLayerNode, tileset, mapCols, mapRows);
            
            //Layers nodes
            XmlNodeList layersNodes = mapNode.SelectNodes("layer");

            tileLayers = new TmxTileLayer[layersNodes.Count];

            for (int i = 0; i < layersNodes.Count; i++)
            {
                tileLayers[i] = new TmxTileLayer(layersNodes[i], tileset, mapCols, mapRows, mapTileW, mapTileH);
            }

            PathfindingMap = new Map(mapCols, mapRows, ObjectsLayer);
        }

        public static int GetIntAttribute(XmlNode node, string attrName)
        {
            return int.Parse(GetStringAttribute(node, attrName));
        }

        public static bool GetBoolAttribute(XmlNode node, string attrName)
        {
            return bool.Parse(GetStringAttribute(node, attrName));
        }

        public static string GetStringAttribute(XmlNode node, string attrName)
        {
            return node.Attributes.GetNamedItem(attrName).Value;
        }

        public void Draw()
        {
            for (int i = 0; i < tileLayers.Length; i++)
            {
                tileLayers[i].Draw();
            }
        }
    }
}
