public class ConfigurationBuilder
{
    private Configuration _configuration1 = new();
    private Configuration _configuration2 = new();

    public ConfigurationBuilder Configure1(Action<Configuration> c)
    {
        c?.Invoke(_configuration1);
        return this;
    }
    public ConfigurationBuilder Configure2(Action<Configuration> c)
    {
        c?.Invoke(_configuration2);
        return this;
    }

    public Configuration Build()
    {
        return new Configuration()
        {
            Settings1 = _configuration1.Settings1 + _configuration2.Settings1,
            Settings2 = _configuration1.Settings2 + _configuration2.Settings2,
        };
    }

}

class Client
{
    void Do()
    {
        var b = new ConfigurationBuilder()
            .Configure1(c =>
            {
                c.Settings1 = "s1-1";
                c.Settings2 = "s2-1";
            })
            .Configure2(c =>
            {
                c.Settings1 = "s2-2";
                c.Settings2 = "s2-2";
            });

        var res = b.Build();
    }
}

public class Configuration
{
    public string Settings1 { get; set; }
    public string Settings2 { get; set; }
}