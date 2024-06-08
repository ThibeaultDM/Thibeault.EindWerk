int id = 99999;
string trackingNumber = "K";

trackingNumber += id.ToString().PadLeft(4, '0');


Console.WriteLine(trackingNumber);
Console.ReadLine();