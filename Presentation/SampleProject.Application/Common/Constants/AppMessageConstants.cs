﻿namespace SampleProject.Application.Common.Constants
{
    /// <summary>
    /// Should define message key after that Frontend, Mobile app will define the i18n libs to have multi-languages
    /// </summary>
    public static class AppMessageConstants
    {
        #region Account

        public const string EMAIL_IS_REQUIRED = "validate.account.email.required";
        public const string EMAIL_IS_ALREADY_TAKEN = "validate.account.email.alreadyTaken";
        public const string EMAIL_IS_INVALID = "validate.account.email.invalid";
        public const string PASSWORD_IS_REQUIRED = "validate.account.password.required";
        public const string PHONE_IS_REQUIRED = "validate.account.phone.required";
        public const string ACCOUNT_REFRESH_TOKEN_IS_REQUIRED = "validate.account.refreshToken.required";

        #endregion

        #region Me

        public const string USER_NOT_FOUND = "message.me.profile.userNotFound";

        #endregion

        #region Company 

        public const string COMPANY_NOT_FOUND = "message.companyNotFound";
        public const string COMPANY_CREATED_SUCCESSFULLY = "message.companyCreatedSuccessfully";
        public const string COMPANY_UPDATED_SUCCESSFULLY = "message.companyUpdatedSuccessfully";
        public const string COMPANY_DELETED_SUCCESSFULLY = "message.companyDeletedSuccessfully";
        public const string COMPANY_RESTORED_SUCCESSFULLY = "message.companyRestoredSuccessfully";
        public const string COMPANY_EXISTED = "message.companyExisted";
        public const string ORGANIZATION_ID_EXISTED = "message.organizationIdExisted";
        public const string COMPANY_TYPE_NOT_FOUND = "message.companyTypeNotFound";
        
        #endregion
    }
}
