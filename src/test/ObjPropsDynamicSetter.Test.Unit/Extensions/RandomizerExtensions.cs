using NUnit.Framework.Internal;

namespace ObjPropsDynamicSetter.Test.Unit.Extensions
{
    public static class RandomizerExtensions
    {
        public static char NextChar(this Randomizer randomizer) => Randomizer.DefaultStringChars[randomizer?.Next(0, Randomizer.DefaultStringChars.Length) ?? 0];
    }
}
