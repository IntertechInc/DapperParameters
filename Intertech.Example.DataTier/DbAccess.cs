using DapperWrapper;
using Intertech.Example.DataTier.Config;
using Intertech.Facade.DapperParameters;
using System.Data;

namespace Intertech.Example.DataTier
{
    public class DbAccess
    {
        public int ExampleMethod(string valueParm)
        {
            using (var cnn = DependencyInjector.GetDependency<IDbExecutor>())
            {
                var p = DependencyInjector.GetDependency<IDapperParameters>();
                p.Add("Parm1", valueParm, dbType: DbType.String, direction: ParameterDirection.Input, size: 8);
                p.AddOutputParameter("Output1", DbType.Boolean);
                p.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                cnn.Execute("MySproc", param: p.DynamicParameters, commandType: CommandType.StoredProcedure);

                var returnVal = p.Get<int>("ReturnValue");
                if (p.Get<bool>("Output1"))
                    return returnVal;

                return 0;
            }
        }
    }
}
