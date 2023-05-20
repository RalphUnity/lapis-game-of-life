# lapis-game-of-life
Lapis Game of Life - MVC Implementation

This project is an implementation of the Game of Life using the Model-View-Controller (MVC) architectural pattern. The Game of Life is a cellular automaton devised by mathematician John Horton Conway, which simulates the evolution of a population of cells based on simple rules.

Features:
- Interactive gameplay: Users can interact with the game by toggling cells on/off and stepping through generations.
- Grid: The game grid is fixed to a size of 1280x720 pixels, providing a consistent visual representation.
- Customizable rules: Users can define their own rules for cell birth, survival, and death.
- Visual representation: The game provides a visual representation of the grid, updating in real-time as generations progress.

Architecture:
- Model: The Model component represents the data and logic of the game. It defines the grid, implements the rules of the Game of Life, and manages the state transitions between generations.
- View: The View component is responsible for rendering the grid and handling user interactions. It provides an interface for users to interact with the game, such as toggling cells on/off and stepping through generations.
- Controller: The Controller component acts as an intermediary between the Model and View. It receives user input from the View, updates the Model accordingly, and triggers the necessary updates in the View to reflect the changes.

Usage:
To play the Game of Life, please follow the instructions provided in the following link: [https://lapislazuligames.itch.io/game-of-life]
