using System;
using System.Windows.Forms;
using System.Collections;

namespace SimpleProjectManagement
{
    public class ListViewColumnSorter : IComparer
    {
        // stupac koji sortiramo
        public int SortColumn { get; set; }
        
        // specificira order sortiranja (asc, desc)
        public SortOrder Order { get; set; }

        private CaseInsensitiveComparer ObjectCompare;

        public ListViewColumnSorter()
        {
            SortColumn = 0;
            Order = SortOrder.Ascending;
            ObjectCompare = new CaseInsensitiveComparer();
        }

        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;

            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            // usporedimo dva elementa
            compareResult = ValidateInput(listviewX.SubItems[SortColumn].Text, listviewY.SubItems[SortColumn].Text);

            if (Order == SortOrder.Ascending)
            {
                return compareResult;
            }
            else if (Order == SortOrder.Descending)
            {
                return (-compareResult);
            }
            else
            {
                return 0;
            }
        }

        private int ValidateInput(string x, string y)
        {
            if (Validator.TextIsDate(x) && Validator.TextIsDate(y))
            {
                return ObjectCompare.Compare(Convert.ToDateTime(x), Convert.ToDateTime(y));
            }
            else if (Validator.TextIsNumber(x) && Validator.TextIsNumber(y))
            {
                return ObjectCompare.Compare(Convert.ToInt32(x), Convert.ToInt32(y));
            }
            else
            {
                return ObjectCompare.Compare(x, y);
            }
        }
    }
}