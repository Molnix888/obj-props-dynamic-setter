using NUnit.Framework.Internal;

namespace ObjPropsDynamicSetter.Test.Unit.Extensions
{
    internal static class RandomizerExtensions
    {
        internal static char NextChar(this Randomizer randomizer) => Randomizer.DefaultStringChars[randomizer.Next(0, Randomizer.DefaultStringChars.Length)];
    }
}
