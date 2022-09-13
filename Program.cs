﻿using System;
using System.Collections.Generic;
using Roommates.Models;
using Roommates.Repositories;

namespace Roommates
{
    class Program
    {
        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true;TrustServerCertificate=true;";
       
        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);
            bool runProgram = true;
             while (runProgram)
 {
     string selection = GetMenuSelection();

     switch (selection)
     {
         case ("Show all rooms"):
             List<Room> rooms = roomRepo.GetAll();
             foreach (Room r in rooms)
             {
                 Console.WriteLine($"{r.Name} has an Id of {r.Id} and a max occupancy of {r.MaxOccupancy}");
             }
             Console.Write("Press any key to continue");
             Console.ReadKey();
             break;
         case ("Search for room"):
            Console.Write("Room Id: ");
            int id = int.Parse(Console.ReadLine());

            Room room = roomRepo.GetById(id);

            Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
            Console.Write("Press any key to continue");
            Console.ReadKey();
            break;
         case ("Add a room"):
            Console.Write("Room name: ");
            string name = Console.ReadLine();

            Console.Write("Max occupancy: ");
            int max = int.Parse(Console.ReadLine());

            Room roomToAdd = new Room()
            {
                Name = name,
                MaxOccupancy = max
            };

            roomRepo.Insert(roomToAdd);

            Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
            Console.Write("Press any key to continue");
            Console.ReadKey();
            break;

         case ("Show all chores"):
             List<Chore> chores = choreRepo.GetAll();
             foreach(Chore c in chores)
                        {
                            Console.WriteLine($"{c.Name} has an id of {c.Id}");

                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
         case ("Search for a chore"):
              Console.Write("Chore Id: ");
              int choreid = int.Parse(Console.ReadLine());

              Chore chore = choreRepo.GetById(choreid);

              Console.WriteLine($"{chore.Id} - {chore.Name} ");
              Console.Write("Press any key to continue");
              Console.ReadKey();
                        break;

         case ("Add a chore"):
              Console.Write("Chore name: ");
              string choreName = Console.ReadLine();

              Chore choreToAdd = new Chore()
                   {
                       Name = choreName 
                    };

              choreRepo.Insert(choreToAdd);

              Console.WriteLine($"{choreToAdd.Name} has been added and assigned an Id of {choreToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
         case ("Search for a roommate"):
              Console.Write("Roommate Id: ");
              int roommateId = int.Parse(Console.ReadLine());

              Roommate roommate = roommateRepo.GetById(roommateId);

              Console.WriteLine($" Roommate [{roommate.Id}]: Name- {roommate.FirstName} and lives in {roommate.Room.Name} ");
              Console.Write("Press any key to continue");
              Console.ReadKey();
                        break;
         case ("See unassigned chores"):
              List<Chore> unassignedChores = choreRepo.GetUnassignedChores();
              foreach (Chore c in unassignedChores)
                  {
                       Console.WriteLine($"{c.Name} has an id of {c.Id}");
                   }
              Console.Write("Press any key to continue");
              Console.ReadKey();
                        break;
         case ("Assign a chore"):
                        List<Chore> assignableChores = choreRepo.GetUnassignedChores();

                        foreach (Chore c in assignableChores)
                        {
                            Console.WriteLine($"{c.Name} has an id of {c.Id}");
                        }
                        Console.Write("Chore Id: ");
                        int choreId = int.Parse(Console.ReadLine());
                        List<Roommate> assignableRoomates = roommateRepo.GetAll();
                        foreach (Roommate r in assignableRoomates)
                        {
                            Console.WriteLine($"[{r.Id}] : {r.FirstName}");
                        }
                        Console.Write("Roommate Id: ");
                        int choreRoommateId = int.Parse(Console.ReadLine());
                        choreRepo.AssignChore(choreRoommateId, choreId);
                        Console.WriteLine("The chore has been assigned");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Exit"):
             runProgram = false;
             break;

            
     }
 }

        }

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
            {
                "Show all rooms",
                "Search for room",
                "Add a room",
                "Show all chores",
                "Search for a chore",
                "Add a chore",
                "See unassigned chores",
                "Search for a roommate",
                "Assign a chore",
                "Exit"
            };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }
        }
    }
}