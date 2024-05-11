﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MS0XLT_HFT_2023241.Endpoint.Services;
using MS0XLT_HFT_2023241.Logic;
using MS0XLT_HFT_2023241.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MS0XLT_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        IGradeLogic logic;
        IHubContext<SignalRHub> hub;

        public GradeController(IGradeLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        // GET: api/<GradeController>
        [HttpGet]
        public IEnumerable<Grade> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<GradeController>/5
        [HttpGet("{id}")]
        public Grade Get(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<GradeController>
        [HttpPost]
        public void Create([FromBody] Grade value)
        {
            this.logic.Create(value);
            hub.Clients.All.SendAsync("GradeCreated", value);
        }

        // PUT api/<GradeController>/5
        [HttpPut("{id}")]
        public void Update([FromBody] Grade value)
        {
            this.logic.Update(value);

            hub.Clients.All.SendAsync("GradeUpdated", value);
        }

        // DELETE api/<GradeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var gradeToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            hub.Clients.All.SendAsync("GradeDeleted", gradeToDelete);
        }
    }
}
