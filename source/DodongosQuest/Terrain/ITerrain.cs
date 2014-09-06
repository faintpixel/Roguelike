using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DodongosQuest.Creatures;

namespace DodongosQuest.Terrain
{
    public interface ITerrain
    {
        string Name { get; set; }
        bool ObstructsLineOfSight { get; set; }
        bool IsVisibleToPlayer { get; set; }
        bool IsExploredByPlayer { get; set; }
        Vector2 WorldIndex { get; }

        bool CanCreatureEnter(ICreature creature);
        void AffectCreature(ICreature creature);
        void Draw(GameTime gameTime);
        void DrawMiniMap(GameTime gameTime, Vector2 position);
        void Update(GameTime gameTime);
    }
}
