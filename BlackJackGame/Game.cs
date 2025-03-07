using System;

namespace BlackJackGame
{
    public class Game
    {
        // Create instances of the Player, Dealer, and Deck classes
        private readonly Player Player = new();
        private readonly Dealer Dealer = new();
        private readonly Deck Deck = new();

        private int round = 0;

        // Start the game
        public void StartGame()
        {
            while (true)
            {
                DisplayMenu();
                string? input = Console.ReadLine()?.ToLower();

                if (input == "q") return; // If input is 'q', exit the game
                if (input != "m") continue; // If input is not 'm', go back to the start of the loop

                ResetGameState();
                PlayRound();
            }
        }

        // Reset the game state
        private void ResetGameState()
        {
            round = 0;
            Player.Hand.ClearHand();
            Dealer.Hand.ClearHand();
        }

        // Play a round of the game
        private void PlayRound()
        {
            // Deal initial cards
            Player.DealInitialHand(Deck);
            Dealer.DealInitialHand(Deck);
         
            // Check for immediate blackjack
            if (CheckInitialBlackjacks())
                return;

            // Player's turn
            if (!PlayerTurn())
                return;

            // Dealer's turn (only happens if player stands)
            DealerTurn();
        }

        // Check for immediate blackjack
        private bool CheckInitialBlackjacks()
        {
            if (Player.HasBlackjack())
            {
                Console.WriteLine("\nPlayer has blackjack! Player wins!");
                return true;
            }
            if (Dealer.HasBlackjack())
            {
                Console.WriteLine("\nDealer has blackjack! Dealer wins!");
                return true;
            }
            return false;
        }


        // Player's turn
        private bool PlayerTurn()
        {
            while (true) // Loop until player stands or busts
            {
              
                DisplayGameState();

                Console.WriteLine("\nPress 'h' to hit or 's' to stand");
                string? input = Console.ReadLine()?.ToLower();

                if (input == "h")
                {
                    Player.Hit(Deck);
                    Console.WriteLine("\nPlayer hits!");

                    if (Player.IsBust() || Player.HasBlackjack()) // Check if player busts or gets blackjack
                    {
                        DisplayGameState(showAllDealerCards: true);
                        if (EvaluateGameResult())
                            return false;
                    }
                }
                else if (input == "s")
                {
                    Player.Stand();
                    return true;
                }
                round++;
            }
        }

        // Dealer's turn
        private void DealerTurn()
        {
            Dealer.PlayTurn(Deck); // Dealer plays until hand value is 17 or higher
            DisplayGameState(showAllDealerCards: true); // Show all dealer cards
            EvaluateGameResult();
        }

        // Evaluate the game result
        private bool EvaluateGameResult()
        {
            if (Player.HasBlackjack())
            {
                Console.WriteLine("\nPlayer has blackjack! Player wins!");
                return true;
            }
            else if (Dealer.HasBlackjack())
            {
                Console.WriteLine("\nDealer has blackjack! Dealer wins!");
                return true;
            }
            else if (Player.IsBust())
            {
                Console.WriteLine("\nPlayer busts! Dealer wins!");
                return true;
            }
            else if (Dealer.GetHandValue() > 21)
            {
                Console.WriteLine("\nDealer busts! Player wins!");
                return true;
            }
            else if (Player.DoesStand)
            {
                int playerValue = Player.GetHandValue();
                int dealerValue = Dealer.GetHandValue();

                if (dealerValue > playerValue)
                {
                    Console.WriteLine("\nDealer wins!");
                    return true;
                }
                else if (dealerValue < playerValue)
                {
                    Console.WriteLine("\nPlayer wins!");
                    return true;
                }
                else
                {
                    Console.WriteLine("\nIt's a tie!");
                    return true;
                }
            }
            return false;
        }

        // Display the game state
        private void DisplayGameState(bool showAllDealerCards = false)
        {
            Console.WriteLine("\nPlayer's Hand:");
            Console.WriteLine($"Value: {Player.GetHandValue()}");
            Console.WriteLine($"Cards: {Player.Hand}");

            Console.WriteLine("\nDealer's Hand:");
            if (round == 0 && !showAllDealerCards)
            {
                Console.WriteLine($"Value: {Dealer.GetHandRoundOne()}");
                Console.WriteLine($"Cards: {Dealer.GetFaceUpCard()}, [Face Down]");
            }
            else
            {
                Console.WriteLine($"Value: {Dealer.GetHandValue()}");
                Console.WriteLine($"Cards: {Dealer.Hand}");
            }
        }

        public static void DisplayMenu()
        {
            Console.WriteLine("\nWelcome to BlackJack!\n");
            Console.WriteLine("Press 'm' to start the game");
            Console.WriteLine("Press 'q' to quit the game");
        }
    }
}