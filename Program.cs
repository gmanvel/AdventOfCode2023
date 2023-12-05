using AdventOfCode2023;
using AdventOfCode2023.December_4;
using System;

var input = File.ReadAllLines(
    Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "December 5",
        "Dec5_Input.txt"));

var indexOfColon = input[0].IndexOf(':');
var data = input[0].AsSpan().Slice(indexOfColon + 1);

var seeds = 
    //data.ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries)
    //.Where((_, index) => index % 2 == 0)
    //.Select(long.Parse)
    //.ToArray();
    new[] {(79, 14), (55, 13)};
    //data.ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

var seedToSoilInput = input.Skip(3).Take(24).ToArray();

var seedToSoil = 
    GetMap(new[] { "50 98 2", "52 50 48" });
    //GetMap(seedToSoilInput);

var soilToFertilizerInput = input.Skip(29).Take(31).ToArray();

var soilToFertilizer = 
    GetMap(new[] { "0 15 37", "37 52 2", "39 0 15" });
    //GetMap(soilToFertilizerInput);

var fertilizerToWaterInput = input.Skip(62).Take(10).ToArray();

var fertilizerToWater = 
    GetMap(new[] { "49 53 8", "0 11 42", "42 0 7", "57 7 4" });
    //GetMap(fertilizerToWaterInput);

var waterToLightInput = input.Skip(74).Take(27).ToArray();

var waterToLight = 
    GetMap(new[] { "88 18 7", "18 25 70" });
    //GetMap(waterToLightInput);

var lightToTemperatureInput = input.Skip(103).Take(11).ToArray();

var lightToTemperature = 
    GetMap(new[] { "45 77 23", "81 45 19", "68 64 13" });
    //GetMap(lightToTemperatureInput);

var temperatureToHumidityInput = input.Skip(116).Take(13).ToArray();

var temperatureToHumidity = 
    GetMap(new[] { "0 69 1", "1 0 69" });
    //GetMap(temperatureToHumidityInput);

var humidityToLocationInput = input.Skip(131).Take(8).ToArray();

var humidityToLocation = 
    GetMap(new[] { "60 56 37", "56 93 4" });
    //GetMap(humidityToLocationInput);

//var bigSeeds = new[]
//{
//    Enumerable.Range(364807853, 408612163),
//    Enumerable.Range(302918330, 20208251),
//    Enumerable.Range(1499552892, 200291842),
//    Enumerable.Range(3284226943, 16030044),
//    Enumerable.Range(364807853, 408612163),
//    Enumerable.Range(364807853, 408612163),
//    Enumerable.Range(364807853, 408612163),
//    Enumerable.Range(364807853, 408612163),
//    Enumerable.Range(364807853, 408612163),
//    Enumerable.Range(364807853, 408612163)
//};

var minLocation = long.MaxValue;

foreach (var seed in seeds)
{
    var soil = MapSoil(seedToSoil, seed);

    var fertilizer = MapValue(soilToFertilizer, soil);
    var water = MapValue(fertilizerToWater, fertilizer);
    var light = MapValue(waterToLight, water);
    var temperature = MapValue(lightToTemperature, light);
    var humidity = MapValue(temperatureToHumidity, temperature);
    var location = MapValue(humidityToLocation, humidity);

    minLocation = Math.Min(minLocation, location);

    Console.WriteLine($"Seed {seed}, soil {soil}, fertilizer {fertilizer}, water {water}, light {light}, temperature {temperature}, humidity {humidity}, location {location}.");
}

Console.WriteLine(minLocation);

//var minLocation = long.MaxValue;

//foreach (var seed in seeds)
//{
//    var soil = MapValue(seedToSoil, seed);

//    var fertilizer = MapValue(soilToFertilizer, soil);
//    var water = MapValue(fertilizerToWater, fertilizer);
//    var light = MapValue(waterToLight, water);
//    var temperature = MapValue(lightToTemperature, light);
//    var humidity = MapValue(temperatureToHumidity, temperature);
//    var location = MapValue(humidityToLocation, humidity);

//    minLocation = Math.Min(minLocation, location);

//    //Console.WriteLine($"Seed {seed}, soil {soil}, fertilizer {fertilizer}, water {water}, light {light}, temperature {temperature}, humidity {humidity}, location {location}.");
//}

//Console.WriteLine(minLocation);

//var minLocation2 = long.MaxValue;
//Parallel.For(364807853, (364807853 + 408612163),
//    seed =>
//    {
//        var soil = MapValue(seedToSoil, seed);

//        var fertilizer = MapValue(soilToFertilizer, soil);
//        var water = MapValue(fertilizerToWater, fertilizer);
//        var light = MapValue(waterToLight, water);
//        var temperature = MapValue(lightToTemperature, light);
//        var humidity = MapValue(temperatureToHumidity, temperature);
//        var location = MapValue(humidityToLocation, humidity);

//        Console.WriteLine(
//            $"Seed {seed}, soil {soil}, fertilizer {fertilizer}, water {water}, light {light}, temperature {temperature}, humidity {humidity}, location {location}.");
//    });

//foreach (var seed in Enumerable.Range())
//{
//    var soil = MapValue(seedToSoil, seed);

//    var fertilizer = MapValue(soilToFertilizer, soil);
//    var water = MapValue(fertilizerToWater, fertilizer);
//    var light = MapValue(waterToLight, water);
//    var temperature = MapValue(lightToTemperature, light);
//    var humidity = MapValue(temperatureToHumidity, temperature);
//    var location = MapValue(humidityToLocation, humidity);

//    minLocation = Math.Min(minLocation, location);

//    Console.WriteLine($"Seed {seed}, soil {soil}, fertilizer {fertilizer}, water {water}, light {light}, temperature {temperature}, humidity {humidity}, location {location}.");
//}



long MapValue(List<Input> input, long value)
{
    var map = input.FirstOrDefault(x => x.Source <= value && value <= x.Source + x.Length - 1);

    if (map is null)
        return value;

    var offset = value - map.Source;

    var mappedValue = map.Destination + offset;

    return mappedValue;
}

long MapSoil(List<Input> input, (long value, long length) seed)
{
    var (value, length) = seed;

    var map =
        input
            .Where(inp => inp.Source <= (value + length) && value <= inp.Source + inp.Length)
            .MinBy(inp => inp.Destination);

    if (map is null)
        return value;

    var intersectionPoint = Math.Max(map.Source, value);

    var offset = intersectionPoint - map.Source;

    var mappedValue = map.Destination + offset;

    return mappedValue;
}

//var seedToSoil = GetMap(@"
//50 98 2
//52 50 48");

//var soilToFertilizer = GetMap(@"
//0 15 37
//37 52 2
//39 0 15");

//var fertilizerToWater = GetMap(@"
//49 53 8
//0 11 42
//42 0 7
//57 7 4");

//var waterToLight = GetMap(@"
//88 18 7
//18 25 70");

//var lightToTemperature = GetMap(@"
//45 77 23
//81 45 19
//68 64 13");

//var temperatureToHumidity = GetMap(@"
//0 69 1
//1 0 69");

//var humidityToLocation = GetMap(@"
//60 56 37
//56 93 4");

//int[] seeds = {79, 14, 55, 13};

//for (int index = 0; index < seeds.Length; index++)
//{
//    var soil = seedToSoil.GetValueOrSelf(seeds[index]);
//    var fertilizer = soilToFertilizer.GetValueOrSelf(soil);
//    var water = fertilizerToWater.GetValueOrSelf(fertilizer);
//    var light = waterToLight.GetValueOrSelf(water);
//    var temperature = lightToTemperature.GetValueOrSelf(light);
//    var humidity = temperatureToHumidity.GetValueOrSelf(temperature);
//    var location = humidityToLocation.GetValueOrSelf(humidity);

//    Console.WriteLine($"Seed {seeds[index]}, soil {soil}, fertilizer {fertilizer}, water {water}, light {light}, temperature {temperature}, humidity {humidity}, location {location}.");
//}



//var text = new[] {"50 98 2", "52 50 48"};
//var ns = text[0].Split(' ').Select(int.Parse).ToArray();
//var (destinationStart, sourceStart, length) = (ns[0], ns[1], ns[2]);

//var seedToSoil = new Dictionary<int, int>();
//for (int i = 0; i < length; i++)
//{
//    seedToSoil.Add(sourceStart + i, destinationStart + i);
//}

//ns = text[1].Split(' ').Select(int.Parse).ToArray();
//(destinationStart, sourceStart, length) = (ns[0], ns[1], ns[2]);

//for (int i = 0; i < length; i++)
//{
//    seedToSoil.Add(sourceStart + i, destinationStart + i);
//}

//Console.WriteLine($"{79}={seedToSoil[79]}");
//Console.WriteLine($"{14}={seedToSoil[14]}");
//Console.WriteLine($"{55}={seedToSoil[55]}");
//Console.WriteLine($"{13}={seedToSoil[13]}");


//static Dictionary<int, int> GetMap(string text)
//{
//    var map = new Dictionary<int, int>();

//    foreach (var line in text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
//    {
//        var ns = line.Split(' ').Select(int.Parse).ToArray();
//        var (destinationStart, sourceStart, length) = (ns[0], ns[1], ns[2]);

//        for (int i = 0; i < length; i++)
//        {
//            map.Add(sourceStart + i, destinationStart + i);
//        }
//    }

//    return map;
//}

//static Dictionary<long, long> GetMap(IEnumerable<string> lines)
//{
//    var map = new Dictionary<long, long>();

//    foreach (var line in lines)
//    {
//        var ns = line.Split(' ').Select(long.Parse).ToArray();
//        var (destinationStart, sourceStart, length) = (ns[0], ns[1], ns[2]);

//        for (int i = 0; i < length; i++)
//        {
//            map.Add(sourceStart + i, destinationStart + i);
//        }
//    }

//    return map;
//}

static List<Input> GetMap(IEnumerable<string> lines)
{
    var list = new List<Input>(lines.Count());

    list.AddRange(
        lines.Select(line =>
        {
            var ns = line.Split(' ').Select(long.Parse).ToArray();
            var (destinationStart, sourceStart, length) = (ns[0], ns[1], ns[2]);
            return new Input(sourceStart, destinationStart, length);
        }));

    return list;
}

record Input(long Source, long Destination, long Length);
