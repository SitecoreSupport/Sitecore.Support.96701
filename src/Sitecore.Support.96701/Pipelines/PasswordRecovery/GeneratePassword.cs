namespace Sitecore.Support.Pipelines.PasswordRecovery
{
    using Sitecore.Diagnostics;
    using Sitecore.Pipelines.PasswordRecovery;
    using System;
    using System.Web.Security;

    public class GeneratePassword : PasswordRecoveryProcessor
    {
        public override void Process(PasswordRecoveryArgs args)
        {
            Assert.IsNotNull(args, "args");
            MembershipUser user = Membership.GetUser(args.Username);
          
            if (user == null)
            {
                args.AbortPipeline();
            }

            #region Added code
            if (user.IsLockedOut)
            {
                Log.Error("The account of the specified user was locked out.", this);
                args.AbortPipeline();
            }              
            #endregion

            else
            {
                args.Password = user.ResetPassword();
            }
        }
    }
}