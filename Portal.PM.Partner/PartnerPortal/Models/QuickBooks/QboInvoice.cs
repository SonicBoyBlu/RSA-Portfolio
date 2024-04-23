using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models.QuickBooks
{
    public class QboInvoice
    {
        public QboInvoice()
        {
            SalesTermRefID = 5;
            //DueDate = DateTime.Now;
            LineItems = new List<QboInvoiceLineItem>();
        }
        public string InvoiveNum { get; set; }
        public string Type { get; set; }
        public string BreezewayStatus { get; set; }
        public string Email { get; set; }
        public int CustomerRefID { get; set; }
        public string CustomerName { get; set; }
        public string Line { get; set; }
        public DateTime TxnDate { get; set; }
        public string CustomerMemo_Value { get { return "Work Order #" + InvoiveNum; } }
        public int SalesTermRefID { get; set; }
        public DateTime DueDate
        {
            get
            {
                var dd = TxnDate.AddMonths(1);
                return new DateTime(dd.Year, dd.Month, 1);
            }
        }
        //public DateTime DueDate { get; set; }
        public List<QboInvoiceLineItem> LineItems { get; set; }
        public string BillTo { get; set; }
        public string UnitCode { get; set; }
        public string Description { get; set; }
        public string Resolution { get; set; }
        public double TotalAmount { get; set; }
        public bool IsIgnore { get; set; }
        public bool IsLocked { get; set; }
        public DateTime DateQboCommitted { get; set; }
        public string QboInvoiceNum { get; set; }
        public double QboAmount { get; set; }
        public DateTime QboTxnDate { get; set; }
        public double LineItemsTotal 
        {
            get
            {
                double amount = 0;
                if(LineItems != null)
                {
                    amount = LineItems.Sum(li => li.Amount*li.SalesItemLineDetail_Qty);
                }
                return amount;
            }
        }

        public double LineItemsTotalTaxIncluded
        {
            get
            {
                double amount = LineItemsTotal + LineItemsTotalTax;
                return amount;
            }
        }

        public string Link { get; set; }
        public string Id { get; set; }
        public string QboId { get; set; }

        //TBD: Read this from config settings.
        public static double TaxRate 
        { 
            get
            {
                return .0925;
            }
        }

        public string TaxRateDisplayText
        {
            get
            {
                string rate = (TaxRate * 100).ToString("#.##");
                return $"{rate}%";
            }
        }

        public double LineItemsTotalTax
        {
            get
            {
                double amount = 0;
                if (LineItems != null)
                {
                    amount = LineItems.Sum(li => li.TaxAmount);
                }
                return amount;
            }
        }

        public bool IsCommitted()
        {
            return DateQboCommitted != null && DateQboCommitted > DateTime.MinValue;
        }
    }


    public class QboInvoiceLineItem
    {
        public QboInvoiceLineItem()
        {
            DetailType = "SalesItemLineDetail";
        }
        public string Id { get { return LineNum.ToString(); } } 
        public double LineNum { get; set; }
        public string Description { get; set; } 
        public double Amount { get; set; }
        public double Total { get; set; }
        public string DetailType { get; set; } 
        public string SalesItemLineDetail_ItemRefId { get; set; } 
        public string SalesItemLineDetail_ItemRefName { get; set; } 
        public double SalesItemLineDetail_UnitPrice { get; set; }
        public double SalesItemLineDetail_Qty { get; set; }
        public string SalesItemLineDetail_TaxCodeRefId { get; set; } 
        public DateTime SalesItemLineDetail_ServiceDate { get; set; }
        public bool TaxableItem
        {
            get
            {
                return SalesItemLineDetail_TaxCodeRefId != "NON";
            }
        }
        public double TaxAmount 
        { 
            get
            {
                double amount = 0; 
                if(TaxableItem)
                {
                    amount = ((SalesItemLineDetail_Qty * SalesItemLineDetail_UnitPrice)) * QboInvoice.TaxRate;
                }
                return amount;
            }
        }
    }

}