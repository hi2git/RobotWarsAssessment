using RobotWars;

Console.WriteLine("Please enter the upper-right coordinates of the arena:");
var size = Console.ReadLine();

Console.WriteLine("Please enter robot initial coordinates");
var startPos = Console.ReadLine();

Console.WriteLine("Please enter robot instructions");
var instructionLine = Console.ReadLine();

try {
	var result = Battle.Run(size, startPos, instructionLine);
	Console.WriteLine(result);
}
catch (Exception ex) {
	Console.WriteLine(ex.ToString());
}

Console.Write("\r\nPlease press Enter to exit");
Console.ReadLine();