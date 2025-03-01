Understanding the Role of Each Script
Script Name	             Role
LightSource.cs :	Controls the movement and interaction of light sources in the game. Avoids other lights and damages the player upon contact.
PowerUp.cs	: Handles different power-ups (heal, speed boost) that the player can collect.
Player.cs	: Manages player movement, health, damage, healing, and speed boosts. Inherits from GameEntity.
GameEntity.cs	: Abstract base class for all game entities. Defines an UpdateState method that must be implemented in derived classes.
PauseMenu.cs	: Controls pausing, resuming, and quitting the game.
LightMover.cs	: Moves UI-based light elements randomly within the canvas.
LevelManager.cs	: Spawns lights and power-ups at specified locations. Handles cleanup of destroyed objects.
GameManager.cs	: Manages game state, including win/loss conditions, pausing, and timer updates. Implements the Singleton pattern.
MainMenuManager.cs	: Controls the main menu, level selection, and quitting the game.
IUpdatable.cs	: Interface for updating game entities (UpdateState).
