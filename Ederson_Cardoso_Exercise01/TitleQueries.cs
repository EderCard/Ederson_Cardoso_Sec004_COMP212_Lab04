using BooksLibrary;
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

namespace Ederson_Cardoso_Exercise01
{
    public partial class TitleQueries : Form
    {
        public TitleQueries()
        {
            InitializeComponent();
        }
        
        BooksEntities dbContext = new BooksEntities();
        private void TitleQueries_Load(object sender, EventArgs e)
        {
            // Load Titles table
            dbContext.Titles.Load();

            // Specify DataSource for titleBindingSource
            titleBindingSource.DataSource = dbContext.Titles.Local;

            // set the ComboBox to show the default query that
            queriesComboBox.SelectedIndex = 0;
        }

        // loads data into titleBindingSource based on user-selected query
        private void queriesComboBox_SelectedIndexChanged(
           object sender, EventArgs e)
        {
            // set the data displayed according to what is selected
            switch (queriesComboBox.SelectedIndex)
            {
                case 0: // Get a list of all the titles and the authors who wrote them. 
                        // Sort the results by title.
                    var booksAndAuthors =
                        from book in dbContext.Titles
                        from author in book.Authors
                        orderby book.Title1
                        select new { book.Title1, author.FirstName, author.LastName };
                        
                    titleDataGridView.Columns.Clear();
                    titleDataGridView.DataSource = booksAndAuthors.ToList();
                    break;

                case 1: // Get a list of all the titles and the authors who wrote them. 
                        // Sort the results by title. Each title sort the authors alphabetically by last name, then first name.
                    var titlesByAuthorOrdered =
                        from book in dbContext.Titles
                        from author in book.Authors
                        orderby book.Title1, author.LastName, author.FirstName
                        select new { book.Title1, author.FirstName, author.LastName };

                    titleDataGridView.Columns.Clear();
                    titleDataGridView.DataSource = titlesByAuthorOrdered.ToList();
                    break;

                case 2: // Get a list of all the authors grouped by title, 
                        // sorted by title; 
                        // for a given title sort the author names alphabetically by last name then first name
                    var authorsByTitle =
                        from book in dbContext.Titles
                        orderby book.Title1
                        select new
                        {
                            Title = book.Title1,
                            Authors =
                                from author in book.Authors
                                orderby author.LastName, author.FirstName
                                select new { author.FirstName , author.LastName }
                        };

                    // Create a table to be titleDataGridView DataSource grouped by
                    DataTable table = new DataTable();
                    table.Columns.Add("TITLE", typeof(string));
                    table.Columns.Add("AUTHOR", typeof(string));

                    foreach (var book in authorsByTitle)
                    {
                        table.Rows.Add(book.Title, "");
                        
                        foreach (var author in book.Authors)
                        {
                            table.Rows.Add("", author.FirstName + " " + author.LastName);
                        }
                    }

                    titleDataGridView.Columns.Clear();
                    titleDataGridView.DataSource = table; // Binding data from table
                    break;
            }

            titleBindingSource.MoveFirst(); // Move to first entry
        }
    } // end class
} // end namespace
