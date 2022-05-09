﻿using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyEmployees.Controllers
{
    //[Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public CompaniesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        // If you want to route specific index
        // But you have to comment [Route("api/companies")] 
        [Route("api/companies/getcompanies")]
        public IActionResult GetCompanies()
        {
            try
            {
                var companies = _repository.Company.GetAllCompanies(trackChanges: false);

                var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

                //// this is just mapping with manually
                //var companiesDto = companies.Select(c => new CompanyDto
                //{
                //    Id = c.Id,
                //    Name = c.Name,
                //    FullAddress = string.Join(' ', c.Address, c.Country)
                //}).ToList();

                return Ok(companiesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetCompanies)} action { ex }");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
