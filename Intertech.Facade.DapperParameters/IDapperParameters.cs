using Dapper;
using System.Collections.Generic;
using System.Data;

namespace Intertech.Facade.DapperParameters
{
    /// <summary>
    /// Interface for Dappers DynamicParameters class to facilitate unit testing.
    /// </summary>
    public interface IDapperParameters
    {
        /// <summary>
        /// Dapper DynamicParameters which will be passed to the Execute or Query command.
        /// </summary>
        DynamicParameters DynamicParameters { get; }

        /// <summary>
        /// Alternative way to create the DynamicParameters, template is passed into the constructor.
        /// By default, it's created without a template.
        /// </summary>
        /// <param name="template"></param>
        void CreateParmsWithTemplate(object template);

        /// <summary>
        /// Matches DynamicParameters Add method so backwards compatibility is easier.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="dbType"></param>
        /// <param name="direction"></param>
        /// <param name="size"></param>
        void Add(string name, object value = null, DbType? dbType = default(DbType?), ParameterDirection? direction = default(ParameterDirection?), int? size = default(int?));

        /// <summary>
        /// Matches DynamicParameters AddDynamicParams method.
        /// </summary>
        /// <param name="param"></param>
        void AddDynamicParams(dynamic param);

        /// <summary>
        /// Matches DynamicParameters Get method.
        /// The means to use to pull an output or return value from the stored procedure call.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        T Get<T>(string name);

        /// <summary>
        /// Matches DynamicParameters ParameterNames getter.
        /// </summary>
        IEnumerable<string> ParameterNames { get; }

        /// <summary>
        /// Helper method that adds the stored procedure input parameter for you.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        void AddInputParameter(string name, object value, DbType? dbType = null, int? size = null);

        /// <summary>
        /// Helper method that adds the stored procedure input parameter for you. You can
        /// specify a type for strong typing...required in some unit tests.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <typeparam name="T">The type of the value</typeparam>
        void AddInputParameter<T>(string name, T value, DbType? dbType = null, int? size = null);

        /// <summary>
        /// Helper method that adds the stored procedure output parameter for you.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        void AddOutputParameter(string name, DbType? dbType = null, int? size = null);

        /// <summary>
        /// Helper method that adds the stored procedure return value parameter for you.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        void AddReturnValue(string name, DbType? dbType = null, int? size = null);
    }
}
