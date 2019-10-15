using System;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Utils
{
    public static class StringExtensions
    {
        public static string[] SplitByNewLineEndAndRemoveWindowsLineEnds(this string value)
        {
            var res = value.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = res[i].TrimEnd('\r');
            }
            return res;
        }
    }
}
