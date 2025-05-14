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

            await Page.SetViewportSizeAsync(1920, 1080);

            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }

        [TearDown]
        public async Task TearDown()
        {
            var failed = TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Error
            || TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Failure;

            await MakeScreenShotIfFailedAsync(failed, ".chapternav-wrapper");
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
            var ilElements = Page.Locator(".chapternav-label");

            var count = await ilElements.CountAsync();

            for (int i = 0; i < count; i++)
                await Expect(ilElements.Nth(i)).ToBeVisibleAsync();
        }

        public async Task MakeScreenShotIfFailedAsync(bool failed, string blockToScreen)
        {
            if (failed)
            {
                await Page.WaitForSelectorAsync(blockToScreen);
                await Page.Locator(blockToScreen).ScreenshotAsync(new() { Path = $"..\\..\\..\\Logs\\Screenshots\\Fail_{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyy-dd-M--HH-mm-ss}.png" });
            }
        }
    }
}
