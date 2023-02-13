namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private List<BookLink> bookLinks = new List<BookLink>();
        public Form1()
        {
            InitializeComponent();
        }

        private async Task PopulateBookLinks()
        {
            using (var client = new HttpClient())
            {
                var html = await client.GetStringAsync("https://www.gutenberg.org/");

                int index = 0;
                while (index >= 0)
                {
                    int linkStart = html.IndexOf("<a href=", index);
                    if (linkStart < 0) break;

                    int linkEnd = html.IndexOf("</a>", linkStart);
                    int titleStart = html.IndexOf(">", linkStart);
                    int titleEnd = html.IndexOf("<", titleStart);

                    if (linkStart >= 0 && linkEnd >= 0 && titleStart >= 0 && titleEnd >= 0)
                    {
                        string link = html.Substring(linkStart + 9, linkEnd - linkStart - 9);
                        string title = html.Substring(titleStart + 1, titleEnd - titleStart - 1);
                        bookLinks.Add(new BookLink(title, "https://www.gutenberg.org" + link));
                    }

                    index = linkEnd;
                }
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string searchTerm = searchTextBox.Text.ToLower();
            var results = bookLinks.Where(b => b.Title.ToLower().Contains(searchTerm)).ToList();
            resultsListBox.DataSource = results;
            resultsListBox.DisplayMember = "Title";
        }


        class BookLink
        {
            public string Title { get; set; }
            public string Link { get; set; }

            public BookLink(string title, string link)
            {
                Title = title;
                Link = link;
            }
        }
    }
}