using NUnit.Framework;
using Intertech.Example.DataTier.Config;
using SimpleInjector;
using Intertech.Facade.DapperParameters;
using Moq;
using Intertech.Example.DataTier;
using DapperWrapper;
using System.Data;
using Dapper;

namespace Intertech.Facade.UnitTests
{
    [TestFixture]
    public class DbAccessTests
    {
        private DbAccess _dbAccess;
        private Mock<IDapperParameters> _parmsMock;
        private Mock<IDbExecutor> _execMock;
        private DynamicParameters _dynParms;

        [SetUp]
        public void Setup()
        {
            var container = new Container();
            DependencyInjector.Initialize(container, true);

            _parmsMock = new Mock<IDapperParameters>();
            _execMock = new Mock<IDbExecutor>();

            container.Register(() => { return _parmsMock.Object; });
            container.Register(() => { return _execMock.Object; });

            _dynParms = new DynamicParameters();

            _dbAccess = new DbAccess();
        }

        [Test]
        public void ExampleMethod_Success_Test()
        {
            // Arrange
            string valueParm = "Hello";
            int returnValue = 1;
            bool outputValue = true;

            // Parameter setups
            // This is a bit long so do what I did in my projects and create
            // helper methods to do this automatically for you.
            _parmsMock.Setup(m => m.Add(It.Is<string>(name => name == "Parm1"),
                It.Is<string>(val => val == valueParm),
                It.Is<DbType?>(dt => dt == DbType.String),
                It.Is<ParameterDirection?>(pd => pd == ParameterDirection.Input),
                It.Is<int?>(size => size == 8))).Verifiable();
            _parmsMock.Setup(m => m.AddOutputParameter(It.Is<string>(name => name == "Output1"),
                It.Is<DbType?>(dt => dt == DbType.Boolean),
                It.Is<int?>(size => size == null))).Verifiable();
            _parmsMock.Setup(m => m.Add(It.Is<string>(name => name == "ReturnValue"),
                It.Is<object>(val => val == null),
                It.Is<DbType?>(dt => dt == DbType.Int32),
                It.Is<ParameterDirection?>(pd => pd == ParameterDirection.ReturnValue),
                It.Is<int?>(size => size == null))).Verifiable();
            _parmsMock.SetupGet(m => m.DynamicParameters).Returns(_dynParms).Verifiable();
            _parmsMock.Setup(m => m.Get<int>(It.Is<string>(name => name == "ReturnValue")))
                .Returns(returnValue).Verifiable();
            _parmsMock.Setup(m => m.Get<bool>(It.Is<string>(name => name == "Output1")))
                .Returns(outputValue).Verifiable();

            // Execute setups
            _execMock.Setup(m => m.Execute(It.Is<string>(sql => sql == "MySproc"),
                It.Is<object>(parm => parm == _dynParms),
                It.IsAny<IDbTransaction>(), It.IsAny<int?>(),
                It.Is<CommandType?>(ct => ct == CommandType.StoredProcedure))).Returns(0).Verifiable();

            // Act
            var actual = _dbAccess.ExampleMethod(valueParm);

            // Assert
            _parmsMock.Verify();
            _execMock.Verify();
            Assert.That(actual, Is.EqualTo(returnValue));
        }

        [Test]
        public void ExampleMethod_Fail_Test()
        {
            // Arrange
            string valueParm = "Hello";
            int returnValue = 1;
            bool outputValue = false;

            // Parameter setups
            // This is a bit long so do what I did in my projects and create
            // helper methods to do this automatically for you.
            _parmsMock.Setup(m => m.Add(It.Is<string>(name => name == "Parm1"),
                It.Is<string>(val => val == valueParm),
                It.Is<DbType?>(dt => dt == DbType.String),
                It.Is<ParameterDirection?>(pd => pd == ParameterDirection.Input),
                It.Is<int?>(size => size == 8))).Verifiable();
            _parmsMock.Setup(m => m.AddOutputParameter(It.Is<string>(name => name == "Output1"),
                It.Is<DbType?>(dt => dt == DbType.Boolean),
                It.Is<int?>(size => size == null))).Verifiable();
            _parmsMock.Setup(m => m.Add(It.Is<string>(name => name == "ReturnValue"),
                It.Is<object>(val => val == null),
                It.Is<DbType?>(dt => dt == DbType.Int32),
                It.Is<ParameterDirection?>(pd => pd == ParameterDirection.ReturnValue),
                It.Is<int?>(size => size == null))).Verifiable();
            _parmsMock.SetupGet(m => m.DynamicParameters).Returns(_dynParms).Verifiable();
            _parmsMock.Setup(m => m.Get<int>(It.Is<string>(name => name == "ReturnValue")))
                .Returns(returnValue).Verifiable();
            _parmsMock.Setup(m => m.Get<bool>(It.Is<string>(name => name == "Output1")))
                .Returns(outputValue).Verifiable();

            // Execute setups
            _execMock.Setup(m => m.Execute(It.Is<string>(sql => sql == "MySproc"),
                It.Is<object>(parm => parm == _dynParms),
                It.IsAny<IDbTransaction>(), It.IsAny<int?>(),
                It.Is<CommandType?>(ct => ct == CommandType.StoredProcedure))).Returns(0).Verifiable();

            // Act
            var actual = _dbAccess.ExampleMethod(valueParm);

            // Assert
            _parmsMock.Verify();
            _execMock.Verify();
            Assert.That(actual, Is.EqualTo(0));
        }
    }
}
