using Shouldly;

namespace the80by20.Sandbox;

// TODO do design patterns tests fpor exercising their usage, include spcification desgin pattern

public class ExtendableButClosedToModificationDoneWithDelegatePassedAsArgument
{
    [Fact]
    void Do()
    {
        var client = new StringBuilderExtendableClient();
        var res = client.Do("base");

        res.ShouldBe("base extended");
    }
}

public abstract class AStringBuilderExtendableClient
{
    public string Do(string baseString)
    {
        var res = StringBuilderExtendable.Do(baseString, Extend);
        return res;
    }

    protected virtual string Extend(string input)
    {
        return input;
    }
}

public static class StringBuilderExtendable
{

    public static string Do(string baseString, Func<string, string> extends)
    {
        string extendedBase = extends?.Invoke(baseString);

        return extendedBase;
    }
}

public class StringBuilderExtendableClient : AStringBuilderExtendableClient
{
    protected override string Extend(string input)
    {
        return $"{input} extended";
    }
}