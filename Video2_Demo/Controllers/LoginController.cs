﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Video2_Demo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        [HttpGet]
        public void TestjsonpForVue(string callback)
        {
            TokenModelJwt tokenModelJwt = new TokenModelJwt()
            {
                Role = "jsonp",
                Uid = 1,
                Work = "dsdf"
            };
            //string call = "({" + response + "})";
            string response = string.Format("\"name\":\"{0}\"", "zhagnsan");
            var modlestr = JsonConvert.SerializeObject(tokenModelJwt);
            string call = callback + "(" + modlestr + ")";
            Response.WriteAsync(call);
        }

        // GET: api/Login
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public ActionResult TestProxyForVue()
        {
            TokenModelJwt tokenModelJwt = new TokenModelJwt()
            {
                Role = "proxy",
                Uid = 2,
                Work = "dsdf"
            };
            return Ok(tokenModelJwt);
        }


        [HttpPut]
        [Route("/apicors/Login/TestCORSForVue")]
        public ActionResult TestCORSForVue()
        {
            TokenModelJwt tokenModelJwt = new TokenModelJwt()
            {
                Role = "cors",
                Uid = 3,
                Work = "dsdf"
            };
            return Ok(tokenModelJwt);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class TokenModelJwt
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Uid { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 职能
        /// </summary>
        public string Work { get; set; }

    }

}
