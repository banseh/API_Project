using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketSys.BL.Dtos.Project;
using BugTicketSys.DAL;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

namespace BugTicketSys.BL
{
    public class ProjectDtoValidator : AbstractValidator<ProjectReadDto>
    {
        private readonly IUnitOfWork _unitofwork;
        public ProjectDtoValidator(IConfiguration configuration , IUnitOfWork _unitOfWork)
        {
            RuleFor(d => d.Name)
                .NotEmpty()
                .WithMessage("Name can't be empty")
                .WithErrorCode("ERR-01")
                .MaximumLength(100)
                .WithMessage("Name must be less than 100 characters.");

            RuleFor(d => d.Title)
                .NotEmpty()
                .WithMessage("Title can't be empty")
                .WithErrorCode("ERR-02")
                .MaximumLength(200);
        }

        // Optional async uniqueness check
        private async Task<bool> check_if_nameunique(string name, CancellationToken token)
        {
            // TODO: Inject IUnitOfWork or service to check database
            var proect= await _unitofwork.ProjectRepo.GetAllAsync();
            return true; // Placeholder
        }
    }
}
