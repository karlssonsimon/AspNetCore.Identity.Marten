using System;
using System.Linq.Expressions;
using Marten;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Test;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AspNetCore.Identity.Marten.Test
{
    public class MartenStoreTest : IdentitySpecificationTestBase<MartenUser, MartenRole>, IClassFixture<DatabaseFixture>, IDisposable
    {
        private readonly DatabaseFixture _fixture;
        private IDocumentSession _session;

        public MartenStoreTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        protected override object CreateTestContext()
        {
            return CreateSession();
        }

        private IDocumentSession CreateSession()
        {
            return _session ?? (_session = _fixture.DocumentStore.LightweightSession());
        }

        protected override void AddUserStore(IServiceCollection services, object context = null)
        {
            var session = ((IDocumentSession) context);
            services.AddSingleton<IUserStore<MartenUser>>(new MartenUserStore(session));
        }

        protected override void SetUserPasswordHash(MartenUser user, string hashedPassword)
        {
            user.PasswordHash = hashedPassword;
        }

        protected override MartenUser CreateTestUser(string namePrefix = "", string email = "", string phoneNumber = "",
            bool lockoutEnabled = false, DateTimeOffset? lockoutEnd = null, bool useNamePrefixAsUserName = false)
        {
            return new MartenUser
            {
                UserName = useNamePrefixAsUserName ? namePrefix : $"{namePrefix}{Guid.NewGuid()}",
                Email = email,
                PhoneNumber = phoneNumber,
                LockoutEnabled = lockoutEnabled,
                LockoutEnd = lockoutEnd
            };
        }

        protected override Expression<Func<MartenUser, bool>> UserNameEqualsPredicate(string userName) =>
            user => user.UserName == userName;

        protected override Expression<Func<MartenUser, bool>> UserNameStartsWithPredicate(string userName) =>
            user => user.UserName.StartsWith(userName);

        protected override void AddRoleStore(IServiceCollection services, object context = null)
        {
            var session = ((IDocumentSession) context);
            services.AddSingleton<IRoleStore<MartenRole>>(new MartenRoleStore(session));
        }

        protected override MartenRole CreateTestRole(string roleNamePrefix = "",
            bool useRoleNamePrefixAsRoleName = false)
        {
            var roleName = useRoleNamePrefixAsRoleName ? roleNamePrefix : $"{roleNamePrefix}{Guid.NewGuid()}";
            return new MartenRole(roleName);
        }

        protected override Expression<Func<MartenRole, bool>> RoleNameEqualsPredicate(string roleName) =>
            role => role.Name == roleName;

        protected override Expression<Func<MartenRole, bool>> RoleNameStartsWithPredicate(string roleName) =>
            role => role.Name.StartsWith(roleName);


        public void Dispose()
        {
            _session?.Dispose();
        }
    }
}