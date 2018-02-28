using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using InventorAccessPortal.DB._DB_DatasetTableAdapters;
using System.Linq;
using System.Text;
using InventorAccessPortal.DB.Objects;
using System.Threading.Tasks;

namespace InventorAccessPortal.DB.Objects
{

    public partial class Connection : _DB_Dataset
    {
        public string ConnectionString { get; private set; } = "";
        public string ConnectionStringName { get; private set; } = "";
        public string ProviderName { get; private set; } = "";
        public OleDbConnection DBConnection { get; private set; } = null;
        public TableAdapterManager TableAdapterManager { get; private set; }

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
               
                // creates and tests the connection
                try
                {
                    // new database object
                    DBConnection = new OleDbConnection(connString);
                    DBConnection.Open();
                    DBConnection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not open Database connection. Exception: " + ex.Message);
                }

                // sets database descriptors
                ConnectionString = connString;
                ConnectionStringName = name;
                ProviderName = providerName;

                // sets new table adaptor
                TableAdapterManager = new TableAdapterManager();
                TableAdapterManager.Connection = DBConnection;
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

        // updates all table adapters registered with this connection
        public void SaveChanges()
        {
            try
            {
                TableAdapterManager.UpdateAll(this);
            }
            catch(Exception ex)
            {
                throw new Exception("Table Adaptor failed to update the Database correctly." + Environment.NewLine + ex.Message);
            }
        }

        // updates all table adapters registered with this connection
        public void DeleteChanges()
        {
            this.RejectChanges();
        }

        public void ClearDataSet()
        {
            this.Reset();
        }

        // clears all the table adapters registered with this connection
        public void ClearAllAdaptors()
        {
            TableAdapterManager.Dispose();
            TableAdapterManager = new TableAdapterManager();
            TableAdapterManager.Connection = DBConnection;
        }

    }

    /*
     * The static functions that are used to fill the datatables in each connection class
     */
    public static class FillConnectionData
    {

        public static Task<Connection> FillCodesAsync(this Connection c)
        {
            return Task.Factory.StartNew(() =>
            {
                return FillCodes(c);
            });
        }

        public static Connection FillCodes(this Connection c)
        {
            c.Codes.Clear();
            if (c.TableAdapterManager.CodesTableAdapter != null)
                c.TableAdapterManager.CodesTableAdapter.Dispose();
            c.TableAdapterManager.CodesTableAdapter = new CodesTableAdapter();
            c.TableAdapterManager.CodesTableAdapter.Connection = c.DBConnection;
            c.TableAdapterManager.CodesTableAdapter.Fill(c.Codes);
            return c;
        }

        public static Task<Connection> FillAllInvestigatorsAsync(this Connection c)
        {
            return Task.Factory.StartNew(() =>
            {
                return FillAllInvestigators(c);
            });
        }

        public static Connection FillAllInvestigators(this Connection c)
        {
            c.All_Investigators.Clear();
            if (c.TableAdapterManager.All_InvestigatorsTableAdapter != null)
                c.TableAdapterManager.All_InvestigatorsTableAdapter.Dispose();
            c.TableAdapterManager.All_InvestigatorsTableAdapter = new All_InvestigatorsTableAdapter();
            c.TableAdapterManager.All_InvestigatorsTableAdapter.Connection = c.DBConnection;
            c.TableAdapterManager.All_InvestigatorsTableAdapter.Fill(c.All_Investigators);
            return c;
        }

        public static Task<Connection> FillCollegesAsync(this Connection c)
        {
            return Task.Factory.StartNew(() =>
            {
                return FillColleges(c);
            });
        }

        public static Connection FillColleges(this Connection c)
        {
            c.Colleges.Clear();
            if (c.TableAdapterManager.CollegesTableAdapter != null)
                c.TableAdapterManager.CollegesTableAdapter.Dispose();
            c.TableAdapterManager.CollegesTableAdapter = new CollegesTableAdapter();
            c.TableAdapterManager.CollegesTableAdapter.Connection = c.DBConnection;
            c.TableAdapterManager.CollegesTableAdapter.Fill(c.Colleges);
            return c;
        }

        public static Task<Connection> FillComboFamiliesAsync(this Connection c)
        {
            return Task.Factory.StartNew(() =>
            {
                return FillComboFamilies(c);
            });
        }

        public static Connection FillComboFamilies(this Connection c)
        {
            c.Combo_Families.Clear();
            if (c.TableAdapterManager.Combo_FamiliesTableAdapter != null)
                c.TableAdapterManager.Combo_FamiliesTableAdapter.Dispose();
            c.TableAdapterManager.Combo_FamiliesTableAdapter = new Combo_FamiliesTableAdapter();
            c.TableAdapterManager.Combo_FamiliesTableAdapter.Connection = c.DBConnection;
            c.TableAdapterManager.Combo_FamiliesTableAdapter.Fill(c.Combo_Families);
            return c;
        }

        public static Task<Connection> FillComboFamilyListingsAsync(this Connection c)
        {
            return Task.Factory.StartNew(() =>
            {
                return FillComboFamilyListings(c);
            });
        }

        public static Connection FillComboFamilyListings(this Connection c)
        {
            c.Combo_Family_Listings.Clear();
            if (c.TableAdapterManager.Combo_Family_ListingsTableAdapter != null)
                c.TableAdapterManager.Combo_Family_ListingsTableAdapter.Dispose();
            c.TableAdapterManager.Combo_Family_ListingsTableAdapter = new Combo_Family_ListingsTableAdapter();
            c.TableAdapterManager.Combo_Family_ListingsTableAdapter.Connection = c.DBConnection;
            c.TableAdapterManager.Combo_Family_ListingsTableAdapter.Fill(c.Combo_Family_Listings);
            return c;
        }

        public static Task<Connection> FillDepartmentsAsync(this Connection c)
        {
            return Task.Factory.StartNew(() =>
            {
                return FillDepartments(c);
            });
        }

        public static Connection FillDepartments(this Connection c)
        {
            c.Departments.Clear();
            if (c.TableAdapterManager.DepartmentsTableAdapter != null)
                c.TableAdapterManager.DepartmentsTableAdapter.Dispose();
            c.TableAdapterManager.DepartmentsTableAdapter = new DepartmentsTableAdapter();
            c.TableAdapterManager.DepartmentsTableAdapter.Connection = c.DBConnection;
            c.TableAdapterManager.DepartmentsTableAdapter.Fill(c.Departments);
            return c;
        }

        public static Task<Connection> FillEndingFiscalYearAsync(this Connection c)
        {
            return Task.Factory.StartNew(() =>
            {
                return FillEndingFiscalYear(c);
            });
        }

        public static Connection FillEndingFiscalYear(this Connection c)
        {
            c.Ending_Fiscal_Year.Clear();
            if (c.TableAdapterManager.Ending_Fiscal_YearTableAdapter != null)
                c.TableAdapterManager.Ending_Fiscal_YearTableAdapter.Dispose();
            c.TableAdapterManager.Ending_Fiscal_YearTableAdapter = new Ending_Fiscal_YearTableAdapter();
            c.TableAdapterManager.Ending_Fiscal_YearTableAdapter.Connection = c.DBConnection;
            c.TableAdapterManager.Ending_Fiscal_YearTableAdapter.Fill(c.Ending_Fiscal_Year);
            return c;
        }

        public static Task<Connection> FillFamiliesAsync(this Connection c)
        {
            return Task.Factory.StartNew(() =>
            {
                return FillFamilies(c);
            });
        }

        public static Connection FillFamilies(this Connection c)
        {
            c.Families.Clear();
            if (c.TableAdapterManager.FamiliesTableAdapter != null)
                c.TableAdapterManager.FamiliesTableAdapter.Dispose();
            c.TableAdapterManager.FamiliesTableAdapter = new FamiliesTableAdapter();
            c.TableAdapterManager.FamiliesTableAdapter.Connection = c.DBConnection;
            c.TableAdapterManager.FamiliesTableAdapter.Fill(c.Families);
            return c;
        }

        public static Task<Connection> FillFamilyListingsAsync(this Connection c)
        {
            return Task.Factory.StartNew(() =>
            {
                return FillFamilyListings(c);
            });
        }

        public static Connection FillFamilyListings(this Connection c)
        {
            c.Family_Listings.Clear();
            if (c.TableAdapterManager.Family_ListingsTableAdapter != null)
                c.TableAdapterManager.Family_ListingsTableAdapter.Dispose();
            c.TableAdapterManager.Family_ListingsTableAdapter = new Family_ListingsTableAdapter();
            c.TableAdapterManager.Family_ListingsTableAdapter.Connection = c.DBConnection;
            c.TableAdapterManager.Family_ListingsTableAdapter.Fill(c.Family_Listings);
            return c;
        }

        public static Task<Connection> FillFileNumbersAsync(this Connection c)
        {
            return Task.Factory.StartNew(() =>
            {
                return FillFileNumbers(c);
            });
        }

        public static Connection FillFileNumbers(this Connection c)
        {
            c.File_Numbers.Clear();
            if (c.TableAdapterManager.File_NumbersTableAdapter != null)
                c.TableAdapterManager.File_NumbersTableAdapter.Dispose();
            c.TableAdapterManager.File_NumbersTableAdapter = new File_NumbersTableAdapter();
            c.TableAdapterManager.File_NumbersTableAdapter.Connection = c.DBConnection;
            c.TableAdapterManager.File_NumbersTableAdapter.Fill(c.File_Numbers);
            return c;
        }

        public static Task<Connection> FillGenderAsync(this Connection c)
        {
            return Task.Factory.StartNew(() =>
            {
                return FillGender(c);
            });
        }

        public static Connection FillGender(this Connection c)
        {
            c.Gender.Clear();
            if (c.TableAdapterManager.GenderTableAdapter != null)
                c.TableAdapterManager.GenderTableAdapter.Dispose();
            c.TableAdapterManager.GenderTableAdapter = new GenderTableAdapter();
            c.TableAdapterManager.GenderTableAdapter.Connection = c.DBConnection;
            c.TableAdapterManager.GenderTableAdapter.Fill(c.Gender);
            return c;
        }

        public static Task<Connection> FillInvestigatorsAsync(this Connection c)
        {
            return Task.Factory.StartNew(() =>
            {
                return FillInvestigators(c);
            });
        }

        public static Connection FillInvestigators(this Connection c)
        {
            c.Investigators.Clear();
            if (c.TableAdapterManager.InvestigatorsTableAdapter != null)
                c.TableAdapterManager.InvestigatorsTableAdapter.Dispose();
            c.TableAdapterManager.InvestigatorsTableAdapter = new InvestigatorsTableAdapter();
            c.TableAdapterManager.InvestigatorsTableAdapter.Connection = c.DBConnection;
            c.TableAdapterManager.InvestigatorsTableAdapter.Fill(c.Investigators);
            return c;
        }

        public static Task<Connection> FillOrganizationsAsync(this Connection c)
        {
            return Task.Factory.StartNew(() =>
            {
                return FillOrganizations(c);
            });
        }

        public static Connection FillOrganizations(this Connection c)
        {
            c.Organizations.Clear();
            if (c.TableAdapterManager.OrganizationsTableAdapter != null)
                c.TableAdapterManager.OrganizationsTableAdapter.Dispose();
            c.TableAdapterManager.OrganizationsTableAdapter = new OrganizationsTableAdapter();
            c.TableAdapterManager.OrganizationsTableAdapter.Connection = c.DBConnection;
            c.TableAdapterManager.OrganizationsTableAdapter.Fill(c.Organizations);
            return c;
        }

        public static Task<Connection> FillProjectNumbersAsync(this Connection c)
        {
            return Task.Factory.StartNew(() =>
            {
                return FillProjectNumbers(c);
            });
        }

        public static Connection FillProjectNumbers(this Connection c)
        {
            c.Project_Numbers.Clear();
            if (c.TableAdapterManager.Project_NumbersTableAdapter != null)
                c.TableAdapterManager.Project_NumbersTableAdapter.Dispose();
            c.TableAdapterManager.Project_NumbersTableAdapter = new Project_NumbersTableAdapter();
            c.TableAdapterManager.Project_NumbersTableAdapter.Connection = c.DBConnection;
            c.TableAdapterManager.Project_NumbersTableAdapter.Fill(c.Project_Numbers);
            return c;
        }

        public static Task<Connection> FillRecordsStatusAsync(this Connection c)
        {
            return Task.Factory.StartNew(() =>
            {
                return FillRecordsStatus(c);
            });
        }

        public static Connection FillRecordsStatus(this Connection c)
        {
            c.Records_Status.Clear();
            if (c.TableAdapterManager.Records_StatusTableAdapter != null)
                c.TableAdapterManager.Records_StatusTableAdapter.Dispose();
            c.TableAdapterManager.Records_StatusTableAdapter = new Records_StatusTableAdapter();
            c.TableAdapterManager.Records_StatusTableAdapter.Connection = c.DBConnection;
            c.TableAdapterManager.Records_StatusTableAdapter.Fill(c.Records_Status);
            return c;
        }

        public static Task<Connection> FillRemindersAsync(this Connection c)
        {
            return Task.Factory.StartNew(() =>
            {
                return FillReminders(c);
            });
        }

        public static Connection FillReminders(this Connection c)
        {
            c.Reminders.Clear();
            if (c.TableAdapterManager.RemindersTableAdapter != null)
                c.TableAdapterManager.RemindersTableAdapter.Dispose();
            c.TableAdapterManager.RemindersTableAdapter = new RemindersTableAdapter();
            c.TableAdapterManager.RemindersTableAdapter.Connection = c.DBConnection;
            c.TableAdapterManager.RemindersTableAdapter.Fill(c.Reminders);
            return c;
        }

        public static Task<Connection> FillStartingFiscalYearAsync(this Connection c)
        {
            return Task.Factory.StartNew(() =>
            {
                return FillStartingFiscalYear(c);
            });
        }

        public static Connection FillStartingFiscalYear(this Connection c)
        {
            c.Starting_Fiscal_Year.Clear();
            if (c.TableAdapterManager.Starting_Fiscal_YearTableAdapter != null)
                c.TableAdapterManager.Starting_Fiscal_YearTableAdapter.Dispose();
            c.TableAdapterManager.Starting_Fiscal_YearTableAdapter = new Starting_Fiscal_YearTableAdapter();
            c.TableAdapterManager.Starting_Fiscal_YearTableAdapter.Connection = c.DBConnection;
            c.TableAdapterManager.Starting_Fiscal_YearTableAdapter.Fill(c.Starting_Fiscal_Year);
            return c;
        }

        public static Task<Connection> FillStatusAsync(this Connection c)
        {
            return Task.Factory.StartNew(() =>
            {
                return FillStatus(c);
            });
        }

        public static Connection FillStatus(this Connection c)
        {
            c.Status.Clear();
            if (c.TableAdapterManager.StatusTableAdapter != null)
                c.TableAdapterManager.StatusTableAdapter.Dispose();
            c.TableAdapterManager.StatusTableAdapter = new StatusTableAdapter();
            c.TableAdapterManager.StatusTableAdapter.Connection = c.DBConnection;
            c.TableAdapterManager.StatusTableAdapter.Fill(c.Status);
            return c;
        }

        public static Task<Connection> FillTransactionsAsync(this Connection c)
        {
            return Task.Factory.StartNew(() =>
            {
                return FillTransactions(c);
            });
        }

        public static Connection FillTransactions(this Connection c)
        {
            c.Transactions.Clear();
            if (c.TableAdapterManager.TransactionsTableAdapter != null)
                c.TableAdapterManager.TransactionsTableAdapter.Dispose();
            c.TableAdapterManager.TransactionsTableAdapter = new TransactionsTableAdapter();
            c.TableAdapterManager.TransactionsTableAdapter.Connection = c.DBConnection;
            c.TableAdapterManager.TransactionsTableAdapter.Fill(c.Transactions);
            return c;
        }

        public static Task<Connection> FillLoginDataAsync(this Connection c)
        {
            return Task.Factory.StartNew(() =>
            {
                return FillLoginData(c);
            });
        }

        public static Connection FillLoginData(this Connection c)
        {
            c.Login_Data.Clear();
            if (c.TableAdapterManager.Login_DataTableAdapter != null)
                c.TableAdapterManager.Login_DataTableAdapter.Dispose();
            c.TableAdapterManager.Login_DataTableAdapter = new Login_DataTableAdapter();
            c.TableAdapterManager.Login_DataTableAdapter.Connection = c.DBConnection;
            c.TableAdapterManager.Login_DataTableAdapter.Fill(c.Login_Data);
            return c;
        }


    }
}
