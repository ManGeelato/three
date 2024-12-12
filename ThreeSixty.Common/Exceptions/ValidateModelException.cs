using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ThreeSixty.Common.Exceptions
{
    public class ValidateModelException : ModelStateDictionary
    {

        public ValidateModelException()
        {
            Errors = new Dictionary<string, List<string>>();
        }


        public ValidateModelException(ModelStateDictionary modelState)
            : this()
        {
            foreach (string key in modelState.Keys)
            {
                var property = modelState.GetValueOrDefault(key);

                List<string> errors = Enumerable.ToList<string>(property.Errors.Select(error => error.ErrorMessage));

                Errors.Add(key, errors);
            }
        }

        public IDictionary<string, List<string>> Errors { get; }
    }
}
