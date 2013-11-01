using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace SimpleProjectManagement
{
    public class DBConnection
    {
        private ConnectionStringSettings connString;

        public DBConnection()
        {
            // Dodaj reference na assembly System.Configuration.dll
            connString = ConfigurationManager.ConnectionStrings["SPMConnectionString"];
        }

        public void ExecSQL(string sql, List<SqlCeParameter> parameters)
        {
            try
            {
                using (SqlCeConnection conn = new SqlCeConnection(connString.ConnectionString))
                using (SqlCeCommand command = new SqlCeCommand(sql, conn))
                {
                    if (parameters != null)
                        command.Parameters.AddRange(parameters.ToArray());

                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable FillDataTable(string sql, List<SqlCeParameter> parameters)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlCeConnection conn = new SqlCeConnection(connString.ConnectionString))
                using (SqlCeCommand command = new SqlCeCommand(sql, conn))
                using (SqlCeDataAdapter adapter = new SqlCeDataAdapter())
                {
                    if (parameters != null)
                        command.Parameters.AddRange(parameters.ToArray());

                    adapter.SelectCommand = command;
                    conn.Open();
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        public void FillListViewRows(ListView listView, string sql, List<SqlCeParameter> parameters)
        {
            try
            {
                using (SqlCeConnection conn = new SqlCeConnection(connString.ConnectionString))
                {
                    conn.Open();

                    using (SqlCeCommand command = new SqlCeCommand(sql, conn))
                    {
                        if (parameters != null)
                            command.Parameters.AddRange(parameters.ToArray());

                        using (SqlCeDataReader reader = command.ExecuteReader())
                        {
                            listView.Items.Clear();

                            while (reader.Read())
                            {
                                ListViewItem lvItem = new ListViewItem(reader[0].ToString());

                                for (int i = 1; i < reader.FieldCount; i++)
                                {
                                    if (!reader.IsDBNull(i))
                                    {
                                        if (reader.GetFieldType(i).FullName == typeof(DateTime).FullName)
                                            lvItem.SubItems.Add(reader.GetDateTime(i).ToString("d"));
                                        else
                                            lvItem.SubItems.Add(reader.GetString(i));
                                    }
                                    else
                                    {
                                        lvItem.SubItems.Add(string.Empty);
                                    }
                                }

                                listView.Items.Add(lvItem);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void FillTabs(TabControl tab, string sql)
        {
            try
            {
                using (SqlCeConnection conn = new SqlCeConnection(connString.ConnectionString))
                {
                    conn.Open();

                    using (SqlCeCommand command = new SqlCeCommand(sql, conn))
                    using (SqlCeDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tab.Controls.Add(new TabPage(reader.GetString(0)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}