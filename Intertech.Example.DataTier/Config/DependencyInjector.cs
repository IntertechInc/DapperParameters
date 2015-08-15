using DapperWrapper;
using Intertech.Facade.DapperParameters;
using SimpleInjector;
using System.Configuration;
using System.Data.SqlClient;

namespace Intertech.Example.DataTier.Config
{
    public static class DependencyInjector
    {
        private static bool _isInitialized = false;
        private static Container _container;

        public static void Initialize(Container container, bool isUnitTesting = false)
        {
            if (!_isInitialized)
            {
                _isInitialized = true;
                _container = container;
            }
            else
                return;

            if (!isUnitTesting)
            {
                // Registration for DapperParameters.
                container.Register<IDapperParameters, DapperParameters>();

                container.Register<IDbExecutor>(() =>
                {
                    // Put your connection string from app or web.config here.
                    var connStrings = ConfigurationManager.ConnectionStrings["MyConnection"];
                    SqlConnection connection = new SqlConnection(connStrings.ConnectionString);
                    connection.Open();

                    return new SqlExecutor(connection);
                });
            }
        }

        /// <summary>
        /// Get an instance of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetDependency<T>() where T : class
        {
            return _container.GetInstance<T>();
        }
    }
}
