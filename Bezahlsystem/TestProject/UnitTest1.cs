using Bunit;
using OAOPS.Client.Pages;

namespace TestProject
{
    public class UnitTest1 : TestContext
    {
        [Fact]
        public void CounterShouldIncrementWhenClicked()
        {
            var cut = RenderComponent<Counter>();
            
            cut.Find("button").Click();

            cut.Find("p").MarkupMatches("<p role=\"status\">Current count: 1</p>");

            cut.Find("button").Click();

            cut.Find("p").MarkupMatches("<p role=\"status\">Current count: 2</p>");
        }

        [Fact]
        public void CounterShouldIncrementByValueWhenClicked()
        {
            int value = 2;
            var cut = RenderComponent<Counter>(parameters => parameters.Add(p => p.Value, value));

            cut.Find("button").Click();

            cut.Find("p").MarkupMatches($"<p role=\"status\">Current count: {value}</p>");
        }

    }
}