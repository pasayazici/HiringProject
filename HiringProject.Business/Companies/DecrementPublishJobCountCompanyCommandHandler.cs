﻿using HiringProject.Data.Models;
using HiringProject.Data.Repositories;
using HiringProject.Exceptions;
using HiringProject.Model.Commands.Companies;
using HiringProject.Model.Controllers.Companies.Responses;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HiringProject.Business.Companies
{
    public class DecrementPublishJobCountCompanyCommandHandler : IRequestHandler<DecrementPublishJobCountCompanyCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkRepository _unitOfWork;

        public DecrementPublishJobCountCompanyCommandHandler(IMapper mapper, IUnitOfWorkRepository unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DecrementPublishJobCountCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(request.Id);
            if (company == null)
            {
                throw new DataNotFoundException(nameof(company.Id), company.Id.ToString());
            }

            if (company.RemainPublishJobCount < 1)
            {
                throw new RemainPublishJobCountException(nameof(company.Id), company.Id.ToString());
            }
            company.RemainPublishJobCount--;

            var result = await _unitOfWork.CompanyRepository.UpdateAsync(company.Id, company);

            return true;
        }
    }
}
