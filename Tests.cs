using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework.Internal;

namespace PlaywrightProj
{
    public class Test : PageTest
    {
        private readonly Logger logger = new("D:\\Proj\\PlaywrightProj\\Logs\\log.txt");

        private readonly string[] BlockNames = ["MacBook Air", "MacBook Pro", "iMac", "Mac mini", 
                                                "Mac Studio", "Mac Pro", "Help Me Choose", "Compare",
                                                "Displays", "Accessories", "Sequoia", "Shop Mac"];

        private int index = 0;

        [SetUp]
        public async Task SetUp()
        {
            await Page.GotoAsync("https://www.apple.com/mac/");

            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

            await Page.SetViewportSizeAsync(1920, 1080);

            logger.Log($"\t\tTest {TestContext.CurrentContext.Test.Name} Starts");
        }

        [TearDown]
        public async Task TearDown()
        {
            bool isFailed = TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Error
            || TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Failure;

            if (isFailed)
            {
                logger.Log($"Block {BlockNames[index]} - Failed");

                await MakeScreenShotAsync(".chapternav-wrapper");
            }
            logger.Log($"\t\tTest {TestContext.CurrentContext.Test.Name} Ends");
        }


        [Test]
        public async Task ContentBlockIsVisible()
        {
            var liElements = await Page.Locator(".chapternav-items")
                                       .Locator("li")
                                       .AllAsync();

            foreach (var item in liElements)
            {
                await Expect(item).ToBeVisibleAsync();
                await Expect(item.Locator(".chapternav-label")).ToBeVisibleAsync();

                logger.Log($"Block {BlockNames[index]} - Passed");

                index++;
            }
        }

        public async Task MakeScreenShotAsync(string blockToScreen)
        {
            await Page.WaitForSelectorAsync(blockToScreen);
            await Page.Locator(blockToScreen).ScreenshotAsync(new() { Path = $"..\\..\\..\\Logs\\Screenshots\\Fail_{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyy-dd-M--HH-mm-ss}.png" });
        }
    }
}
