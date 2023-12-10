namespace AdventOfCode2023.December_10;

public interface IPipe
{
    IPipe Next(char[,] pipes);
}

public class DownPipe : IPipe
{
    private readonly (int, int) _position;

    public DownPipe((int, int) position)
    {
        _position = position;
    }
    
    public IPipe Next(char[,] pipes)
    {
        var nextPosition = (_position.Item1 + 1, _position.Item2);
        
        var nextPipe = pipes[_position.Item1 + 1, _position.Item2];

        return nextPipe switch
        {
            '|' => new DownPipe(nextPosition),
            'L' => new DownLPipe(nextPosition),
            'J' => new DownJPipe(nextPosition),
            'S' => new Start(),
            _ => new DeadEnd()
        };
    }
}

public class UpPipe : IPipe
{
    private readonly (int, int) _position;

    public UpPipe((int, int) position)
    {
        _position = position;
    }
    
    public IPipe Next(char[,] pipes)
    {
        var nextPosition = (_position.Item1 - 1, _position.Item2);
        
        var nextPipe = pipes[_position.Item1 - 1, _position.Item2];

        return nextPipe switch
        {
            '|' => new UpPipe(nextPosition),
            '7' => new Up7Pipe(nextPosition),
            'F' => new UpFPipe(nextPosition),
            'S' => new Start(),
            _ => new DeadEnd()
        };
    }
}

public class DownLPipe : IPipe
{
    private readonly (int, int) _position;

    public DownLPipe((int, int) position)
    {
        _position = position;
    }
    
    public IPipe Next(char[,] pipes)
    {
        var nextPosition = (_position.Item1, _position.Item2 + 1);
        
        var nextPipe = pipes[_position.Item1, _position.Item2 + 1];

        return nextPipe switch
        {
            '-' => new RightHorizontalPipe(nextPosition),
            '7' => new Right7Pipe(nextPosition),
            'J' => new RightJPipe(nextPosition),
            'S' => new Start(),
            _ => new DeadEnd()
        };
    }
}

public class LeftLPipe : IPipe
{
    private readonly (int, int) _position;

    public LeftLPipe((int, int) position)
    {
        _position = position;
    }
    
    public IPipe Next(char[,] pipes)
    {
        var nextPosition = (_position.Item1 - 1, _position.Item2);
        
        var nextPipe = pipes[_position.Item1 - 1, _position.Item2];

        return nextPipe switch
        {
            '|' => new RightHorizontalPipe(nextPosition),
            '7' => new Up7Pipe(nextPosition),
            'F' => new UpFPipe(nextPosition),
            'S' => new Start(),
            _ => new DeadEnd()
        };
    }
}

public class DownJPipe : IPipe
{
    private readonly (int, int) _position;

    public DownJPipe((int, int) position)
    {
        _position = position;
    }
    
    public IPipe Next(char[,] pipes)
    {
        var nextPosition = (_position.Item1, _position.Item2 - 1);
        
        var nextPipe = pipes[_position.Item1, _position.Item2 - 1];

        return nextPipe switch
        {
            '-' => new LeftHorizontalPipe(nextPosition),
            'L' => new LeftLPipe(nextPosition),
            'F' => new LeftFPipe(nextPosition),
            'S' => new Start(),
            _ => new DeadEnd()
        };
    }
}

public class RightJPipe : IPipe
{
    private readonly (int, int) _position;

    public RightJPipe((int, int) position)
    {
        _position = position;
    }
    
    public IPipe Next(char[,] pipes)
    {
        var nextPosition = (_position.Item1 - 1, _position.Item2);
        
        var nextPipe = pipes[_position.Item1 - 1, _position.Item2];

        return nextPipe switch
        {
            '|' => new UpPipe(nextPosition),
            '7' => new Up7Pipe(nextPosition),
            'F' => new Up7Pipe(nextPosition),
            'S' => new Start(),
            _ => new DeadEnd()
        };
    }
}

public class Up7Pipe : IPipe
{
    private readonly (int, int) _position;

    public Up7Pipe((int, int) position)
    {
        _position = position;
    }
    
    public IPipe Next(char[,] pipes)
    {
        var nextPosition = (_position.Item1, _position.Item2 - 1);
        
        var nextPipe = pipes[_position.Item1, _position.Item2 - 1];

        return nextPipe switch
        {
            '-' => new LeftHorizontalPipe(nextPosition),
            'L' => new LeftLPipe(nextPosition),
            'F' => new LeftFPipe(nextPosition),
            'S' => new Start(),
            _ => new DeadEnd()
        };
    }
}

public class Right7Pipe : IPipe
{
    private readonly (int, int) _position;

    public Right7Pipe((int, int) position)
    {
        _position = position;
    }
    
    public IPipe Next(char[,] pipes)
    {
        var nextPosition = (_position.Item1 + 1, _position.Item2);
        
        var nextPipe = pipes[_position.Item1 + 1, _position.Item2];

        return nextPipe switch
        {
            '|' => new DownPipe(nextPosition),
            'L' => new DownLPipe(nextPosition),
            'J' => new DownJPipe(nextPosition),
            'S' => new Start(),
            _ => new DeadEnd()
        };
    }
}

public class UpFPipe : IPipe
{
    private readonly (int, int) _position;

    public UpFPipe((int, int) position)
    {
        _position = position;
    }
    
    public IPipe Next(char[,] pipes)
    {
        var nextPosition = (_position.Item1, _position.Item2 + 1);
        
        var nextPipe = pipes[_position.Item1, _position.Item2 + 1];

        return nextPipe switch
        {
            '-' => new RightHorizontalPipe(nextPosition),
            '7' => new Right7Pipe(nextPosition),
            'J' => new RightJPipe(nextPosition),
            'S' => new Start(),
            _ => new DeadEnd()
        };
    }
}

public class LeftFPipe : IPipe
{
    private readonly (int, int) _position;

    public LeftFPipe((int, int) position)
    {
        _position = position;
    }
    
    public IPipe Next(char[,] pipes)
    {
        var nextPosition = (_position.Item1 + 1, _position.Item2);
        
        var nextPipe = pipes[_position.Item1 + 1, _position.Item2];

        return nextPipe switch
        {
            '|' => new DownPipe(nextPosition),
            'L' => new DownLPipe(nextPosition),
            'J' => new DownJPipe(nextPosition),
            'S' => new Start(),
            _ => new DeadEnd()
        };
    }
}

public class LeftHorizontalPipe : IPipe
{
    private readonly (int, int) _position;

    public LeftHorizontalPipe((int, int) position)
    {
        _position = position;
    }
    
    public IPipe Next(char[,] pipes)
    {
        var nextPosition = (_position.Item1, _position.Item2 - 1);
        
        var nextPipe = pipes[_position.Item1, _position.Item2 - 1];

        return nextPipe switch
        {
            '_' => new LeftHorizontalPipe(nextPosition),
            'L' => new LeftLPipe(nextPosition),
            'F' => new LeftFPipe(nextPosition),
            'S' => new Start(),
            _ => new DeadEnd()
        };
    }
}

public class RightHorizontalPipe : IPipe
{
    private readonly (int, int) _position;

    public RightHorizontalPipe((int, int) position)
    {
        _position = position;
    }
    
    public IPipe Next(char[,] pipes)
    {
        var nextPosition = (_position.Item1, _position.Item2 + 1);
        
        var nextPipe = pipes[_position.Item1, _position.Item2 + 1];

        return nextPipe switch
        {
            '_' => new RightHorizontalPipe(nextPosition),
            '7' => new Right7Pipe(nextPosition),
            'J' => new RightJPipe(nextPosition),
            'S' => new Start(),
            _ => new DeadEnd()
        };
    }
}

public class DeadEnd : IPipe
{
    public IPipe Next(char[,] pipes) => this;
}

public class Start : IPipe
{
    public IPipe Next(char[,] pipes)
    {
        throw new NotImplementedException();
    }
}