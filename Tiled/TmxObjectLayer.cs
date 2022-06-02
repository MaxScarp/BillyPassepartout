using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using OpenTK;

namespace BillyPassepartout
{
    class TmxObjectLayer
    {
        public int[] Cells { get; private set; }

        public TmxObjectLayer(XmlNode objectLayerNode, TmxTileset tileset, int mapWidth, int mapHeight)
        {
            XmlNodeList objectsNodes = objectLayerNode.SelectNodes("object");

            TmxObject[] objects = new TmxObject[objectsNodes.Count];

            Cells = new int[mapWidth * mapHeight];
            for (int i = 0; i < Cells.Length; i++)
            {
                Cells[i] = 0;
            }

            for (int i = 0; i < objectsNodes.Count; i++)
            {
                string objName = TmxMap.GetStringAttribute(objectsNodes[i], "name");
                int objId = TmxMap.GetIntAttribute(objectsNodes[i], "gid");
                int objX = TmxMap.GetIntAttribute(objectsNodes[i], "x");
                int objY = TmxMap.GetIntAttribute(objectsNodes[i], "y");
                int objW = TmxMap.GetIntAttribute(objectsNodes[i], "width");
                int objH = TmxMap.GetIntAttribute(objectsNodes[i], "height");

                int objXOff = tileset.GetAtIndex(objId).X;
                int objYOff = tileset.GetAtIndex(objId).Y;

                XmlNode propertyNode = objectsNodes[i].SelectSingleNode("properties/property");
                bool solid = TmxMap.GetBoolAttribute(propertyNode, "value");

                objects[i] = new TmxObject(objName, objXOff, objYOff, (int)Game.PixelsToUnits(objW), (int)Game.PixelsToUnits(objH), solid);
                objects[i].Position = new Vector2(Game.PixelsToUnits(objX), Game.PixelsToUnits(objY)-1);

                int x = objX / objW;
                int y = objY / objH - 1;

                Cells[y * mapWidth + x] = objects[i].Weight;
            }
        }
    }
}
