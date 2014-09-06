using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xnaHelper.Helpers;
using DodongosQuest.Terrain;
using DodongosQuest.Creatures;

namespace DodongosQuest.Screens.Gameplay
{
    public class MiniMap
    {
        private Color _playerColor;
        private Color _fogOfWarColor;
        private Color _monsterColor;
        private const int MAP_TILE_SIZE = 2;
        private Vector2 _position;
        private Texture2D _miniMapTileImage;
        private World _world;
        int _width = 100;
        int _height = 100;

        public MiniMap(Vector2 position, World world)
        {
            _position = position;
            _playerColor = Color.Gold;
            _monsterColor = Color.Red;
            _miniMapTileImage = ContentHelper.Content.Load<Texture2D>(@"Terrain\MiniMapTile");
            _fogOfWarColor = new Color(new Vector4(0, 0, 0, 0.25f));
            _world = world;
        }

        public void Draw(GameTime GameTime)
        {
            for(int y = 0; y < _height; y++)
                for (int x = 0; x < _width; x++)
                {
                    Vector2 drawPosition = ConvertWorldIndexToMiniMapScreenPosition(_world.Tiles[x, y].WorldIndex);
                    if (_world.Player.WorldIndex.X == x && _world.Player.WorldIndex.Y == y)
                        GraphicsHelper.spriteBatch.Draw(_miniMapTileImage, drawPosition, null, _playerColor, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
                    else if(_world.Tiles[x, y].IsVisibleToPlayer && _world.GetCreatureAtIndex(new Vector2(x, y)) != null)
                        GraphicsHelper.spriteBatch.Draw(_miniMapTileImage, drawPosition, null, _monsterColor, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
                    else
                    {
                        _world.Tiles[x, y].DrawMiniMap(GameTime, drawPosition);
                        if (_world.Tiles[x, y].IsVisibleToPlayer == false)
                            GraphicsHelper.spriteBatch.Draw(_miniMapTileImage, drawPosition, null, _fogOfWarColor, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, -0f);
                    }
                }
        }

        public Vector2 ConvertWorldIndexToMiniMapScreenPosition(Vector2 worldIndex)
        {
            return new Vector2(worldIndex.X * MAP_TILE_SIZE + 1 + _position.X, worldIndex.Y * MAP_TILE_SIZE + 1 + _position.Y);
        }
    }
}
