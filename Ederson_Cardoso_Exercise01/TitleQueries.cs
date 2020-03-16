using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private void TitleQueries_Load(object sender, EventArgs e)
        {
            // set the ComboBox to show the default query that
            queriesComboBox.SelectedIndex = 0;
        }

        // loads data into titleBindingSource based on user-selected query
        private void queriesComboBox_SelectedIndexChanged(
           object sender, EventArgs e)
        {
            var dbContext = new BooksLibrary.BooksEntities();

            // set the data displayed according to what is selected
            switch (queriesComboBox.SelectedIndex)
            {
                case 0: // Get a list of all the titles and the authors who wrote them. 
                        // Sort the results by title.
                    var booksAndAuthors =
                        from book in dbContext.Titles
                        from author in book.Authors
                        orderby book.Title1
                        select new { book.Title1, author.LastName, author.FirstName };
                        
                    titleDataGridView.Columns.Clear();
                    titleDataGridView.DataSource = booksAndAuthors.ToList();
                    break;

                case 1: // Get a list of all the titles and the authors who wrote them. 
                        // Sort the results by title. Each title sort the authors
                    var titlesByAuthorOrdered =
                        from book in dbContext.Titles
                        from author in book.Authors
                        orderby book.Title1, author.LastName, author.FirstName
                        select new { book.Title1, author.LastName, author.FirstName };

                    titleDataGridView.Columns.Clear();
                    titleDataGridView.DataSource = titlesByAuthorOrdered.ToList();
                    break;

                case 2: // Get a list of all the authors grouped by title, 
                        // sorted by title; 
                        // for a given title sort the author names alphabetically by last name then first name
                    var authorsByTitle = 
                        from book in dbContext.Titles
                        group book by book.Title1 into x
                        from author in dbContext.Authors
                        let Title = x.Max(t => t.Title1)
                        orderby Title, author.LastName, author.FirstName
                        select new { author.LastName, author.FirstName, Title };

                    titleDataGridView.Columns.Clear();
                    titleDataGridView.DataSource = authorsByTitle.ToList();
                    break;
            }

            titleBindingSource.MoveFirst(); // move to first entry
        }
    }
}
