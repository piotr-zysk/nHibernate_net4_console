using Moq;
using nHibernate.core.webapi.Controllers;
using nHibernate.core.webapi.Repositories;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;

namespace Tests
{
    public class WebapisControllerShould
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Count()
        {
            var repo = new Mock<IUserRepository>();
            repo.Setup(x => x.GetNumberofUsers()).Returns(2);

            var controller = new ValuesController(repo.Object);
            var ret = controller.Count();

            //OkObjectResult expectedRet = new ValuesController(repo.Object).Ok(2);
            ActionResult<int> exp = new ValuesController(repo.Object).Ok(2);

            Assert.That(ret, Is.InstanceOf<ActionResult<int>>());
            //Assert.That(ret.Value, Is.EqualTo(exp));
        }
    }
}