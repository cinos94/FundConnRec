﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FundConnRec.API.Models;
using FundConnRec.Models.Models;
using FundConnRec.API.Repositories.Interfaces;
using System.Net;
using FundConnRec.API.Repositories;
using FundConnRec.Models.Exceptions;

namespace FundConnRec.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfoliosController : ControllerBase
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfoliosController(IPortfolioRepository dataRepository)
        {
            _portfolioRepository = dataRepository;
        }

        // GET: api/Portfolios
        [HttpGet]
        public IActionResult GetPortfolios()
        {
            try
            {
                IEnumerable<Portfolio> x = _portfolioRepository.GetAll();
                return Ok(x);
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET: api/Portfolios/5
        [HttpGet("{isin}")]
        public async Task<IActionResult> GetPortfolio([FromRoute] string isin, [FromQuery]DateTime date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var portfolio = await _portfolioRepository.Get(isin, date.Date);
                if (portfolio == null)
                {
                    return NotFound();
                }
                return Ok(portfolio);
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT: api/Portfolios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPortfolio([FromRoute] int id, [FromBody] Portfolio portfolio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != portfolio.PortfolioId)
            {
                return BadRequest();
            }
            try
            { 
                Portfolio portfolioInDB = await _portfolioRepository.Get(id);
                _portfolioRepository.Update(portfolioInDB, portfolio);
            }
            catch (DbUpdateConcurrencyException)
            {
                //log
                return StatusCode((int)HttpStatusCode.InternalServerError, "Unknown error");
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return NoContent();
        }

        // POST: api/Portfolios
        [HttpPost]
        public async Task<IActionResult> PostPortfolio([FromBody] Portfolio portfolio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _portfolioRepository.Add(portfolio);
            }
            catch(ChangeConflictException ex)
            {
                return StatusCode((int)HttpStatusCode.Conflict, ex.Message);
            }
            catch(ToleranceOfOutRangeException ex)
            {
                return StatusCode((int)HttpStatusCode.UnprocessableEntity, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return CreatedAtAction("GetPortfolio", new { isin = portfolio.ISIN, date = portfolio.Date }, portfolio);
        }

        // DELETE: api/Portfolios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePortfolio([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Portfolio portfolio = await _portfolioRepository.Get(id);
                await this._portfolioRepository.Delete(portfolio);
                return Ok(id);
            }
            catch(ArgumentException)
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}