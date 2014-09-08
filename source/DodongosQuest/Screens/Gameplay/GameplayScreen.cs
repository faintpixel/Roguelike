using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using xnaHelper.Helpers;
using xnaHelper.Input;
using DodongosQuest.Announcements;
using DodongosQuest.Achievements;
using DodongosQuest.Items;
using DodongosQuest.Creatures;
using DodongosQuest.ParticleSystem;
using DodongosQuest.Magic;
using DodongosQuest.Magic.Cast_Strategies;
using DodongosQuest.Special_Effects;
using DodongosQuest.Weapons;
using Microsoft.Xna.Framework.Input.Touch;
using System.Diagnostics;


namespace DodongosQuest.Screens.Gameplay
{
    public class GameplayScreen : IScreen 
    {
        private SideBar _sideBar;
        private MessageBox _messageBox;
        private Inventory _inventory;
        private SpellBook _spells;
        private MiniMap _miniMap;
        private List<AchievementNotifier> _achievementNotifications;
        private MouseHelper _mouse;
        private World _world;
        private User _user;

        private GameState _state;        
        private ISpell _selectedSpell;
        private TimeSpan _timeOfLastMouseMovement;
        private MouseState _previousMouseState;

        private List<ICreature> _availableTargets;
        private int _currentTargetIndex;
        private Texture2D _targetImage;

        public event EventHandler GameOver;

        public GameplayScreen()
        {            
            _state = GameState.PlayerTurn;
            
            _messageBox = _messageBox = new MessageBox(new Vector2(0, 628));
            Announcer.Instance.Announcement += new Announcer.AnnouncementEvent(AddAnnouncement);
            _world = new World();
            _world.AchievementUnlocked += new World.AchievementEvent(AchievementUnlocked);
            _user = new User();
            _user.UserInputReceived += new User.UserInput(UserInputReceived);
            _sideBar = new SideBar(new Vector2(919, 0), ref _world.Player);
            _inventory = new Inventory(new Vector2(10, 30), _world.Player.Inventory);
            _spells = new SpellBook(new Vector2(10, 400), _world.Player.Spells);
            Camera.SetWorldSize(_world.GetWorldSize().X + 120, _world.GetWorldSize().Y + 300);
            _mouse = new MouseHelper();
            _mouse.MouseButtonReleased += new MouseHelper.MouseButtonEventEvent(MouseButtonReleased);
            _achievementNotifications = new List<AchievementNotifier>();              
            _miniMap = new MiniMap(new Vector2(824, 428), _world);
            GameReference.Game.IsMouseVisible = true;
            _timeOfLastMouseMovement = new TimeSpan();
            _previousMouseState = Mouse.GetState();

            _availableTargets = new List<ICreature>();
            _currentTargetIndex = 0;
            _targetImage = ContentHelper.Content.Load<Texture2D>("target");
        }

        private void AchievementUnlocked(AchievementNotifier achievement)
        {
            achievement.DoneDisplaying += new AchievementNotifier.AchievementNotifierEvent(DoneDisplayingAchievement);
            _achievementNotifications.Add(achievement);
        }

        private void MouseButtonReleased(MouseHelper sender, MouseButtons button, MouseState state)
        {
            TouchOrMouse(new Vector2(state.X, state.Y));
        }

        private void TouchOrMouse( Vector2 pos )
        {
            if(_messageBox.IntersectsWith(new Vector2(pos.X, pos.Y)))
            {
                Announcer.Instance.Announce("Clicked message box.", MessageTypes.Other);
            }
            else if (_sideBar.IntersectsWith(new Vector2(pos.X, pos.Y)))
            {
                Announcer.Instance.Announce("Clicked side bar.", MessageTypes.Other);
            }
            else if (_inventory.IntersectsWith(new Vector2(pos.X, pos.Y)))
            {
                Announcer.Instance.Announce("Clicked inventory.", MessageTypes.Other);
            }
            else if (_spells.IntersectsWith(new Vector2(pos.X, pos.Y)))
            {
                //Announcer.Instance.Announce("Clicked spells.", MessageTypes.Other);
                int spellOffset = _spells.GetSpellAt(new Vector2(pos.X, pos.Y));
                if (spellOffset > -1)
                {
                    Announcer.Instance.Announce("Clicked spells: " + spellOffset, MessageTypes.Other);
                    AttemptToUseSpell(spellOffset);
                }
            }
            else
            {
                // clicked the map
                Vector2 clickedWorldIndex = _world.ConvertScreenPositionToTileIndex(pos.X, pos.Y);

                if (_state == GameState.PlayerTurn)
                {
                    ICreature target = _world.GetCreatureAtIndex(clickedWorldIndex);
                    if (target != null)
                    {
                        _world.Player.AttackCreature(ref target);
                        _state = GameState.ComputerTurn;
                    }
                    else
                    {
                        if (_world.PlayerCanSeeWorldIndex(clickedWorldIndex))
                        {
                            // a cheese hack toward getting touch sort of working
                            Vector2 delta = clickedWorldIndex - _world.Player.WorldIndex;
                            if (delta.X < 0)
                            {
                                if (_world.MovePlayerInDirectionSuccessful(Direction.West))
                                {
                                    _state = GameState.ComputerTurn;
                                }
                            }
                            if (delta.X > 0)
                            {
                                if (_world.MovePlayerInDirectionSuccessful(Direction.East))
                                {
                                    _state = GameState.ComputerTurn;
                                }
                            }
                            if (delta.Y < 0)
                            {
                                if (_world.MovePlayerInDirectionSuccessful(Direction.North))
                                {
                                    _state = GameState.ComputerTurn;
                                }
                            }
                            if (delta.Y > 0)
                            {
                                if (_world.MovePlayerInDirectionSuccessful(Direction.South))
                                {
                                    _state = GameState.ComputerTurn;
                                }
                            }
                            _world.CenterCameraOnPlayer();
                        }
                    }
                }
                else if (_state == GameState.PlayerTurnSelectingDoorToClose)
                {
                    if (_world.CloseDoorAtPositionSuccessful(clickedWorldIndex, _world.Player.WorldIndex))
                        _state = GameState.ComputerTurn;
                    else
                    {
                        Announcer.Instance.Announce("Door at location does not exist or can't be closed.", MessageTypes.Other);
                        _state = GameState.PlayerTurn;
                    }
                }
                else if (_state == GameState.PlayerTurnSelectingTargetForSpell)
                {
                    Vector2 targetIndex = clickedWorldIndex;
                    if (_selectedSpell.TargetCanMove == false)
                        targetIndex = _world.Player.WorldIndex;
                    if (_selectedSpell.CastSpell(_world.Player, targetIndex) == true)
                        _state = GameState.ComputerTurn;
                    else
                        _state = GameState.PlayerTurn;
                }

                Announcer.Instance.Announce(clickedWorldIndex.X + ", " + clickedWorldIndex.Y, MessageTypes.Other);
            }
        }

        private void AddAnnouncement(Message message)
        {
            _messageBox.AddMessage(message);
        }

        public void Draw(GameTime gameTime)
        {
            _world.Draw(gameTime);
            _sideBar.Draw(gameTime);
            _messageBox.Draw(gameTime);
            _inventory.Draw(gameTime);
            _spells.Draw(gameTime);
            _miniMap.Draw(gameTime);
            for (int i = _achievementNotifications.Count - 1; i >= 0; i--)
                _achievementNotifications[i].Draw(gameTime);

            if (_state == GameState.PlayerTurnSelectingTargetForSpell)
            {
                DrawAffectedAreaForSelectedSpell(gameTime);
            }

            if (_availableTargets.Count > _currentTargetIndex)
            {
                Vector2 worldPosition = _world.ConvertTileIndexToWorldPosition(_availableTargets[_currentTargetIndex].WorldIndex.X, _availableTargets[_currentTargetIndex].WorldIndex.Y);
                Vector2 screenPosition = Camera.GetScreenPosition(worldPosition);
                GraphicsHelper.spriteBatch.Draw(_targetImage, screenPosition, null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.29f);
            }

        }

        private void DrawAffectedAreaForSelectedSpell(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();
            Vector2 selectedWorldIndex;
            if (_selectedSpell.TargetCanMove)
                selectedWorldIndex = _world.ConvertScreenPositionToTileIndex(state.X, state.Y);
            else
                selectedWorldIndex = _world.Player.WorldIndex;
            _selectedSpell.AffectedArea.Draw(gameTime, selectedWorldIndex);
        }

        private void SetMouseVisibility(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (_previousMouseState.X == mouseState.X && _previousMouseState.Y == mouseState.Y)
            {
                if (gameTime.TotalGameTime - _timeOfLastMouseMovement >= new TimeSpan(0, 0, 1))
                    GameReference.Game.IsMouseVisible = false;
            }
            else
            {
                GameReference.Game.IsMouseVisible = true;
                _timeOfLastMouseMovement = gameTime.TotalGameTime;
            }
            _previousMouseState = mouseState;
        }

        public void Update(GameTime gameTime)
        {
            _world.Update(gameTime);
            _user.Update(gameTime);
            _sideBar.Update(gameTime);
            _messageBox.Update(gameTime);
            _inventory.Update(gameTime);
            _spells.Update(gameTime);
            Camera.Update(gameTime);
            _mouse.Update();

            // touch mega hack
            TouchCollection tc = TouchPanel.GetState();
            foreach (TouchLocation tl in tc)
            {
                if (tl.State == TouchLocationState.Released)
                {
                    Debug.WriteLine( "touch: " + tl.Position.ToString());
                    TouchOrMouse(tl.Position);
                    // the map just scolls offscreen for some reason
                    _world.CenterCameraOnPlayer();
                }
            }


            SetMouseVisibility(gameTime);            

            if (_state == GameState.ComputerTurn)
            {
                _world.TakeTurn();
                SetTargets();
                _world.Player.TakeTurn();
                _state = GameState.PlayerTurn;
            }

            for (int i = _achievementNotifications.Count - 1; i >= 0; i--)
                _achievementNotifications[i].Update(gameTime);

            if (_world.Player.Health.Current <= 0)
                if (GameOver != null)
                    GameOver(this, null);
        }

        private void SetTargets()
        {
            _availableTargets = _world.GetCreaturesWithinVisibleDistanceOfPlayer(_world.Player.Weapon.Range);
            if (_availableTargets.Count >= _currentTargetIndex)
                _currentTargetIndex = 0;
        }

        private void HandleUserInputForSelectingSpellTarget(User sender, List<Keys> userInput)
        {
            if (userInput.Contains(Controls.MoveLeft))
                _selectedSpell.AffectedArea.DirectionFacing = Direction.West;
            else if (userInput.Contains(Controls.MoveRight))
                _selectedSpell.AffectedArea.DirectionFacing = Direction.East;
            else if (userInput.Contains(Controls.MoveUp))
                _selectedSpell.AffectedArea.DirectionFacing = Direction.North;
            else if (userInput.Contains(Controls.MoveDown))
                _selectedSpell.AffectedArea.DirectionFacing = Direction.South;
            else if (userInput.Contains(Controls.CastSpellOrAttackTarget))
            {
                if (MouseIsOnWorld())
                {
                    MouseState mouseState = Mouse.GetState();
                    Vector2 selectedWorldIndex = _world.ConvertScreenPositionToTileIndex(mouseState.X, mouseState.Y);
                    if (_selectedSpell.TargetCanMove == false)
                        selectedWorldIndex = _world.Player.WorldIndex;
                    if (_selectedSpell.CastSpell(_world.Player, selectedWorldIndex) == true)
                        _state = GameState.ComputerTurn;
                    else
                        _state = GameState.PlayerTurn;
                }                
            }
        }

        private bool MouseIsOnWorld()
        {
            MouseState mouseState = Mouse.GetState();
            if (_sideBar.IntersectsWith(new Vector2(mouseState.X, mouseState.Y)) || _messageBox.IntersectsWith(new Vector2(mouseState.X, mouseState.Y)))
                return false;
            else
                return true;
        }

        private void HandleUserInputForPlayerTurn(User sender, List<Keys> userInput)
        {
            if (userInput.Contains(Controls.Wait))
            {
                _state = GameState.ComputerTurn;
            }
            else if (userInput.Contains(Controls.MoveLeft))
            {
                if (_world.MovePlayerInDirectionSuccessful(Direction.West))
                    _state = GameState.ComputerTurn;
            }
            else if (userInput.Contains(Controls.MoveRight))
            {
                if (_world.MovePlayerInDirectionSuccessful(Direction.East))
                    _state = GameState.ComputerTurn;
            }
            else if (userInput.Contains(Controls.MoveUp))
            {
                if (_world.MovePlayerInDirectionSuccessful(Direction.North))
                    _state = GameState.ComputerTurn;
            }
            else if (userInput.Contains(Controls.MoveDown))
            {
                if (_world.MovePlayerInDirectionSuccessful(Direction.South))
                    _state = GameState.ComputerTurn;
            }
            else if (userInput.Contains(Controls.CloseDoor))
            {
                _state = GameState.PlayerTurnSelectingDoorToClose;
                Announcer.Instance.Announce("Select door to close.", MessageTypes.Other);
            }
            else if (userInput.Contains(Controls.CenterViewOnPlayer))
            {
                _world.CenterCameraOnPlayer();
            }
            else if (userInput.Contains(Controls.UseHealthPotion))
            {
                IItem playersHealingPotion = _world.Player.GetItemFromInventory(ItemTypes.PotionOfMinorHealth);
                if (playersHealingPotion != null)
                {
                    ICreature playerCreature = (ICreature)_world.Player;
                    playersHealingPotion.Use(playerCreature);
                    _state = GameState.ComputerTurn;
                }
                else
                    Announcer.Instance.Announce("You have no healing potions.", MessageTypes.Other);
            }
            else if (userInput.Contains(Controls.TEST_DoSomething))
            {
                IWeapon bow = WeaponFactory.CreateBow(_world.Player.WorldIndex, _world);
                ((IItem)bow).Get(_world.Player);
            }
            else if (userInput.Contains(Controls.UseInventoryItem0))
            {
                AttemptToUseItemAtInventoryIndex(0);
            }
            else if (userInput.Contains(Controls.UseInventoryItem1))
            {
                AttemptToUseItemAtInventoryIndex(1);
            }
            else if (userInput.Contains(Controls.UseInventoryItem2))
            {
                AttemptToUseItemAtInventoryIndex(2);
            }
            else if (userInput.Contains(Controls.UseInventoryItem3))
            {
                AttemptToUseItemAtInventoryIndex(3);
            }
            else if (userInput.Contains(Controls.UseInventoryItem4))
            {
                AttemptToUseItemAtInventoryIndex(4);
            }
            else if (userInput.Contains(Controls.UseInventoryItem5))
            {
                AttemptToUseItemAtInventoryIndex(5);
            }
            else if (userInput.Contains(Controls.UseInventoryItem6))
            {
                AttemptToUseItemAtInventoryIndex(6);
            }
            else if (userInput.Contains(Controls.UseInventoryItem7))
            {
                AttemptToUseItemAtInventoryIndex(7);
            }
            else if (userInput.Contains(Controls.UseInventoryItem8))
            {
                AttemptToUseItemAtInventoryIndex(8);
            }
            else if (userInput.Contains(Controls.UseInventoryItem9))
            {
                AttemptToUseItemAtInventoryIndex(9);
            }
            else if (userInput.Contains(Controls.UseSpell0))
            {
                AttemptToUseSpell(0);
            }
            else if (userInput.Contains(Controls.UseSpell1))
            {
                AttemptToUseSpell(1);
            }
            else if (userInput.Contains(Controls.UseSpell2))
            {
                AttemptToUseSpell(2);
            }
            else if (userInput.Contains(Controls.UseSpell3))
            {
                AttemptToUseSpell(3);
            }
            else if (userInput.Contains(Controls.UseSpell4))
            {
                AttemptToUseSpell(4);
            }
            else if (userInput.Contains(Controls.UseSpell5))
            {
                AttemptToUseSpell(5);
            }
            else if (userInput.Contains(Controls.SelectNextTarget))
            {
                if (_availableTargets.Count > _currentTargetIndex + 1)
                    _currentTargetIndex += 1;
                else
                    _currentTargetIndex = 0;
            }
            else if (userInput.Contains(Controls.SelectPreviousTarget))
            {
                if (_currentTargetIndex - 1 >= 0)
                    _currentTargetIndex -= 1;
                else
                    _currentTargetIndex = _availableTargets.Count - 1;
            }
            else if (userInput.Contains(Controls.CastSpellOrAttackTarget))
            {
                if (_availableTargets.Count > _currentTargetIndex)
                {
                    ICreature target = _availableTargets[_currentTargetIndex];
                    _world.Player.AttackCreature(ref target);
                    _state = GameState.ComputerTurn;
                }
            }
        }

        private void UserInputReceived(User sender, List<Keys> userInput)
        {
            if (_state == GameState.PlayerTurnSelectingTargetForSpell)
            {
                HandleUserInputForSelectingSpellTarget(sender, userInput);
            }
            if (_state == GameState.PlayerTurn)
            {
                HandleUserInputForPlayerTurn(sender, userInput);
            }

            _world.CenterCameraOnPlayer();
        }

        private void AttemptToUseItemAtInventoryIndex(int index)
        {
            if (_world.Player.Inventory.Count > index)
            {
                ICreature playerCreature = _world.Player;
                _world.Player.Inventory[index].Use(playerCreature);
                _state = GameState.ComputerTurn;
            }
        }

        private void AttemptToUseSpell(int index)
        {
            if (_world.Player.Spells.Count > index)
            {
                _selectedSpell = _world.Player.Spells[index];
                Announcer.Instance.Announce("Select target for " + _selectedSpell.Name + ".", MessageTypes.Other);
                _state = GameState.PlayerTurnSelectingTargetForSpell;
            }
        }

        private void DoneDisplayingAchievement(AchievementNotifier sender)
        {
            _achievementNotifications.Remove(sender);
        }

    }
}
