using Cr.ArgParse.Actions;
using NUnit.Framework;

namespace Cr.ArgParse.Tests
{
    [TestFixture] public class ActionContainerTest : BaseTest
    {
        [Test] public void AddOneOptionalArgument()
        {
            var actionContainer = new ActionContainer();
            Action res = actionContainer.AddArgument(new Argument("-e", "--example"));

            CollectionAssert.AreEquivalent(new[] {"-e", "--example"},
                ((IActionContainer) actionContainer).OptionStringActions.Keys);

            Assert.That(res, Is.InstanceOf<StoreAction>());
            Asserter.AreEqual(
                new StoreAction(new Argument("-e", "--example") {Destination = "example"}, actionContainer)
                ,
                res);
        }

        [Test] public void AddOneOptionalArgumentWithActionName()
        {
            var actionContainer = new ActionContainer();
            var argument = new Argument("-e", "--example")
            {
                ActionName = "count"
            };
            var argumentCopy = new Argument(argument);
            argumentCopy.Destination = "example";
            Action res =
                actionContainer.AddArgument(argument);

            CollectionAssert.AreEquivalent(new[] {"-e", "--example"}, argument.OptionStrings);

            Assert.That(res, Is.InstanceOf<CountAction>());
            Asserter.AreEqual(
                new CountAction(argumentCopy, actionContainer),
                res);
        }

        [Test] public void CreateDefault()
        {
            var actionContainer = new ActionContainer();
            CollectionAssert.AreEqual(new[] {"--", "-", "/"}, actionContainer.Prefixes);
        }
    }
}