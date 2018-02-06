using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using InventorAccessPortal.DB._DB_DatasetTableAdapters;
using JetEntityFrameworkProvider;

namespace InventorAccessPortal.DB.Objects
{
    public class Connection : IDisposable
    {
        // connection data
        public string ConnectionString { get; private set; } = "";
        public string ConnectionStringName { get; private set; } = "";
        public string ProviderName { get; private set; } = "";
        public OleDbConnection DBConnection { get; private set; } = null;

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
                        connString += ";Provider=\"" + providerName + "\"";
                    }
                    else // if no provider name can be found throw an error
                    {
                        throw new Exception("No Provider Name is in either connection string or in settings");
                    }
                }

                // sets the object variables
                DBConnection = new OleDbConnection(connString);
                ConnectionString = connString;
                ConnectionStringName = name;
                ProviderName = providerName;
            }
            catch (Exception ex)
            {
                // throws a verbose exception
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

        /*
         *  Data Table Connectors/Instanciators
         */

        // A method to instanciate codesTableAdapter only when needed
        private CodesTableAdapter codesTableAdapter = null;
        public CodesTableAdapter CodesTableAdapter
        {
            get
            {
                if (codesTableAdapter == null)
                {
                    codesTableAdapter = new CodesTableAdapter();
                    codesTableAdapter.Connection = DBConnection;
                }
                return codesTableAdapter;
            }
            private set { }
        }

        // A method to instanciate allInvestigatorsTableAdapters only when needed
        private All_InvestigatorsTableAdapter allInvestigatorsTableAdapters = null;
        public All_InvestigatorsTableAdapter AllInvestigatorsTableAdapters
        {
            get
            {
                if (allInvestigatorsTableAdapters == null)
                {
                    allInvestigatorsTableAdapters = new All_InvestigatorsTableAdapter();
                    allInvestigatorsTableAdapters.Connection = DBConnection;
                }
                return allInvestigatorsTableAdapters;
            }
            private set { }
        }

        // A method to instanciate collegesTableAdapter only when needed
        private CollegesTableAdapter collegesTableAdapter = null;
        public CollegesTableAdapter CollegesTableAdapter
        {
            get
            {
                if (collegesTableAdapter == null)
                {
                    collegesTableAdapter = new CollegesTableAdapter();
                    collegesTableAdapter.Connection = DBConnection;
                }
                return collegesTableAdapter;
            }
            private set { }
        }

        // A method to instanciate comboFamiliesTableAdapter only when needed
        private Combo_FamiliesTableAdapter comboFamiliesTableAdapter = null;
        public Combo_FamiliesTableAdapter ComboFamiliesTableAdapter
        {
            get
            {
                if (comboFamiliesTableAdapter == null)
                {
                    comboFamiliesTableAdapter = new Combo_FamiliesTableAdapter();
                    comboFamiliesTableAdapter.Connection = DBConnection;
                }
                return comboFamiliesTableAdapter;
            }
            private set { }
        }

        // A method to instanciate comboFamilyListingsTableAdapter only when needed
        private Combo_Family_ListingsTableAdapter comboFamilyListingsTableAdapter = null;
        public Combo_Family_ListingsTableAdapter ComboFamilyListingsTableAdapter
        {
            get
            {
                if (comboFamilyListingsTableAdapter == null)
                {
                    comboFamilyListingsTableAdapter = new Combo_Family_ListingsTableAdapter();
                    comboFamilyListingsTableAdapter.Connection = DBConnection;
                }
                return comboFamilyListingsTableAdapter;
            }
            private set { }
        }

        // A method to instanciate departmentsTableAdapter only when needed
        private DepartmentsTableAdapter departmentsTableAdapter = null;
        public DepartmentsTableAdapter DepartmentsTableAdapter
        {
            get
            {
                if (departmentsTableAdapter == null)
                {
                    departmentsTableAdapter = new DepartmentsTableAdapter();
                    departmentsTableAdapter.Connection = DBConnection;
                }
                return departmentsTableAdapter;
            }
            private set { }
        }

        // A method to instanciate endingFiscalYearTableAdapter only when needed
        private Ending_Fiscal_YearTableAdapter endingFiscalYearTableAdapter = null;
        public Ending_Fiscal_YearTableAdapter EndingFiscalYearTableAdapter
        {
            get
            {
                if (endingFiscalYearTableAdapter == null)
                {
                    endingFiscalYearTableAdapter = new Ending_Fiscal_YearTableAdapter();
                    endingFiscalYearTableAdapter.Connection = DBConnection;
                }
                return endingFiscalYearTableAdapter;
            }
            private set { }
        }

        // A method to instanciate familiesTableAdapter only when needed
        private FamiliesTableAdapter familiesTableAdapter = null;
        public FamiliesTableAdapter FamiliesTableAdapter
        {
            get
            {
                if (familiesTableAdapter == null)
                {
                    familiesTableAdapter = new FamiliesTableAdapter();
                    familiesTableAdapter.Connection = DBConnection;
                }
                return familiesTableAdapter;
            }
            private set { }
        }

        // A method to instanciate familyListingsTableAdapter only when needed
        private Family_ListingsTableAdapter familyListingsTableAdapter = null;
        public Family_ListingsTableAdapter FamilyListingsTableAdapter
        {
            get
            {
                if (familyListingsTableAdapter == null)
                {
                    familyListingsTableAdapter = new Family_ListingsTableAdapter();
                    familyListingsTableAdapter.Connection = DBConnection;
                }
                return familyListingsTableAdapter;
            }
            private set { }
        }

        // A method to instanciate fileNumbersTableAdapter only when needed
        private File_NumbersTableAdapter fileNumbersTableAdapter = null;
        public File_NumbersTableAdapter FileNumbersTableAdapter
        {
            get
            {
                if (fileNumbersTableAdapter == null)
                {
                    fileNumbersTableAdapter = new File_NumbersTableAdapter();
                    fileNumbersTableAdapter.Connection = DBConnection;
                }
                return fileNumbersTableAdapter;
            }
            private set { }
        }

        // A method to instanciate genderTableAdapter only when needed
        private GenderTableAdapter genderTableAdapter = null;
        public GenderTableAdapter GenderTableAdapter
        {
            get
            {
                if (genderTableAdapter == null)
                {
                    genderTableAdapter = new GenderTableAdapter();
                    genderTableAdapter.Connection = DBConnection;
                }
                return genderTableAdapter;
            }
            private set { }
        }

        // A method to instanciate investigatorsTableAdapter only when needed
        private InvestigatorsTableAdapter investigatorsTableAdapter = null;
        public InvestigatorsTableAdapter InvestigatorsTableAdapter
        {
            get
            {
                if (investigatorsTableAdapter == null)
                {
                    investigatorsTableAdapter = new InvestigatorsTableAdapter();
                    investigatorsTableAdapter.Connection = DBConnection;
                }
                return investigatorsTableAdapter;
            }
            private set { }
        }

        // A method to instanciate organizationsTableAdapter only when needed
        private OrganizationsTableAdapter organizationsTableAdapter = null;
        public OrganizationsTableAdapter OrganizationsTableAdapter
        {
            get
            {
                if (organizationsTableAdapter == null)
                {
                    organizationsTableAdapter = new OrganizationsTableAdapter();
                    organizationsTableAdapter.Connection = DBConnection;
                }
                return organizationsTableAdapter;
            }
            private set { }
        }

        // A method to instanciate projectNumbersTableAdapter only when needed
        private Project_NumbersTableAdapter projectNumbersTableAdapter = null;
        public Project_NumbersTableAdapter ProjectNumbersTableAdapter
        {
            get
            {
                if (projectNumbersTableAdapter == null)
                {
                    projectNumbersTableAdapter = new Project_NumbersTableAdapter();
                    projectNumbersTableAdapter.Connection = DBConnection;
                }
                return projectNumbersTableAdapter;
            }
            private set { }
        }

        // A method to instanciate recordsStatusTableAdapter only when needed
        private Records_StatusTableAdapter recordsStatusTableAdapter = null;
        public Records_StatusTableAdapter RecordsStatusTableAdapter
        {
            get
            {
                if (recordsStatusTableAdapter == null)
                {
                    recordsStatusTableAdapter = new Records_StatusTableAdapter();
                    recordsStatusTableAdapter.Connection = DBConnection;
                }
                return recordsStatusTableAdapter;
            }
            private set { }
        }

        // A method to instanciate remindersTableAdapter only when needed
        private RemindersTableAdapter remindersTableAdapter = null;
        public RemindersTableAdapter RemindersTableAdapter
        {
            get
            {
                if (remindersTableAdapter == null)
                {
                    remindersTableAdapter = new RemindersTableAdapter();
                    remindersTableAdapter.Connection = DBConnection;
                }
                return remindersTableAdapter;
            }
            private set { }
        }

        // A method to instanciate startingFiscalYearTableAdapter only when needed
        private Starting_Fiscal_YearTableAdapter startingFiscalYearTableAdapter = null;
        public Starting_Fiscal_YearTableAdapter StartingFiscalYearTableAdapter
        {
            get
            {
                if (startingFiscalYearTableAdapter == null)
                {
                    startingFiscalYearTableAdapter = new Starting_Fiscal_YearTableAdapter();
                    startingFiscalYearTableAdapter.Connection = DBConnection;
                }
                return startingFiscalYearTableAdapter;
            }
            private set { }
        }

        // A method to instanciate statusTableAdapter only when needed
        private StatusTableAdapter statusTableAdapter = null;
        public StatusTableAdapter StatusTableAdapter
        {
            get
            {
                if (statusTableAdapter == null)
                {
                    statusTableAdapter = new StatusTableAdapter();
                    statusTableAdapter.Connection = DBConnection;
                }
                return statusTableAdapter;
            }
            private set { }
        }

        // A method to instanciate transactionsTableAdapter only when needed
        private TransactionsTableAdapter transactionsTableAdapter = null;
        public TransactionsTableAdapter TransactionsTableAdapter
        {
            get
            {
                if (transactionsTableAdapter == null)
                {
                    transactionsTableAdapter = new TransactionsTableAdapter();
                    transactionsTableAdapter.Connection = DBConnection;
                }
                return transactionsTableAdapter;
            }
            private set { }
        }

        // A method to instanciate tableAdapterManager only when needed
        private TableAdapterManager tableAdapterManager = null;
        public TableAdapterManager TableAdapterManager
        {
            get
            {
                if (tableAdapterManager == null)
                {
                    tableAdapterManager = new TableAdapterManager();
                    tableAdapterManager.Connection = DBConnection;
                }
                return tableAdapterManager;
            }
            private set { }
        }

        // A method to instanciate loginDataAdapterManager only when needed
        private Login_DataTableAdapter loginDataAdapterManager = null;
        public Login_DataTableAdapter LoginDataAdapterManager
        {
            get
            {
                if (loginDataAdapterManager == null)
                {
                    loginDataAdapterManager = new Login_DataTableAdapter();
                    loginDataAdapterManager.Connection = DBConnection;
                }
                return loginDataAdapterManager;
            }
            private set { }
        }

        // disposes of the databse connection adn all assigned table adaptors
        public void Dispose()
        {
            DBConnection.Dispose();
            if (codesTableAdapter != null) codesTableAdapter.Dispose();
            if (allInvestigatorsTableAdapters != null) allInvestigatorsTableAdapters.Dispose();
            if (collegesTableAdapter != null) collegesTableAdapter.Dispose();
            if (comboFamiliesTableAdapter != null) comboFamiliesTableAdapter.Dispose();
            if (comboFamilyListingsTableAdapter != null) comboFamilyListingsTableAdapter.Dispose();
            if (departmentsTableAdapter != null) departmentsTableAdapter.Dispose();
            if (endingFiscalYearTableAdapter != null) endingFiscalYearTableAdapter.Dispose();
            if (familiesTableAdapter != null) familiesTableAdapter.Dispose();
            if (familyListingsTableAdapter != null) familyListingsTableAdapter.Dispose();
            if (fileNumbersTableAdapter != null) fileNumbersTableAdapter.Dispose();
            if (genderTableAdapter != null) genderTableAdapter.Dispose();
            if (investigatorsTableAdapter != null) investigatorsTableAdapter.Dispose();
            if (organizationsTableAdapter != null) organizationsTableAdapter.Dispose();
            if (projectNumbersTableAdapter != null) projectNumbersTableAdapter.Dispose();
            if (recordsStatusTableAdapter != null) recordsStatusTableAdapter.Dispose();
            if (remindersTableAdapter != null) remindersTableAdapter.Dispose();
            if (startingFiscalYearTableAdapter != null) startingFiscalYearTableAdapter.Dispose();
            if (statusTableAdapter != null) statusTableAdapter.Dispose();
            if (transactionsTableAdapter != null) transactionsTableAdapter.Dispose();
            if (tableAdapterManager != null) tableAdapterManager.Dispose();
            if (loginDataAdapterManager != null) loginDataAdapterManager.Dispose();
        }
    }
    
}
