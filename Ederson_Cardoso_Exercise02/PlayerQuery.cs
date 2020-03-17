using BaseballLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ederson_Cardoso_Exercise02
{
    public partial class PlayerQuery : Form
    {
        public PlayerQuery()
        {
            InitializeComponent();
        }

        BaseballEntities dbContext = new BaseballEntities();
        /// <summary>
        /// This method load players table to the playerDataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayPlayersTable_Load(object sender, EventArgs e)
        {
            // Load players table
            dbContext.Players.Load();

            // Specify DataSource for playerBindingSource
            playerBindingSource.DataSource = dbContext.Players.Local;
        }

        /// <summary>
        /// This method returns a searchButton_Click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchButton_Click(object sender, EventArgs e)
        {
            findPlayerByLastName(lastNameTextBox.Text.ToLower());
        }

        /// <summary>
        /// This method returns a clearButton_Click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearButton_Click(object sender, EventArgs e)
        {
            findPlayerByLastName("");
        }

        /// <summary>
        /// This method returns a list of players matching the last name informed
        /// </summary>
        /// <param name="lastName"></param>
        private void findPlayerByLastName(string lastName)
        {
            try
            {
                var queryPlayer =
                    from player in dbContext.Players
                    .Where(player => player.LastName.ToLower().Contains(lastName))
                    select player;

                playerDataGridView.DataSource = queryPlayer.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    } // end class
} // end namespace
