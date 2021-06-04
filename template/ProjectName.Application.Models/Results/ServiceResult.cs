using System.Collections.Generic;
using System.Linq;

namespace ProjectName.Application.Models
{
    public class ServiceResult
    {
        internal ServiceResult(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }

        public static ServiceResult Success()
        {
            return new ServiceResult(true, System.Array.Empty<string>());
        }

        public static ServiceResult Failure(IEnumerable<string> errors)
        {
            return new ServiceResult(false, errors);
        }
    }
}
