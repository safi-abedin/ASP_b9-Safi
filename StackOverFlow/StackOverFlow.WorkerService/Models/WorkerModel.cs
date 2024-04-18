using Autofac;
using Microsoft.AspNetCore.Identity;
using StackOverFlow.Infrastructure.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlow.WorkerService.Models
{
    public class WorkerModel
    {
        private  UserManager<ApplicationUser> _userManager;

        private ILifetimeScope _scope;



        public WorkerModel()
        {
            
        }

        public WorkerModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        internal async Task AddClaimAsync()
        {
            var users = _userManager.Users.ToList();


            foreach (var user in users)
            {
                if (user.Reputation >= 10)
                {
                    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("CreateAnswer", "true"));
                }
                if (user.Reputation >= 20)
                {
                    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("CreateQuestion", "true"));
                }
                if (user.Reputation >= 30)
                {
                    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("EditQuestion", "true"));
                }
                if (user.Reputation >= 40)
                {
                    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("DeleteQuestion", "true"));
                }
            }
        }

        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _userManager = _scope.Resolve <UserManager<ApplicationUser>> ();
        }
    }
}
