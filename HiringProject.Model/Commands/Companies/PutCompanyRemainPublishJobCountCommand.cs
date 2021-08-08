﻿using HiringProject.Model.Controllers.Companies.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringProject.Model.Commands.Companies
{
    public class PutCompanyRemainPublishJobCountCommand : IRequest<CompanyInfoResponse>
    {
        public string Id { get; set; }
        public int RemainPublishJobCount { get; set; } 
    }
}