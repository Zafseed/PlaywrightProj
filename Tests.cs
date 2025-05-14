using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightProj
{
    public class Tests : PageTest
    {
        [SetUp]
        public async Task SetUp()
        {
            await Page.GotoAsync("https://www.apple.com/mac/");
        }

        [Test]
        public async Task NamesIsMathing()
        {
            var productNameList = new List<string>()
            {
                "MacBook Air",
                "MacBook Pro",
                "iMac",
                "Mac mini",
                "Mac Studio",
                "Mac Pro",
                "Help Me Choose",
                "Compare",
                "Displays",
                "Accessories",
                "Sequoia",
                "Shop Mac"
            };

            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

            var ilElements = Page.Locator(".chapternav-label");

            var count = await ilElements.CountAsync();

            if (count != productNameList.Count)
                throw new Exception("Количество элементов на странице не совпадает с ожидаемым списком");

            for (int i = 0; i < count; i++)
                await Expect(ilElements.Nth(i)).ToContainTextAsync(productNameList[i]);
        }

        [Test]
        public async Task ContentBlockIsVisible()
        {
            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

            var ilElements = Page.Locator(".chapternav-label");

            var count = await ilElements.CountAsync();

            for (int i = 0; i < count; i++)
                await Expect(ilElements.Nth(i)).ToBeVisibleAsync();
        }
    }
}
