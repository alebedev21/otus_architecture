using Server;
using Server.Adapters;
using Server.Commands;
using Server.Entities;
using Server.Interfaces;

UnknownPositionObject obj = new();
obj.SetPosition(new(12,5));
obj.SetVelocity(new(-7,3));

IMovable adapter = new MovableAdapter(obj);
MoveCommand moveCommand = new(adapter);

Core core = new();
core.AddCommand(moveCommand);
core.Start();

Console.WriteLine("Ok");
