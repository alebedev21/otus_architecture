using Server;
using Server.Adapters;
using Server.Commands;
using Server.Entities;
using Server.Interfaces;

Starship starship = new()
{
    Position = new(12,5),
    Velocity = new(-7,3)
};

IMovable adapter = new MovableAdapter(starship);
MoveCommand moveCommand = new(adapter);

Core core = new();
core.AddCommand(moveCommand);
core.Start();

Console.WriteLine("Ok");
