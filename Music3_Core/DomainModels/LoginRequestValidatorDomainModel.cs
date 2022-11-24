using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music3_Core.DomainModels
{
    public class LoginRequestValidatorDomainModel : AbstractValidator<LoginRequestDomainModel>
    {
        public LoginRequestValidatorDomainModel()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Nhập Username");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Nhập Password")
                .MinimumLength(6).WithMessage("Password tối thiểu 6 kí tự");
        }
    }
}
