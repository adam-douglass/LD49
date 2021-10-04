using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Randwhich
{
    public System.Random _rand = new System.Random();
    public List<int> previousRandomNumbers;
    public int numberOfPrevious = 4;
        
    public Randwhich()
    {
        previousRandomNumbers = new List<int>();
    }

    public int GetRandom(int maxExclusive)
    {
        int index = -1;
        do
        {
            index = _rand.Next(maxExclusive);
        } while (previousRandomNumbers.Contains(index));

        if (previousRandomNumbers.Count >= Math.Min(numberOfPrevious, maxExclusive - 2))
        {
            previousRandomNumbers.RemoveAt(0);
        }
        previousRandomNumbers.Add(index);
        return index;
    }
}
