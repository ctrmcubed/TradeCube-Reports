using System;

namespace TradeCube_Reports.Exceptions
{
    public class RecipeException : Exception
    {
        public RecipeException(string message) : base(message)
        {
        }
    }
}
