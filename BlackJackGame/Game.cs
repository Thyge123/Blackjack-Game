using System;

namespace BlackJackGame
{
    public class Game
    {
        private readonly Player Player = new();
        private readonly Dealer Dealer = new();
        private readonly Deck Deck = new();

        private int round = 0;

        public void StartGame()
        {
            while (true)
            {
                DisplayMenu();
                string? input = Console.ReadLine()?.ToLower();

                if (input == "q") return;
                if (input != "m") continue;

                ResetGameState();
                PlayRound();
            }
        }

        private void ResetGameState()
        {
            round = 0;
            Player.Hand.ClearHand();
            Dealer.Hand.ClearHand();
        }

        private void PlayRound()
        {
            // Deal initial cards
            Player.DealInitialHand(Deck);
            Dealer.DealInitialHand(Deck);

            DisplayGameState();

            // Check for immediate blackjack
            if (CheckInitialBlackjacks())
                return;

            // Player's turn
            if (!PlayerTurn())
                return;

            // Dealer's turn (only happens if player stands)
            DealerTurn();
        }

        private bool CheckInitialBlackjacks()
        {
            if (Player.HasBlackjack())
            {
                Console.WriteLine("\nPlayer has blackjack! Player wins!");
                return true;
            }
            return false;
        }

        private bool PlayerTurn()
        {
            while (true)
            {
                round++;
                DisplayGameState();

                Console.WriteLine("\nPress 'h' to hit or 's' to stand");
                string? input = Console.ReadLine()?.ToLower();

                if (input == "h")
                {
                    Player.Hit(Deck);
                    Console.WriteLine("\nPlayer hits!");

                    if (Player.IsBust() || Player.HasBlackjack())
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
            }
        }

        private void DealerTurn()
        {
            Dealer.PlayTurn(Deck);
            DisplayGameState(showAllDealerCards: true);
            EvaluateGameResult();
        }

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