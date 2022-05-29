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
    class TmxTileLayer
    {
        private Texture tilesetImage;
        private Texture layerTexture;
        private Sprite layerSprite;
        public string[] IDs { get; }
        public DrawLayer Layer { get; protected set; }

        public TmxTileLayer(XmlNode layerNode, TmxTileset tileset, int cols, int rows, int tileW, int tileH)
        {
            XmlNode dataNode = layerNode.SelectSingleNode("data");
            string csvData = dataNode.InnerText;
            csvData = csvData.Replace("\r\n", "").Replace("\n", "").Replace(" ", "");

            string[] Ids = csvData.Split(',');
            IDs = Ids;

            tilesetImage = GfxManager.GetTexture(tileset.TextureName);

            // Create a single texture for the whole map
            layerTexture = new Texture(1024, 768);
            byte[] mapBitmap = new byte[layerTexture.Width * layerTexture.Height * 4];
            Texture tilesetTexture = GfxManager.GetTexture("tileset");
            byte[] tilesetBitmap = tilesetTexture.Bitmap;

            int bytesPerPixel = 4;
            int tilesetBitmapRowLength = 256 * bytesPerPixel;
            int mapBitmapRowLength = layerTexture.Width * bytesPerPixel;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    int tileId = int.Parse(IDs[r * cols + c]);

                    if (tileId > 0)
                    {
                        // Get correct tilesetBitmap's section starting index
                        // Get tilesetBitmapIndex offset X in pixels and convert it in bytes
                        int tilesetXOff = tileset.GetAtIndex(tileId).X * bytesPerPixel;
                        // Get tilesetBitmapIndex offset Y in pixels and convert it in bytes
                        int tilesetYOff = tileset.GetAtIndex(tileId).Y * tilesetBitmapRowLength;
                        // Calculate tilesetBitmap starting index
                        int tilesetBitmapIndexInitial = tilesetYOff + tilesetXOff;

                        // Get correct mapBitmap's section starting index
                        int mapXOff = c * tileW * bytesPerPixel;
                        int mapYOff = r * tileH * mapBitmapRowLength;

                        int mapBitmapIndexInitial = mapXOff + mapYOff;

                        // This loop is to copy each single tile from tilesetBitmap to mapBitmap
                        // Loop through each row copying a tile length every time (32 pixels = 32 * 4 bytes)
                        for (int i = 0; i < tileH; i++)
                        {
                            // How tilesetBitmapIndexInitial increments
                            int tilesetBitmapIndexUpdate = i * tilesetBitmapRowLength;
                            // How mapBitmapIndexInitial increments
                            int mapBitmapIndexUpdate = i * mapBitmapRowLength;

                            // Copy tilesetBitmap's tile section to mapBitmap in correct position
                            Array.Copy(tilesetBitmap,                                           // source array
                                        tilesetBitmapIndexInitial + tilesetBitmapIndexUpdate,    // source index
                                        mapBitmap,                                               // dest array
                                        mapBitmapIndexInitial + mapBitmapIndexUpdate,                // dest index
                                        tileW * bytesPerPixel);                                  // length
                        }
                    }
                }
            }

            layerTexture.Update(mapBitmap);

            layerSprite = new Sprite(Game.PixelsToUnits(layerTexture.Width), Game.PixelsToUnits(layerTexture.Height));
        }

        public void Draw()
        {
            layerSprite.DrawTexture(layerTexture);
        }
    }
}
