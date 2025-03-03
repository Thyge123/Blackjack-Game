using System;

namespace BlackJackGame
{
    public class Game
    {
        private readonly Player Player = new();
        private readonly Dealer Dealer = new();
        private readonly Deck Deck = new();

        public Game() { }

        public void StartGame()
        {
            while (true)
            {
                DisplayMenu();
                string? input = Console.ReadLine()?.ToLower();

                if (input == "q") return;
                if (input != "m") continue;

                PlayRound();
            }
        }

        private void PlayRound()
        {
            // Deal initial cards
            Player.DealInitialHand(Deck);
            Dealer.DealInitialHand(Deck);

            // Check for initial blackjacks
            if (Player.HasBlackjack())
            {
                DisplayGameState();
                Console.WriteLine("\nPlayer has blackjack! Player wins!");
                return;
            }

            if (Dealer.HasBlackjack())
            {
                DisplayGameState();
                Console.WriteLine("\nDealer has blackjack! Dealer wins!");
                return;
            }

            // Player's turn
            while (true)
            {
                DisplayGameState();

                Console.WriteLine("\nPress 'h' to hit or 's' to stand");
                string? input = Console.ReadLine()?.ToLower();

                if (input == "h")
                {
                    PlayerHit();
                    if (CheckGameEndConditions()) return;
                }
                else if (input == "s")
                {
                    PlayerStand();
                    return;
                }
            }
        }

        private void PlayerHit()
        {
            Player.Hit(Deck);

            if (Player.IsBust())
            {
                DisplayGameState();
                Console.WriteLine("\nPlayer busts! Dealer wins!");
                return;
            }
        }

        private void PlayerStand()
        {
            Console.WriteLine("\nPlayer stands");
            Dealer.PlayTurn(Deck);

            DisplayGameState();

            if (Dealer.GetHandValue() > 21)
            {
                Console.WriteLine("\nDealer busts! Player wins!");
            }
            else if (Dealer.GetHandValue() > Player.GetHandValue())
            {
                Console.WriteLine("\nDealer wins!");
            }
            else if (Dealer.GetHandValue() == Player.GetHandValue())
            {
                Console.WriteLine("\nIt's a tie!");
            }
            else
            {
                Console.WriteLine("\nPlayer wins!");
            }
        }

        private bool CheckGameEndConditions()
        {
            if (Player.HasBlackjack())
            {
                DisplayGameState();
                Console.WriteLine("\nPlayer has blackjack! Player wins!");
                return true;
            }

            if (Dealer.HasBlackjack())
            {
                DisplayGameState();
                Console.WriteLine("\nDealer has blackjack! Dealer wins!");
                return true;
            }

            if (Player.IsBust())
            {
                DisplayGameState();
                Console.WriteLine("\nPlayer busts! Dealer wins!");
                return true;
            }

            if (Dealer.GetHandValue() > 21)
            {
                DisplayGameState();
                Console.WriteLine("\nDealer busts! Player wins!");
                return true;
            }

            return false;
        }

        private void DisplayGameState()
        {
            Console.WriteLine("\nPlayer's Hand:");
            Console.WriteLine($"Value: {Player.GetHandValue()}");
            Console.WriteLine($"Cards: {Player.Hand}");

            Console.WriteLine("\nDealer's Hand:");
            Console.WriteLine($"Value: {Dealer.GetHandValue()}");
            Console.WriteLine($"Cards: {Dealer.Hand}");
        }

        public static void DisplayMenu()
        {
            Console.WriteLine("\nWelcome to BlackJack!");
            Console.WriteLine("Press 'm' to start the game");
            Console.WriteLine("Press 'q' to quit the game");
        }
    }
}
