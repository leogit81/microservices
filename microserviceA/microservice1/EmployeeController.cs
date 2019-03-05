using NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace microservice1
{
    [RoutePrefix("api/employee")]
    public class EmployeeController: ApiController
    {
        private readonly ISession _session;

        public EmployeeController(ISession sess)
        {
            _session = sess;
        }

        [HttpGet]
        [Route("")]
        // GET api/values 
        public IEnumerable<Employee> Get()
        {
            return _session.Query<Employee>().ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public Employee Get(long id)
        {
            return _session.Query<Employee>().FirstOrDefault(x => x.Id == id);
        }

        [HttpPost]
        [Route("")]
        public Employee Post([FromBody]Employee value)
        {
            object idObj = _session.Save(value);
            long newId;
            if (long.TryParse(idObj.ToString(), out newId))
            {
                value.Id = newId;
            }
            return value;
        }

        [HttpPut]
        [Route("")]
        public void Put([FromBody]Employee value)
        {
            //_session.Load<Employee>(value.Id);
            _session.Update(value);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(long id)
        {
            Employee toDelete = _session.Get<Employee>(id);
            if (toDelete != null)
            {
                _session.Delete(toDelete);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
