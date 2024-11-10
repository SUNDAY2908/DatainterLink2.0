 // IndexModel.cs
using FinalProject.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Pages
{
    public class IndexModel : PageModel
    {
        public List<EmailInfo> listEmails = new List<EmailInfo>();
        private readonly ILogger<IndexModel> _logger;
        private readonly string connectionString = "Server=tcp:datainterlink-server.database.windows.net,1433;Initial Catalog=DataInterLink;Persist Security Info=False;User ID=datainterlink;Password=epsw_1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string username = "";
                    if (User.Identity.Name == null)
                    {
                        username = "";
                    }
                    else
                    {
                        username = User.Identity.Name;
                    }

                    using (SqlCommand command = new SqlCommand("SELECT * FROM email WHERE recipient_id = @Username ORDER BY date_time DESC", connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EmailInfo emailInfo = new EmailInfo();
                                emailInfo.email_id = reader.GetInt32(0).ToString();
                                emailInfo.subject = reader.IsDBNull(1) ? "" : reader.GetString(1);
                                emailInfo.body = reader.IsDBNull(2) ? "" : reader.GetString(2);
                                emailInfo.date_time = reader.GetDateTime(3).ToString("dd/MM/yyyy HH:mm");
                                emailInfo.is_read = reader.GetInt32(4).ToString();
                                emailInfo.sender_id = reader.IsDBNull(5) ? "" : reader.GetString(5);
                                emailInfo.recipient_id = reader.IsDBNull(6) ? "" : reader.GetString(6);
                                listEmails.Add(emailInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching emails");
            }
        }
    }

    public class EmailInfo
    {
        public string email_id;
        public string subject;
        public string body;
        public string date_time;
        public string is_read;
        public string sender_id;
        public string recipient_id;
    }
}