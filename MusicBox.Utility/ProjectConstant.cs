﻿namespace MusicBox.Utility
{
    public static class ProjectConstant
    {
        public const string ResultNotFound = "Data Not Found";


        #region Dapper
        public const string Proc_CoverType_GetAll = "usp_GetCoverTypes";
        public const string Proc_CoverType_Get = "usp_GetCoverType";
        public const string Proc_CoverType_Delete = "usp_DeleteCoverType";
        public const string Proc_CoverType_Create = "usp_CreateCoverType";
        public const string Proc_CoverType_Update = "usp_UpdateCoverType";
        #endregion
    }
}
