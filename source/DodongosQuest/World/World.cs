using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DodongosQuest.Creatures;
using DodongosQuest.Creatures.Player;
using DodongosQuest.Terrain;
using DodongosQuest.Items;
using DodongosQuest.Containers;
using DodongosQuest.Announcements;
using DodongosQuest.Quests;
using DodongosQuest.Weapons;
using DodongosQuest.Creatures.Remains;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using xnaHelper.Helpers;
using DodongosQuest.ParticleSystem;
using DodongosQuest.Achievements;
using DodongosQuest.Special_Effects;

namespace DodongosQuest
{
    public class World
    {
        public PlayerCharacter Player;
        private List<ICreature> _creatures;
        private List<IContainer> _containers;
        private List<Door> _doors;
        private List<IQuest> _quests;         
        private List<IRemains> _remains;
        private List<ISpecialEffect> _specialEffects;
        public ITerrain[,] Tiles { get; set; }
        private List<Fire> _fire;

        private const int WORLD_WIDTH = 100;
        private const int WORLD_HEIGHT = 100;
        public const int TILE_SIZE = 25;

        public event AchievementEvent AchievementUnlocked;
        public delegate void AchievementEvent(AchievementNotifier achievement);

        public World()
        {
            _creatures = new List<ICreature>();
            _containers = new List<IContainer>();
            _doors = new List<Door>();
            _quests = new List<IQuest>();
            _fire = new List<Fire>();
            _remains = new List<IRemains>();
            _specialEffects = new List<ISpecialEffect>();

            SetMap(@"Content\TestLevel2.png");                      

            Player = new PlayerCharacter(new Vector2(47, 25), this);
            CenterCameraOnPlayer();
            DiscoverTerrainAroundPlayer();
          
            TEST_addStuffToWorld();
        }

        private void TEST_addStuffToWorld()
        {           
            ICreature bat = CreatureFactory.CreateCreature(DodongosQuest.Creatures.Creatures.GiantBat, new Vector2(9, 94), this);
            bat.Death += new CreatureEvent(HandleCreatureDeath);
            _creatures.Add(bat);

            bat = CreatureFactory.CreateCreature(DodongosQuest.Creatures.Creatures.GiantBat, new Vector2(32, 96), this);
            bat.Death += new CreatureEvent(HandleCreatureDeath);
            _creatures.Add(bat);

            bat = CreatureFactory.CreateCreature(DodongosQuest.Creatures.Creatures.GiantBat, new Vector2(40, 87), this);
            bat.Death += new CreatureEvent(HandleCreatureDeath);
            _creatures.Add(bat);

            bat = CreatureFactory.CreateCreature(DodongosQuest.Creatures.Creatures.GiantBat, new Vector2(66, 86), this);
            bat.Death += new CreatureEvent(HandleCreatureDeath);
            _creatures.Add(bat);

            bat = CreatureFactory.CreateCreature(DodongosQuest.Creatures.Creatures.GiantBat, new Vector2(46, 75), this);
            bat.Death += new CreatureEvent(HandleCreatureDeath);
            _creatures.Add(bat);

            bat = CreatureFactory.CreateCreature(DodongosQuest.Creatures.Creatures.GiantBat, new Vector2(76, 32), this);
            bat.Death += new CreatureEvent(HandleCreatureDeath);
            _creatures.Add(bat);

            ICreature fireSpirit = CreatureFactory.CreateCreature(DodongosQuest.Creatures.Creatures.FireSpirit, new Vector2(82, 23), this);
            fireSpirit.Death += new CreatureEvent(HandleCreatureDeath);
            _creatures.Add(fireSpirit);

            ICreature manEatingPlant = CreatureFactory.CreateCreature(Creatures.Creatures.ManEatingPlant, new Vector2(53, 33), this);
            manEatingPlant.Death += new CreatureEvent(HandleCreatureDeath);
            manEatingPlant.Death += new CreatureEvent(manEatingPlant_Death);
            _creatures.Add(manEatingPlant);

            _quests.Add(QuestFactory.MakeQuest(this, manEatingPlant));
            _quests[0].AnnounceQuest();
            _quests[0].DoneWithQuest += new QuestEvent(World_DoneWithQuest);

            ICreature golem = CreatureFactory.CreateCreature(Creatures.Creatures.Golem, new Vector2(46, 24), this);
            golem.Death += new CreatureEvent(HandleCreatureDeath);
            _creatures.Add(golem);

            ICreature skeleton = CreatureFactory.CreateCreature(DodongosQuest.Creatures.Creatures.SkeletonWarrior, new Vector2(65, 33), this);
            skeleton.Death += new CreatureEvent(HandleCreatureDeath);
            _creatures.Add(skeleton);

            skeleton = CreatureFactory.CreateCreature(DodongosQuest.Creatures.Creatures.SkeletonWarrior, new Vector2(56, 13), this);
            skeleton.Death += new CreatureEvent(HandleCreatureDeath);
            _creatures.Add(skeleton);

            skeleton = CreatureFactory.CreateCreature(DodongosQuest.Creatures.Creatures.SkeletonWarrior, new Vector2(51, 12), this);
            skeleton.Death += new CreatureEvent(HandleCreatureDeath);
            _creatures.Add(skeleton);

            skeleton = CreatureFactory.CreateCreature(DodongosQuest.Creatures.Creatures.SkeletonWarrior, new Vector2(63, 5), this);
            skeleton.Death += new CreatureEvent(HandleCreatureDeath);
            _creatures.Add(skeleton);

            skeleton = CreatureFactory.CreateCreature(DodongosQuest.Creatures.Creatures.SkeletonWarrior, new Vector2(50, 3), this);
            skeleton.Death += new CreatureEvent(HandleCreatureDeath);
            _creatures.Add(skeleton);

            skeleton = CreatureFactory.CreateCreature(DodongosQuest.Creatures.Creatures.SkeletonWarrior, new Vector2(36, 20), this);
            skeleton.Death += new CreatureEvent(HandleCreatureDeath);
            _creatures.Add(skeleton);

            skeleton = CreatureFactory.CreateCreature(DodongosQuest.Creatures.Creatures.SkeletonWarrior, new Vector2(34, 13), this);
            skeleton.Death += new CreatureEvent(HandleCreatureDeath);
            _creatures.Add(skeleton);

            skeleton = CreatureFactory.CreateCreature(DodongosQuest.Creatures.Creatures.SkeletonWarrior, new Vector2(41, 70), this);
            skeleton.Death += new CreatureEvent(HandleCreatureDeath);
            _creatures.Add(skeleton);

            skeleton = CreatureFactory.CreateCreature(DodongosQuest.Creatures.Creatures.SkeletonWarrior, new Vector2(44, 63), this);
            skeleton.Death += new CreatureEvent(HandleCreatureDeath);
            _creatures.Add(skeleton);

            skeleton = CreatureFactory.CreateCreature(DodongosQuest.Creatures.Creatures.SkeletonWarrior, new Vector2(57, 58), this);
            skeleton.Death += new CreatureEvent(HandleCreatureDeath);
            _creatures.Add(skeleton);

            ICreature octopus = CreatureFactory.CreateCreature(DodongosQuest.Creatures.Creatures.Octopus, new Vector2(11, 87), this);
            octopus.Death += new CreatureEvent(HandleCreatureDeath);
            _creatures.Add(octopus);

            octopus = CreatureFactory.CreateCreature(DodongosQuest.Creatures.Creatures.Octopus, new Vector2(14, 94), this);
            octopus.Death += new CreatureEvent(HandleCreatureDeath);
            _creatures.Add(octopus);

            octopus = CreatureFactory.CreateCreature(DodongosQuest.Creatures.Creatures.Octopus, new Vector2(65, 22), this);
            octopus.Death += new CreatureEvent(HandleCreatureDeath);
            _creatures.Add(octopus);

            ICreature walkingDragon = CreatureFactory.CreateCreature(DodongosQuest.Creatures.Creatures.WalkingDragon, new Vector2(15, 92), this);
            walkingDragon.Death += new CreatureEvent(HandleCreatureDeath);
            walkingDragon.FlamingAttack += new CreatureFlameEvent(HandleFlameAttack);
            _creatures.Add(walkingDragon);

            IItem doll = ItemFactory.CreateItem(DodongosQuest.Items.ItemTypes.CursedDoll, new Vector2(68, 30), this);

            TreasureChest chest = new TreasureChest(new List<IItem>(), true, new Vector2(68, 30), this);
            chest.Contents.Add(ItemFactory.CreateItem(DodongosQuest.Items.ItemTypes.PotionOfBlindness, new Vector2(68, 30), this));
            chest.Contents.Add(doll);
            _containers.Add(chest);

            _quests.Add(QuestFactory.MakeItemFindQuest(this, doll));
            _quests[1].AnnounceQuest();
            _quests[1].DoneWithQuest += new QuestEvent(World_DoneWithQuest);

            chest = new TreasureChest(new List<IItem>(), false, new Vector2(53, 30), this);
            chest.Contents.Add(ItemFactory.CreateItem(DodongosQuest.Items.ItemTypes.PotionOfMinorHealth, new Vector2(53, 30), this));
            chest.Contents.Add(ItemFactory.CreateItem(DodongosQuest.Items.ItemTypes.Diamond, new Vector2(53, 30), this));
            chest.Contents.Add(ItemFactory.CreateItem(DodongosQuest.Items.ItemTypes.OldKey, new Vector2(53, 30), this));
            chest.Contents.Add(ItemFactory.CreateItem(DodongosQuest.Items.ItemTypes.PotionOfMinorEnergy, new Vector2(53, 30), this));
            chest.Contents.Add((IItem)WeaponFactory.CreateGiantAxe(new Vector2(53, 30), this));
            _containers.Add(chest);

            chest = new TreasureChest(new List<IItem>(), false, new Vector2(59, 11), this);
            chest.Contents.Add(ItemFactory.CreateItem(DodongosQuest.Items.ItemTypes.Diamond, new Vector2(59, 11), this));
            _containers.Add(chest);

            chest = new TreasureChest(new List<IItem>(), false, new Vector2(53, 11), this);
            chest.Contents.Add(ItemFactory.CreateItem(DodongosQuest.Items.ItemTypes.PotionOfMinorHealth, new Vector2(53, 11), this));
            _containers.Add(chest);

            chest = new TreasureChest(new List<IItem>(), false, new Vector2(17, 17), this);
            chest.Contents.Add(ItemFactory.CreateItem(DodongosQuest.Items.ItemTypes.PotionOfMinorHealth, new Vector2(17, 17), this));
            _containers.Add(chest);

            chest = new TreasureChest(new List<IItem>(), false, new Vector2(66, 60), this);
            chest.Contents.Add(ItemFactory.CreateItem(DodongosQuest.Items.ItemTypes.Diamond, new Vector2(66, 60), this));
            _containers.Add(chest);

            chest = new TreasureChest(new List<IItem>(), false, new Vector2(46, 64), this);
            chest.Contents.Add(ItemFactory.CreateItem(DodongosQuest.Items.ItemTypes.Diamond, new Vector2(46, 64), this));
            _containers.Add(chest);

            chest = new TreasureChest(new List<IItem>(), false, new Vector2(47, 64), this);
            chest.Contents.Add(ItemFactory.CreateItem(DodongosQuest.Items.ItemTypes.PotionOfMinorHealth, new Vector2(47, 64), this));
            _containers.Add(chest);

            chest = new TreasureChest(new List<IItem>(), false, new Vector2(70, 83), this);
            chest.Contents.Add(ItemFactory.CreateItem(DodongosQuest.Items.ItemTypes.Diamond, new Vector2(70, 83), this));
            chest.Contents.Add(ItemFactory.CreateItem(DodongosQuest.Items.ItemTypes.PotionOfMinorHealth, new Vector2(70, 83), this));
            _containers.Add(chest);

            chest = new TreasureChest(new List<IItem>(), false, new Vector2(6, 94), this);
            chest.Contents.Add(ItemFactory.CreateItem(DodongosQuest.Items.ItemTypes.Diamond, new Vector2(6, 94), this));
            _containers.Add(chest);            

            _doors.Add(new Door(new Vector2(55, 34), false, this));
            _doors.Add(new Door(new Vector2(62, 33), false, this));
            _doors.Add(new Door(new Vector2(51, 16), false, this));
            _doors.Add(new Door(new Vector2(57, 16), false, this));
            _doors.Add(new Door(new Vector2(45, 21), false, this));
            _doors.Add(new Door(new Vector2(45, 9), false, this));
            _doors.Add(new Door(new Vector2(42, 6), false, this));
            _doors.Add(new Door(new Vector2(30, 11), false, this));
            _doors.Add(new Door(new Vector2(30, 23), false, this));
            _doors.Add(new Door(new Vector2(15, 18), false, this));
            _doors.Add(new Door(new Vector2(47, 73), false, this));
            _doors.Add(new Door(new Vector2(49, 51), false, this));
            _doors.Add(new Door(new Vector2(42, 51), false, this));
            _doors.Add(new Door(new Vector2(43, 40), false, this));
            _doors.Add(new Door(new Vector2(27, 44), false, this));
        }

        void manEatingPlant_Death(ICreature sender)
        {
            if (AchievementUnlocked != null)
                AchievementUnlocked(new AchievementNotifier(new Vector2(0, 0), 5000, ContentHelper.Content.Load<Texture2D>(@"Achievements\FlowerKiller"), "Plant Killer", "Kill a man eating plant."));
        }

        void World_DoneWithQuest(IQuest sender)
        {
            _quests.Remove(sender);
        }

        void HandleCreatureDeath(ICreature sender)
        {
            if (sender.Remains != null)
            {
                sender.Remains.WorldIndex = sender.WorldIndex;
                _remains.Add(sender.Remains);
            }
            _creatures.Remove(sender);
        }

        void HandleFlameAttack(ICreature sender)
        {
            Vector2 testLocation = new Vector2(sender.WorldIndex.X , sender.WorldIndex.Y);
            Fire testFlame = new Fire(testLocation, this);          
            testFlame.Flame += new Fire.FlamingAttackEvent(Flame_Out);
            _fire.Add(testFlame);
            
        }

        void Flame_Out(Fire sender)
        {
            _fire.Remove(sender);
        }

        public void CenterCameraOnPlayer()
        {
            Vector2 playerWorldPosition = ConvertTileIndexToWorldPosition(Player.WorldIndex.X, Player.WorldIndex.Y);
            Camera.CenterCameraOnWorldPosition(playerWorldPosition.X, playerWorldPosition.Y);
        }

        public bool MovePlayerInDirectionSuccessful(Direction direction)
        {
            bool turnOver = MoveCreatureInDirectionSuccessful(direction, Player);
            if (turnOver)
            {
                DiscoverTerrainAroundPlayer();
            }
            return turnOver;
        }

        public Vector2 GetWorldIndexOneSpaceAway(Direction direction, Vector2 startingIndex)
        {
            Vector2 worldIndex = new Vector2(startingIndex.X, startingIndex.Y);

            if (direction == Direction.North)
                worldIndex.Y -= 1;
            else if (direction == Direction.South)
                worldIndex.Y += 1;
            else if (direction == Direction.East)
                worldIndex.X += 1;
            else if (direction == Direction.West)
                worldIndex.X -= 1;

            return worldIndex;
        }

        public bool MoveCreatureInDirectionSuccessful(Direction direction, ICreature creature)
        {
            Vector2 attemptedMovePosition = GetWorldIndexOneSpaceAway(direction, creature.WorldIndex);
            
            bool turnOver = MovingToSpaceCausesAttack(attemptedMovePosition, creature);
            if (IsIndexWithinBoundsOfWorld(attemptedMovePosition))
            {
                if (turnOver == false && creature.IsPlayer())
                    turnOver = MovingToSpaceOpensContainer(attemptedMovePosition, creature);
                if (turnOver == false && creature.IsPlayer())
                    turnOver = MovingToSpaceOpensDoor(attemptedMovePosition, creature);
                if (turnOver == false)
                    turnOver = MovingToSpaceIsSuccessful(attemptedMovePosition, creature);
            }

            if(turnOver && creature.IsPlayer())
                DiscoverTerrainAroundPlayer();
            return turnOver;
        }

        private bool MovingToSpaceIsSuccessful(Vector2 movePosition, ICreature creature)
        {
            if (Tiles[(int)movePosition.X, (int)movePosition.Y].CanCreatureEnter(creature))
            {
                IContainer containerAtPosition = GetContainerAtIndex(movePosition);
                if (containerAtPosition == null)
                {
                    creature.WorldIndex = movePosition;
                    
                    return true;
                }
            }

            return false;
        }

      
        private bool MovingToSpaceCausesAttack(Vector2 movePosition, ICreature creature)
        {
            ICreature creatureAtPosition = GetCreatureAtIndex(movePosition);
            if (creatureAtPosition != null)
            {  
                creature.AttackCreature(ref creatureAtPosition);
                return true;
            }
            else
                return false;
        }

        private bool MovingToSpaceOpensContainer(Vector2 movePosition, ICreature creature)
        {
            IContainer containerAtPosition = GetContainerAtIndex(movePosition);
            if (containerAtPosition != null)
            {
                if (containerAtPosition.IsLocked)
                {
                    IItem key = creature.GetItemFromInventory(ItemTypes.OldKey);
                    if (key == null)
                    {
                        Announcer.Instance.Announce("Container is locked.", MessageTypes.Other);
                        return false;
                    }
                    Announcer.Instance.Announce(creature.Name + " used " + key.Name + ".", MessageTypes.Other);
                    key.Give(creature);
                }

                foreach (IItem item in containerAtPosition.Contents)
                {
                    item.Get(creature);
                }
                containerAtPosition.Contents.Clear();
                return true;
            }
            else
                return false;
        }

        private bool MovingToSpaceOpensDoor(Vector2 movePosition, ICreature creature)
        {
            Door doorAtPosition = GetDoorAtIndex(movePosition);
            if (doorAtPosition != null)
            {
                if (doorAtPosition.IsOpen == false)
                {
                    doorAtPosition.Open();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }

        public void DiscoverTerrainAroundPlayer()
        {
            foreach (ITerrain terrain in Tiles)
                terrain.IsVisibleToPlayer = false;

            List<Vector2> discoveredTiles = new List<Vector2>();
            for (int y = 0; y < WORLD_HEIGHT; y++)
            {
                for (int x = 0; x < WORLD_WIDTH; x++)
                {
                    List<Vector2> pathToCurrentPoint = GetWorldIndexPointsAlongLine(new Vector2(x, y), Player.WorldIndex);
                    if (pathToCurrentPoint.Count < Player.ViewDistance.Current)
                    {
                        bool currentPointIsVisible = true;
                        foreach (Vector2 pointAlongPath in pathToCurrentPoint)
                        {
                            if (Tiles[(int)pointAlongPath.X, (int)pointAlongPath.Y].ObstructsLineOfSight)
                            {
                                currentPointIsVisible = false;
                                break;
                            }
                            Door doorAtIndex = GetDoorAtIndex(pointAlongPath);
                            if(doorAtIndex != null)
                                if (doorAtIndex.IsOpen == false)
                                {
                                    currentPointIsVisible = false;
                                    break;
                                }
                        }
                        if (currentPointIsVisible)
                            discoveredTiles.Add(new Vector2(x, y));
                    }
                }
            }

            foreach (Vector2 index in discoveredTiles)
                DiscoverTerrainAndSetVisible(index);
        }

        public bool PlayerCanSeeWorldIndex(Vector2 worldIndex)
        {
            if (Tiles[(int)worldIndex.X, (int)worldIndex.Y].IsVisibleToPlayer == true)
                return true;
            else
                return false;
        }

        public bool PlayerHasExploredWorldIndex(Vector2 worldIndex)
        {
            if (Tiles[(int)worldIndex.X, (int)worldIndex.Y].IsExploredByPlayer == true)
                return true;
            else
                return false;
        }

        private void DiscoverTerrain(Vector2 worldIndex)
        {
            List<Vector2> terrainToAffect = GetSurroundingWorldIndexPositions(worldIndex);
            foreach (Vector2 index in terrainToAffect)
                Tiles[(int)index.X, (int)index.Y].IsExploredByPlayer = true;
        }

        private void DiscoverTerrainAndSetVisible(Vector2 worldIndex)
        {
            List<Vector2> terrainToAffect = GetSurroundingWorldIndexPositions(worldIndex);
            foreach (Vector2 index in terrainToAffect)
            {
                Tiles[(int)index.X, (int)index.Y].IsExploredByPlayer = true;
                Tiles[(int)index.X, (int)index.Y].IsVisibleToPlayer = true;
            }
        }

        public ICreature GetCreatureAtIndex(Vector2 index)
        {
            if (Player.WorldIndex == index)
                return Player;
            foreach (ICreature creature in _creatures)
            {
                if (creature.WorldIndex == index)
                    return creature;
            }

            return null;
        }

        public bool IsWorldIndexFreeOfObstacles(Vector2 index)
        {
            IContainer containerAtIndex = GetContainerAtIndex(index);
            Door doorAtIndex = GetDoorAtIndex(index);
            ICreature creatureAtIndex = GetCreatureAtIndex(index);
            if (containerAtIndex == null && doorAtIndex == null && creatureAtIndex == null)
                return true;
            else
                return false;
        }

        public IContainer GetContainerAtIndex(Vector2 index)
        {
            foreach (IContainer container in _containers)
            {
                if (container.WorldIndex == index)
                    return container;
            }

            return null;
        }

        public Door GetDoorAtIndex(Vector2 index)
        {
            foreach (Door door in _doors)
            {
                if (door.WorldIndex == index)
                    return door;
            }

            return null;
        }

        public bool CloseDoorAtPositionSuccessful(Vector2 doorWorldIndex, Vector2 playerWorldIndex)
        {
            List<Vector2> validPositionsToCloseDoor = GetSurroundingWorldIndexPositions(playerWorldIndex);
            if (validPositionsToCloseDoor.Contains(doorWorldIndex))
            {
                Door doorAtIndex = GetDoorAtIndex(doorWorldIndex);
                if (doorAtIndex != null)
                    if (doorAtIndex.IsOpen)
                    {
                        doorAtIndex.Close();
                        DiscoverTerrainAroundPlayer();
                        return true;
                    }
            }

            return false;
        }

        public List<Vector2> GetSurroundingWorldIndexPositions(Vector2 worldIndex)
        {
            List<Vector2> surroundingWorldIndexPositions = new List<Vector2>();

            Vector2 left = new Vector2(worldIndex.X - 1, worldIndex.Y);
            Vector2 right = new Vector2(worldIndex.X + 1, worldIndex.Y);
            Vector2 above = new Vector2(worldIndex.X, worldIndex.Y - 1);
            Vector2 below = new Vector2(worldIndex.X, worldIndex.Y + 1);

            if (IsIndexWithinBoundsOfWorld(left))
                surroundingWorldIndexPositions.Add(left);
            if (IsIndexWithinBoundsOfWorld(right))
                surroundingWorldIndexPositions.Add(right);
            if (IsIndexWithinBoundsOfWorld(above))
                surroundingWorldIndexPositions.Add(above);
            if (IsIndexWithinBoundsOfWorld(below))
                surroundingWorldIndexPositions.Add(below);
            return surroundingWorldIndexPositions;
        }

        private bool IsIndexWithinBoundsOfWorld(Vector2 index)
        {
            if (index.X >= 0 && index.X < WORLD_WIDTH)
                if (index.Y >= 0 && index.Y < WORLD_HEIGHT)
                    return true;

            return false;
        }

        public Vector2 GetWorldSize()
        {
            return new Vector2(WORLD_WIDTH * TILE_SIZE, WORLD_HEIGHT * TILE_SIZE);
        }

        public void SetMap(string mapFile)
        {
            Bitmap mapImage = (Bitmap)Bitmap.FromFile(mapFile);

            Tiles = new ITerrain[WORLD_WIDTH, WORLD_HEIGHT];

            for (int x = 0; x < WORLD_WIDTH; x++)
                for (int y = 0; y < WORLD_HEIGHT; y++)
                {
                    Vector2 worldIndex = new Vector2(x, y);

                    if (mapImage.GetPixel(x, y).Name == "ff6a4e2f")
                    {
                        Tiles[x, y] = TerrainFactory.CreateCaveFloor(worldIndex, this);
                    }
                    else if (mapImage.GetPixel(x, y).Name == "ff8c8c8c")
                    {
                        Tiles[x, y] = TerrainFactory.CreateCaveWall(worldIndex, this); 
                    }
                    else if (mapImage.GetPixel(x, y).Name == "ff000000")
                    {
                        Tiles[x, y] = TerrainFactory.CreateNothing(worldIndex, this);
                    }
                    else if (mapImage.GetPixel(x, y).Name == "ff0000ff")
                    {
                        Tiles[x, y] = TerrainFactory.CreateWater(worldIndex, this);
                    }
                    else
                    {
                        Console.WriteLine("ERROR: Unknown terrain type");
                    }
                }
        }

        public List<ICreature> GetCreaturesWithinVisibleDistanceOfPlayer(int range)
        {
            SortedList<string, ICreature> creaturesWithinRange = new SortedList<string, ICreature>();

            foreach (ICreature creature in _creatures)
            {
                if (CanPlayerSeeWorldIndex(creature.WorldIndex))
                {
                    int distanceFromPlayer = GetStraightLineDistance(Player.WorldIndex, creature.WorldIndex);
                    if (distanceFromPlayer <= range)
                        creaturesWithinRange.Add(distanceFromPlayer.ToString() + Guid.NewGuid().ToString(), creature);
                }
            }

            return new List<ICreature>(creaturesWithinRange.Values);
        }

        public Vector2 ConvertTileIndexToWorldPosition(float x, float y)
        {
            return new Vector2(x * TILE_SIZE + 1, y * TILE_SIZE + 1);
        }

        public Vector2 ConvertScreenPositionToTileIndex(float x, float y)
        {
            Vector2 worldPosition = Camera.GetWorldPosition(new Vector2(x, y));
            return ConvertWorldPositionToTileIndex(worldPosition.X, worldPosition.Y);
        }

        public Vector2 ConvertWorldPositionToTileIndex(float x, float y)
        {
            float iX = (x - 1) / TILE_SIZE;
            float iY = (y - 1) / TILE_SIZE;
            return new Vector2((int)iX, (int)iY);
        }

        public List<Vector2> GetWorldIndexPointsAlongLine(Vector2 point1, Vector2 point2)
        {
            // this is using Bresenham's Line Algorithm (http://en.wikipedia.org/wiki/Bresenham's_line_algorithm)

            List<Vector2> points = new List<Vector2>();

            int x0 = (int)point1.X;
            int x1 = (int)point2.X;
            int y0 = (int)point1.Y;
            int y1 = (int)point2.Y;
            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (steep)
            {
                int temp = y0;
                y0 = x0;
                x0 = temp;
                temp = y1;
                y1 = x1;
                x1 = temp;
            }
            if (x0 > x1)
            {
                int temp = x1;
                x1 = x0;
                x0 = temp;
                temp = y1;
                y1 = y0;
                y0 = temp;
            }

            int deltax = x1 - x0;
            int deltay = Math.Abs(y1 - y0);
            int error = deltax / 2;
            int ystep;
            int y = y0;
            if (y0 < y1)
                ystep = 1;
            else
                ystep = -1;

            for (int x = x0; x <= x1; x++)
            {
                if (steep)
                    points.Add(new Vector2(y, x));
                else
                    points.Add(new Vector2(x, y));
                error = error - deltay;
                if (error < 0)
                {
                    y = y + ystep;
                    error = error + deltax;
                }
            }

            return points;
        }

        public int GetStraightLineDistance(Vector2 point1, Vector2 point2)
        {
            List<Vector2> points = GetWorldIndexPointsAlongLine(point1, point2);

            return points.Count - 1;
        }

        public void Update(GameTime gameTime)
        {
            foreach (ITerrain terrain in Tiles)
                terrain.Update(gameTime);
            for(int i = _creatures.Count - 1; i >= 0; i--)
                _creatures[i].Update(gameTime);
            for (int i = _containers.Count - 1; i >= 0; i--)
                _containers[i].Update(gameTime);
            for (int i = _doors.Count - 1; i >= 0; i--)
                _doors[i].Update(gameTime);
            for (int i = _quests.Count - 1; i >= 0; i--)
                _quests[i].Update(gameTime);
            for (int i = _fire.Count - 1; i >= 0; i--)
                _fire[i].Update(gameTime);
            for (int i = _specialEffects.Count - 1; i >= 0; i--)
                _specialEffects[i].Update(gameTime);
            Player.Update(gameTime);
        }
             
        public void Draw(GameTime gameTime)
        {
            foreach (ITerrain terrain in Tiles)
                terrain.Draw(gameTime);
            for (int i = _creatures.Count - 1; i >= 0; i--)
                _creatures[i].Draw(gameTime);
            for (int i = _containers.Count - 1; i >= 0; i--)
                _containers[i].Draw(gameTime);
            for (int i = _doors.Count - 1; i >= 0; i--)
                _doors[i].Draw(gameTime);
            for (int i = _remains.Count - 1; i >= 0; i--)
                _remains[i].Draw(gameTime);  
            for (int i = _fire.Count - 1; i >= 0; i--)
                _fire[i].Draw(gameTime);
            for (int i = _specialEffects.Count - 1; i >= 0; i--)
                _specialEffects[i].Draw(gameTime);
            Player.Draw(gameTime);
            
        }

        public void TakeTurn()
        {
            foreach (ICreature creature in _creatures)
                creature.TakeTurn();
        }

        public bool CanPlayerSeeWorldIndex(Vector2 worldIndex)
        {
            return Tiles[(int)worldIndex.X, (int)worldIndex.Y].IsVisibleToPlayer;
        }

        public void AddSpecialEffectToWorld(ISpecialEffect specialEffect)
        {
            specialEffect.Done += new SpecialEffectEvent(SpecialEffectDone);
            _specialEffects.Add(specialEffect);
        }

        private void SpecialEffectDone(ISpecialEffect sender)
        {
            _specialEffects.Remove(sender);
        }
    }
}
