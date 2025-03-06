# BlackJack Game

A console-based implementation of the classic BlackJack card game built with C# and .NET 8.

# Overview

This BlackJack implementation follows standard casino rules where players try to beat the dealer by getting a card value as close to 21 as possible without going over. The game features a complete deck of cards, player actions (hit/stand), dealer AI, and game state management.

# Features

•	Complete implementation of standard BlackJack rules

•	Proper card value handling (Aces can be 1 or 11)

•	Dealer AI that follows casino rules (hit on 16 or below, stand on 17+)

•	Automatic deck reshuffling when empty

•	Object-oriented design with classes for Card, Deck, Hand, Player, and Dealer

•	Special handling for soft 17 (Ace + 6) situations for the dealer



# Game Rules

•	Ace cards are worth 11 points, but automatically adjusted to 1 when needed to avoid busting

•	Face cards (Jack, Queen, King) are worth 10 points

•	Number cards are worth their face value

•	Dealer must hit on 16 or lower, and must hit on "soft 17" (Ace + 6)

•	Player can choose to "hit" (draw another card) or "stand" (end turn)

•	The winner is whoever has the highest card value without exceeding 21


# Project Structure

•	Card.cs: Represents a playing card with suit and value

•	CardSuit.cs: Defines the four card suits (Hearts, Diamonds, Clubs, Spades)

•	CardValue.cs: Defines the card values (Ace through King)

•	Deck.cs: Manages a collection of 52 cards with shuffling and drawing functionality

•	Hand.cs: Manages a player's current set of cards and calculates hand values

•	Player.cs: Base class with logic for player actions

•	Dealer.cs: Extends Player class with dealer-specific behavior

•	Game.cs: Controls game flow and manages rounds


# Requirements

•	.NET 8 SDK

•	C# 12.0 compatible IDE

# Getting Started

Clone the repository
git clone https://github.com/Thyge123/BlackJackGame.git

Navigate to project directory
cd BlackJackGame

Build the project
dotnet build

Run the game
dotnet run


# How to Play
• Launch the game

• Press 'm' to start a new game round

• You'll be dealt two cards, and the dealer will show one card

• Choose 'h' to hit (draw another card) or 's' to stand

• Try to get closer to 21 than the dealer without busting

• Press 'q' at any time to quit the game
