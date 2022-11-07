using System;
using System.Collections;
using System.Collections.Generic;

namespace Hangman
{
    class Program
    {
        //Displays current state of board: hangman drawing, guessed letters, wrong letters
        static void DisplayBoard(List<string> game_board, string secret_word, int wrong_counter, List<Char> found_letters, List<Char> wrong_letters, bool word_guess_correct)
        { 
            Console.WriteLine(game_board[wrong_counter]);
            //guessed letters in place, blanks where missing letters:
            Console.WriteLine();
            //if user guessed word correctly, print secret_word
            if (word_guess_correct)
            {
                Console.WriteLine(secret_word.ToUpper());
            }
            //otherwise
            else
            {
                //for each letter in secret word:
                foreach (char letter in secret_word)
                {
                    //if current_letter in guessed_letters:
                    if (found_letters.Contains(letter))
                    {
                        //print letter
                        Console.Write(Char.ToUpper(letter) + " ");
                    }
                    else
                    {
                        //print _
                        Console.Write("_ ");
                    }
                }
            }

            //incorrect guess letters
            //for each letter in wrong_letters:
            Console.WriteLine();
            Console.Write("Wrong letters: ");
            foreach (char letter in wrong_letters)
            {
                Console.Write(Char.ToUpper(letter) + " ");
            }
            Console.WriteLine();
        }
        
        static void Main(string[] args)
        {
            // create word bank
            var word_bank = new List<string>() { "ant", "baboon", "badger", "bat", "bear", "beaver", "camel", "cat", "clam", "cobra", "cougar" };

            //create list of hangman drawings
            var game_board = new List<string>()
            {
                @"
                      +---+
                      |   |
                          |
                          |
                          |
                          |
                    =========
                ",
                @"
                      +---+
                      |   |
                      O   |
                          |
                          |
                          |
                    =========
                ",
                @"
                      +---+
                      |   |
                      O   |
                      |   |
                          |
                          |
                    =========
                ",
                @"
                      +---+
                      |   |
                      O   |
                     /|   |
                          |
                          |
                    =========
                ",
                @"
                      +---+
                      |   |
                      O   |
                     /|\  |
                          |
                          |
                    =========
                ",
                @"
                      +---+
                      |   |
                      O   |
                     /|\  |
                     /    |
                          |
                    =========
                ",
                @"
                      +---+
                      |   |
                      O   |
                     /|\  |
                     / \  |
                          |
                    =========
                ",
            };

            //select random word
            System.Random random = new System.Random();
            string secret_word = word_bank[random.Next(0, 10)];
            //Console.WriteLine(secret_word); //print the answer! -> for testing

            //display number of letters
            Console.WriteLine();
            foreach (char letter in secret_word)
            {
                Console.Write("_ ");
            }
            Console.WriteLine();

            //initialise  wrong_counter as 0
            int wrong_counter = 0;
            //initialise empty found_letters and wrong_letters lists
            var found_letters = new List<Char>() { };
            var wrong_letters = new List<Char>() { };
            //initialise word_guess_correct as false - used for final display if user guesses correct word
            bool word_guess_correct = false;

            //game loop
            //while wrong_counter less than  7
            while (wrong_counter < 7)
            {

                //ask user if would like to guess letter or word
                Console.WriteLine("Would you like to guess a word or a letter? Enter W or L");
                string user_choice = Console.ReadLine();

                //if user wants to guess word-----------------------------
                if (user_choice == "W" || user_choice == "w")
                {

                    //get user word input
                    Console.WriteLine("Input guess word: ");
                    string user_word_input = Console.ReadLine();

                    //if word = secret word, upper or lower case accepted
                    if (user_word_input == secret_word || user_word_input == secret_word.ToUpper() )
                    {
                        //user wins
                        DisplayBoard(game_board, secret_word, wrong_counter, found_letters, wrong_letters, word_guess_correct=true);
                        Console.WriteLine("CORRECT! YOU WIN!");
                        break;
                    }
                    else
                    {
                        //increment wrong_counter to add extra line to board when game next displayed
                        ++wrong_counter;
                        Console.WriteLine("WRONG " + wrong_counter);
                    }
                }

                //if user wants to guess letter-----------------------------
                else if (user_choice == "L" || user_choice == "l")
                {

                    //get user letter input
                    Console.WriteLine("Input guess letter: ");
                    Char user_letter_input = Console.ReadLine()[0];

                    //user input error handling: request alternative input if letter already tried
                    //while guessed letter is in previous guesses
                    while (found_letters.Contains(user_letter_input) || wrong_letters.Contains(user_letter_input))
                    {
                        Console.WriteLine("You've already tried that one! Enter new guess letter: ");
                        //take new user input
                        user_letter_input = Console.ReadLine()[0];
                    }

                    //if letter is in word
                    if (secret_word.Contains(user_letter_input))
                    {
                        //print correct guess!
                        Console.WriteLine("CORRECT GUESS!");
                        //append to letters_found array for display
                        found_letters.Add(user_letter_input);

                        //Endgame
                        //if no more letters left to guess, every letter in secret_word is in found_letters -> user wins
                        bool count_ok = true;
                        foreach (char letter in secret_word)
                        {
                            if ( !(found_letters.Contains(letter)) )
                            {
                                count_ok = false;
                            }
                        }
                        // if count_ok true, end game
                        if (count_ok)
                        {
                            DisplayBoard(game_board, secret_word, wrong_counter, found_letters, wrong_letters, word_guess_correct);
                            Console.WriteLine("WELL DONE! YOU WIN!");
                            break;
                        }

                    }
                    else
                    {
                        Console.WriteLine("WRONG GUESS");
                        //append to wrong_guesses array
                        wrong_letters.Add(user_letter_input);
                        //increment wrong_counter to add extra line to board when game next displayed
                        ++wrong_counter;
                    }

                }

                //user error handling: if don't select either option
                else
                {
                    Console.WriteLine("Invalid input: enter W or L");
                    continue;
                }

                //display current game board---------------------------------
                DisplayBoard(game_board, secret_word, wrong_counter, found_letters, wrong_letters, word_guess_correct);

            }

            //outside while loop
            Console.WriteLine("GAME OVER");
            Console.ReadLine(); //so doesn't bug

        }
    }
}
