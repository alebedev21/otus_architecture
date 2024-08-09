using Server.ValueTypes;

Angle angle = new();

for (int i = 0; i <= 360; i++)
{
    angle.SetValue(i);
    Console.WriteLine(angle.GetValue());
}

Console.WriteLine("Ok");
