using System.Collections.Specialized;

namespace AdventOfCode2023.December_15;

public class Box
{
    private readonly int _boxId;

    public Box(int boxId) => _boxId = boxId;

    private readonly OrderedDictionary _lenses = new();

    public void RemoveLens(string label) => _lenses.Remove(label);

    public void AddLens(string label, int focalPoint)
    {
        if (_lenses.Contains(label) is false)
            _lenses.Add(label, focalPoint);
        else
            _lenses[label] = focalPoint;
    }

    public int GetLensFocusPowerSum()
    {
        var focalPoints = new int[_lenses.Count];
        _lenses.Values.CopyTo(focalPoints, 0);

        var sum = 0;
        for (var index = 0; index < _lenses.Count; index++)
        {
            var focalPoint = focalPoints[index];

            sum += (_boxId + 1) * (index + 1) * focalPoint;
        }

        return sum;
    }
}