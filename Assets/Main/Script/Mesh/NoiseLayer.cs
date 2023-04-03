
public class NoiseLayer 
{
    public Noise[] noises;
    public int count;
    public int seed;

    public NoiseLayer(int _count, int _seed)
    {
        seed = _seed;
        noises = new Noise[_count];
        count = _count;
        for(int i = 0; i < count; i++)
        {
            noises[i] = new Noise(seed+i);
        }
    }
}
