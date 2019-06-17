using Data;
using Data.Providers;
using Models.Domain;
using Models.Requests.Emails;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Services 
{
    public class ContributorService: IContributorService
    {
        private IDataProvider _dataProvider = null;
      

        public ContributorService(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void UpdateConfirmStatus(int id)
        {
            _dataProvider.ExecuteNonQuery("dbo.Contributor_UpdateConfirmStatus",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Id", id);
                }
            );
        }

        public void InsertContributors(InviteContributor model)
        {   
            _dataProvider.ExecuteNonQuery("dbo.EventContributors_Insert",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@EventId", model.EventId);
                    paramCol.AddWithValue("@Contributor", model.Contributor);
                    paramCol.AddWithValue("@ContributionTypeId", model.ContributionTypeId);

                }
            );
        }

        public List<InviteContributorRequest> Invite_Contributors(List<InviteContributorRequest> model)
        {
            _dataProvider.ExecuteNonQuery(
            "dbo.Contributors_Invite",
            inputParamMapper: delegate (SqlParameterCollection parameterCollection)

            {
                DataTable dt = new DataTable();
                dt.Columns.Add("UserId", typeof(int));
                dt.Columns.Add("ContributionTypeId", typeof(int));
                dt.Columns.Add("EventId", typeof(int));
                dt.Columns.Add("Token", typeof(string));

                foreach (var item in model)
                {
                    item.Token = GenerateToken();

                    DataRow dr = dt.NewRow();
                    dr[0] = item.UserId;
                    dr[1] = item.ContributionTypeId;
                    dr[2] = item.EventId;
                    dr[3] = item.Token;
                    dt.Rows.Add(dr);

                }
                parameterCollection.AddWithValue("@TokenType", 3);
                parameterCollection.AddWithValue("ContributorTokens", dt);

            });
            return model;
        }

        public string GenerateToken()
        {
            Guid obj = Guid.NewGuid();
            string token = obj.ToString();
            return token;
        }

    }
}
