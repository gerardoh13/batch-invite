using Data;
using Data.Providers;
using Models.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Services
{
    public class TokenService : ITokenService
    {
        private IDataProvider _dataProvider;

        public TokenService(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public InviteContributor ConfirmContributor(string token)
        {
            InviteContributor model = null;

            _dataProvider.ExecuteCmd("dbo.Contributors_SelectByToken",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Token", token);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    model = MapContributor(reader);
                }
                );
            return model;
        }
        private static InviteContributor MapContributor(IDataReader reader)
        {
            InviteContributor model = new InviteContributor();
            int index = 0;
            model.Id = reader.GetSafeInt32(index++);
            model.EventId = reader.GetSafeInt32(index++);
            model.Contributor = reader.GetSafeInt32(index++);
            model.ContributionTypeId = reader.GetSafeInt32(index++);
            return model;
        }
    }
}
