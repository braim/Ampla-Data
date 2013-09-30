﻿

namespace AmplaWeb.Data.Membership
{
    public class AmplaRoleProvider : ReadOnlyRoleProvider
    {
        /// <summary>
        /// Gets a value indicating whether the specified user is in the specified role for the configured applicationName.
        /// </summary>
        /// <param name="username">The user name to search for.</param>
        /// <param name="roleName">The role to search in.</param>
        /// <returns>
        /// true if the specified user is in the specified role for the configured applicationName; otherwise, false.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override bool IsUserInRole(string username, string roleName)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets a list of the roles that a specified user is in for the configured applicationName.
        /// </summary>
        /// <param name="username">The user to return a list of roles for.</param>
        /// <returns>
        /// A string array containing the names of all the roles that the specified user is in for the configured applicationName.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override string[] GetRolesForUser(string username)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets a value indicating whether the specified role name already exists in the role data source for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to search for in the data source.</param>
        /// <returns>
        /// true if the role name already exists in the data source for the configured applicationName; otherwise, false.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override bool RoleExists(string roleName)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets a list of all the roles for the configured applicationName.
        /// </summary>
        /// <returns>
        /// A string array containing the names of all the roles stored in the data source for the configured applicationName.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override string[] GetAllRoles()
        {
            throw new System.NotImplementedException();
        }
    }
}