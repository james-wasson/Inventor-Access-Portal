using System;
using System.Data.OleDb;
using InventorAccessPortal.DB._DB_DataSetTableAdapters;

namespace InventorAccessPortal.DB.Objects
{
    public class Connection : IDisposable
    {
        public string ConnectionString { get; private set; }
        public string Name { get; private set; }
        public string ProviderName { get; private set; }
        public OleDbConnection DBConnection { get; private set; }
        public All_InvestigatorsTableAdapter AllInvestigatorsTableAdapter { get; private set; }
        public CodesTableAdapter CodesTableAdapter { get; private set; }
        public CollegesTableAdapter CollegesTableAdapter { get; private set; }
        public Combo_FamiliesTableAdapter ComboFamiliesTableAdapter { get; private set; }
        public Combo_Family_ListingsTableAdapter ComboFamilyListingsTableAdapter { get; private set; }
        public DepartmentsTableAdapter DepartmentsTableAdapter { get; private set; }
        public Ending_Fiscal_YearTableAdapter EndingFiscalYearTableAdapter { get; private set; }
        public FamiliesTableAdapter FamiliesTableAdapter { get; private set; }
        public Family_ListingsTableAdapter FamilyListingsTableAdapter { get; private set; }
        public File_NumbersTableAdapter FileNumbersTableAdapter { get; private set; }
        public GenderTableAdapter GenderTableAdapter { get; private set; }
        public InvestigatorsTableAdapter InvestigatorsTableAdapter { get; private set; }
        public OrganizationsTableAdapter OrganizationsTableAdapter { get; private set; }
        public Project_NumbersTableAdapter ProjectNumbersTableAdapter { get; private set; }
        public Records_StatusTableAdapter RecordsStatusTableAdapter { get; private set; }
        public RemindersTableAdapter RemindersTableAdapter { get; private set; }
        public Starting_Fiscal_YearTableAdapter StartingFiscalYearTableAdapter { get; private set; }
        public StatusTableAdapter StatusTableAdapter { get; private set; }
        public TransactionsTableAdapter TransactionsTableAdapter { get; private set; }
        public TableAdapterManager TableAdapterManager { get; private set; }

        // removes default constructor
        private Connection() { }
        // public constructor
        public Connection(string connString, string name, string providerName = "")
        {
            try
            {
                if (connString == null || connString == "")
                    throw new Exception("No Connection String Provided");
                // if not provider is specifed in the string
                if (!connString.ToLower().Contains("provider="))
                {
                    // use the one in the config file if it exists
                    if (providerName != null && providerName != "")
                    {
                        connString += ";Provider=" + providerName;
                    }
                    else // if no provider name can be found throw an error
                    {
                        throw new Exception("No Provider Name is in either connection string or in settings");
                    }
                }
                // sets the object variables
                DBConnection = new OleDbConnection(connString);
                ConnectionString = connString;
                Name = name;
                ProviderName = providerName;
                SetUpAdapterTables();
            }
            catch (Exception ex)
            {
                throw new Exception("DataBase Connection failed" +
                    Environment.NewLine +
                    "Connection String: " + (connString != null ? connString : "Error: Can't Read") +
                    Environment.NewLine +
                    "Name: " + (name != null ? name : "Error: Can't Read") +
                    Environment.NewLine +
                    "Provider Name: " + (providerName != null ? providerName : "Error: Can't Read") +
                    Environment.NewLine +
                    "Exception: " + ex.Message);
            }
        }

        private void SetUpAdapterTables()
        {
            // initalizes the Table Adaptor
            AllInvestigatorsTableAdapter = new All_InvestigatorsTableAdapter();
            CodesTableAdapter = new CodesTableAdapter();
            CollegesTableAdapter = new CollegesTableAdapter();
            ComboFamiliesTableAdapter = new Combo_FamiliesTableAdapter();
            ComboFamilyListingsTableAdapter = new Combo_Family_ListingsTableAdapter();
            DepartmentsTableAdapter = new DepartmentsTableAdapter();
            EndingFiscalYearTableAdapter = new Ending_Fiscal_YearTableAdapter();
            FamiliesTableAdapter = new FamiliesTableAdapter();
            FamilyListingsTableAdapter = new Family_ListingsTableAdapter();
            FileNumbersTableAdapter = new File_NumbersTableAdapter();
            GenderTableAdapter = new GenderTableAdapter();
            InvestigatorsTableAdapter = new InvestigatorsTableAdapter();
            OrganizationsTableAdapter = new OrganizationsTableAdapter();
            ProjectNumbersTableAdapter = new Project_NumbersTableAdapter();
            RecordsStatusTableAdapter = new Records_StatusTableAdapter();
            RemindersTableAdapter = new RemindersTableAdapter();
            StartingFiscalYearTableAdapter = new Starting_Fiscal_YearTableAdapter();
            StatusTableAdapter = new StatusTableAdapter();
            TransactionsTableAdapter = new TransactionsTableAdapter();
            TableAdapterManager = new TableAdapterManager();

            // set the connection to the right database
            AllInvestigatorsTableAdapter.Connection = DBConnection;
            CodesTableAdapter.Connection = DBConnection;
            CollegesTableAdapter.Connection = DBConnection;
            ComboFamiliesTableAdapter.Connection = DBConnection;
            ComboFamilyListingsTableAdapter.Connection = DBConnection;
            DepartmentsTableAdapter.Connection = DBConnection;
            EndingFiscalYearTableAdapter.Connection = DBConnection;
            FamiliesTableAdapter.Connection = DBConnection;
            FamilyListingsTableAdapter.Connection = DBConnection;
            FileNumbersTableAdapter.Connection = DBConnection;
            GenderTableAdapter.Connection = DBConnection;
            InvestigatorsTableAdapter.Connection = DBConnection;
            OrganizationsTableAdapter.Connection = DBConnection;
            ProjectNumbersTableAdapter.Connection = DBConnection;
            RecordsStatusTableAdapter.Connection = DBConnection;
            RemindersTableAdapter.Connection = DBConnection;
            StartingFiscalYearTableAdapter.Connection = DBConnection;
            StatusTableAdapter.Connection = DBConnection;
            TransactionsTableAdapter.Connection = DBConnection;
            TableAdapterManager.Connection = DBConnection;
        }


        public void Dispose()
        {

        }
    }
}
