using System;

namespace Zuul
{
	public class Game
	{
		private Parser parser;
		private Player player;
		private Inventory inventory;

		public Game ()
		{
			parser = new Parser();
			player = new Player();
			CreateRooms();
		}

		private void CreateRooms()
		{
			// create the rooms
			Room outside = new Room("outside the main entrance of the university");
			Room theatre = new Room("in a lecture theatre");
			Room pub = new Room("in the campus pub");
			Room lab = new Room("in a computing lab");
			Room office = new Room("in the computing admin office");
			Room cave = new Room("in a dark and unpleasantly moist cave");
			
			Item JeMoeder = new Item(2000, "Yo mama so fat she doesn't even fit in your inventory");

			outside.Chest.Put("JeMoeder", JeMoeder);

			// initialise room exits
			outside.AddExit("east", theatre);
			outside.AddExit("south", lab);
			outside.AddExit("west", pub);
			outside.AddExit("down", cave);
			theatre.AddExit("west", outside);

			pub.AddExit("east", outside);

			lab.AddExit("north", outside);
			lab.AddExit("east", office);

			office.AddExit("west", lab);

			cave.AddExit("up", outside);
			player.CurrentRoom = outside;  // start game outside
		}

		/**
		 *  Main play routine.  Loops until end of play.
		 */
		public void Play()
		{
			PrintWelcome();

			// Enter the main command loop.  Here we repeatedly read commands and
			// execute them until the player wants to quit.
			bool finished = false;
			while (!finished)
			{
				if (player.IsAlive() == false)
				{
					Console.WriteLine("You are now dead, welcome to heaven.");
					finished = true;
				}
				else
				{
					Command command = parser.GetCommand();
					finished = ProcessCommand(command);
				}
			}
			Console.WriteLine("Thank you for playing.");
			Console.WriteLine("Press [Enter] to continue.");
			Console.ReadLine();
		}

		/**
		 * Print out the opening message for the player.
		 */
		private void PrintWelcome()
		{
			Console.WriteLine();
			Console.WriteLine("Welcome to Zuul!");
			Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
			Console.WriteLine("Type 'help' if you need help.");
			Console.WriteLine();
			Console.WriteLine(player.CurrentRoom.GetLongDescription());
		}

		/**
		 * Given a command, process (that is: execute) the command.
		 * If this command ends the game, true is returned, otherwise false is
		 * returned.
		 */
		private bool ProcessCommand(Command command)
		{
			bool wantToQuit = false;

			if(command.IsUnknown())
			{
				Console.WriteLine("I don't know what you mean...");
				return false;
			}

			string commandWord = command.GetCommandWord();
			switch (commandWord)
			{
				case "help":
					PrintHelp();
					player.ShowHealth();
					break;
				case "go":
					GoRoom(command);
					break;
				case "quit":
					wantToQuit = true;
					break;
				case "look":
					Console.WriteLine(player.CurrentRoom.GetLongDescription());
					break;
				case "take":
					Take(command);
					break;
				case "drop":
					Drop(command);
					break;
			}

			return wantToQuit;
		}

		// implementations of user commands:

		/**
		 * Print out some help information.
		 * Here we print the mission and a list of the command words.
		 */
		private void PrintHelp()
		{
			Console.WriteLine("You are lost. You are alone.");
			Console.WriteLine("You wander around at the university.");
			Console.WriteLine();
			// let the parser print the commands
			parser.PrintValidCommands();
		}

		/**
		 * Try to go to one direction. If there is an exit, enter the new
		 * room, otherwise print an error message.
		 */
		private void GoRoom(Command command)
		{
			if(!command.HasSecondWord())
			{
				// if there is no second word, we don't know where to go...
				Console.WriteLine("Go where?");
				return;
			}

			string direction = command.GetSecondWord();
			string itemName = command.GetSecondWord();

			// Try to go to the next room.
			Room nextRoom = player.CurrentRoom.GetExit(direction);

			if (nextRoom == null)
			{
				Console.WriteLine("There is no door to "+direction+"!");
			}
			else
			{
				player.CurrentRoom = nextRoom;
				player.Damage(50);
				Console.WriteLine(player.CurrentRoom.GetLongDescription());
			}
		}

		private void Take(Command command)
		{
			if (!command.HasSecondWord())
			{
				// if there is no second word, we don't know where to go...
				Console.WriteLine("Take what?");
				return;
			}
			player.TakeFromChest(command.GetSecondWord());
		}

		private void Drop(Command command)
		{
			if (!command.HasSecondWord())
			{
				// if there is no second word, we don't know where to go...
				Console.WriteLine("Drop where?");
				return;
			}
			player.DropToChest(command.GetSecondWord());
		}
	}
}

