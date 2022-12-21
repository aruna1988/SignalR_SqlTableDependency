using SignalR_SqlTableDependency.Hubs;
using SignalR_SqlTableDependency.Models;
using System.Configuration;
using TableDependency.SqlClient;

namespace SignalR_SqlTableDependency.SubscribeTableDependencies
{
    public class SubscribeProductTableDependency : ISubscribeTableDependency
    {
        SqlTableDependency<EmpDetLocal> tableDependency;
        DashboardHub dashboardHub;
        string connectionString_ = "";
        public SubscribeProductTableDependency(DashboardHub dashboardHub, IConfiguration configuration)
        {
            this.dashboardHub = dashboardHub;
            connectionString_ = configuration.GetConnectionString("DefaultConnection");
        }

        public void SubscribeTableDependency()
        {
            //Data Source=.;Initial Catalog=empLocal;User Id=sa;Password=SYSMANAGER
            //Data Source=.;Initial Catalog=empLocal;Persist Security Info=True;User ID=sa;Password=SYSMANAGER
            //string connectionString = connectionString_;
             string connectionString = "Server=ASIRI-MF-LP;Database=empLocal;User Id=sa;password=SYSMANAGER;Trusted_Connection=False;MultipleActiveResultSets=true;";
            tableDependency = new SqlTableDependency<EmpDetLocal>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        private void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<EmpDetLocal> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                dashboardHub.SendProducts();
            }
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(EmpDetLocal)} SqlTableDependency error: {e.Error.Message}");
        }
    }
}
