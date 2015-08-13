using System.Data;
using Dapper;
using System;
using System.Collections.Generic;

namespace Intertech.Facade.DapperParameters
{
    /// <summary>
    /// Implementation of IDapperParameters interface.
    /// </summary>
    public class DapperParameters : IDapperParameters
    {
        private DynamicParameters _dynamicParms;

        /// <summary>
        /// Constructor. Creates DynamicParameters without a template.  Call CreateParmsWithTemplate
        /// to use templates.
        /// </summary>
        public DapperParameters()
        {
            _dynamicParms = new DynamicParameters();
        }

        /// <summary>
        /// Alternative way to create the DynamicParameters, template is passed into the constructor.
        /// </summary>
        /// <param name="template"></param>
        public void CreateParmsWithTemplate(object template)
        {
            if (template == null)
                throw new ArgumentNullException("template");

            _dynamicParms = new DynamicParameters(template);
        }

        /// <summary>
        /// Dapper DynamicParameters which will be passed to the Execute or Query command.
        /// </summary>
        public DynamicParameters DynamicParameters
        {
            get
            {
                return _dynamicParms;
            }
        }

        /// <summary>
        /// Matches DynamicParameters ParameterNames getter.
        /// </summary>
        public IEnumerable<string> ParameterNames
        {
            get
            {
                return _dynamicParms.ParameterNames;
            }
        }

        /// <summary>
        /// Matches DynamicParameters Add method so backwards compatibility is easier.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="dbType"></param>
        /// <param name="direction"></param>
        /// <param name="size"></param>
        public void Add(string name, object value = null, DbType? dbType = default(DbType?), ParameterDirection? direction = default(ParameterDirection?), int? size = default(int?))
        {
            _dynamicParms.Add(name, value, dbType, direction, size);
        }

        /// <summary>
        /// Matches DynamicParameters AddDynamicParams method.
        /// </summary>
        /// <param name="param"></param>
        public void AddDynamicParams(dynamic param)
        {
            _dynamicParms.AddDynamicParams(param);
        }

        /// <summary>
        /// Helper method that adds the stored procedure input parameter for you.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        public void AddInputParameter(string name, object value, DbType? dbType = default(DbType?), int? size = default(int?))
        {
            _dynamicParms.Add(name, value, dbType: dbType, direction: ParameterDirection.Input, size: size);
        }

        /// <summary>
        /// Helper method that adds the stored procedure input parameter for you. You can
        /// specify a type for strong typing...required in some unit tests.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <typeparam name="T">The type of the value</typeparam>
        public void AddInputParameter<T>(string name, T value, DbType? dbType = default(DbType?), int? size = default(int?))
        {
            _dynamicParms.Add(name, value, dbType: dbType, direction: ParameterDirection.Input, size: size);
        }

        /// <summary>
        /// Helper method that adds the stored procedure output parameter for you.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        public void AddOutputParameter(string name, DbType? dbType = default(DbType?), int? size = default(int?))
        {
            _dynamicParms.Add(name, dbType: dbType, direction: ParameterDirection.Output, size: size);
        }

        /// <summary>
        /// Helper method that adds the stored procedure return value parameter for you.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        public void AddReturnValue(string name, DbType? dbType = default(DbType?), int? size = default(int?))
        {
            _dynamicParms.Add(name, dbType: dbType, direction: ParameterDirection.ReturnValue, size: size);
        }

        /// <summary>
        /// Matches DynamicParameters Get method.
        /// The means to use to pull an output or return value from the stored procedure call.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T Get<T>(string name)
        {
            return _dynamicParms.Get<T>(name);
        }
    }
}
