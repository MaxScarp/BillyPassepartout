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
        private bool open;
        private bool close;
        private bool solid;
        private bool pressed;
        private bool active;

        public int[] Cells { get; private set; }
        public List<TmxObject> Objects { get; private set; }

        public TmxObjectLayer(XmlNode objectLayerNode, TmxTileset tileset, int mapWidth, int mapHeight)
        {
            Objects = new List<TmxObject>();

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

                XmlNodeList propertyNodes = objectsNodes[i].SelectNodes("properties/property");
                foreach (XmlNode node in propertyNodes)
                {
                    string attributeName = TmxMap.GetStringAttribute(node, "name");

                    switch (attributeName)
                    {
                        case "Solid":
                            solid = TmxMap.GetBoolAttribute(node, "value");
                            break;
                        case "Open":
                            open = TmxMap.GetBoolAttribute(node, "value");
                            break;
                        case "Close":
                            close = TmxMap.GetBoolAttribute(node, "value");
                            break;
                        case "Pressed":
                            pressed = TmxMap.GetBoolAttribute(node, "value");
                            break;
                        case "Active":
                            active = TmxMap.GetBoolAttribute(node, "value");
                            break;
                    }   
                }

                objects[i] = new TmxObject(objName, objXOff, objYOff, (int)Game.PixelsToUnits(objW), (int)Game.PixelsToUnits(objH), solid, open, close, pressed, active);

                objects[i].Position = new Vector2((int)Game.PixelsToUnits(objX), (int)Game.PixelsToUnits(objY) - 1);

                int x = objX / objW;
                int y = objY / objH - 1;

                Cells[y * mapWidth + x] = objects[i].Weight;

                Objects.Add(objects[i]);
            }
        }
    }
}
