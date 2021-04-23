using System.ComponentModel.DataAnnotations;
using System.IO;

namespace FluentDeploy.Commands.Validation
{
    public class ReturnCodeCommandValidator : ICommandExecutionValidator
    {
        private int _refRetCode;

        public ReturnCodeCommandValidator(int refRetCode)
        {
            _refRetCode = refRetCode;
        }

        public void Validate(int retCode, Stream stdOut)
        {
            if (retCode != _refRetCode)
            {
                throw new ValidationException($"The return code is: {retCode} but should be {_refRetCode}");
            }
        }
    }
}