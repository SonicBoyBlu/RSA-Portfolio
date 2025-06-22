import { useOutletContext } from "react-router";
import { CompanyBalanceSheet } from "../../API/company";
import { formatLargeMonetaryNumber } from "../../Helpers/formatNumbers";
import { useEffect, useState } from "react";
import { fetchBalanceSheet } from "../../API/API-FMP";
import IndicateLoading from "../Controls/Loaders/indicateLoading";
import { FaScaleBalanced } from "react-icons/fa6";
//import Table from "../Controls/Table/Table";
import RatioList from "../Controls/RatioList/RatioList";

interface Props {}
const tableConfig = [
  {
    label: <div className="font-bold">Total Assets</div>,
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.totalAssets),
  },
  {
    label: "Current Assets",
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.totalCurrentAssets),
  },
  {
    label: "Total Cash",
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.cashAndCashEquivalents),
  },
  {
    label: "Property & equipment",
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.propertyPlantEquipmentNet),
  },
  {
    label: "Intangible Assets",
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.intangibleAssets),
  },
  {
    label: "Long Term Debt",
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.longTermDebt),
  },
  {
    label: "Total Debt",
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.otherCurrentLiabilities),
  },
  {
    label: <div className="font-bold">Total Liabilites</div>,
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.totalLiabilities),
  },
  {
    label: "Current Liabilities",
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.totalCurrentLiabilities),
  },
  {
    label: "Long-Term Debt",
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.longTermDebt),
  },
  {
    label: "Long-Term Income Taxes",
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.otherLiabilities),
  },
  {
    label: "Stakeholder's Equity",
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.totalStockholdersEquity),
  },
  {
    label: "Retained Earnings",
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.retainedEarnings),
  },
];
function BalanceSheet({}: Props): JSX.Element {
  const ticker = useOutletContext<string>();
  const [getBalanceSheetData, setBalanceSheetData] =
    useState<CompanyBalanceSheet>();
  const [isLoading, setLoading] = useState(false);

  useEffect(() => {
    const getBalanceSheetInit = async () => {
      if (!isLoading) {
        setLoading(true);
        console.info("Fetching Balance Sheet...");
        await fetchBalanceSheet(ticker)
          .then((result) => {
            setBalanceSheetData(result!.data[0]);
          })
          .finally(() => setLoading(false));
      }
    };
    getBalanceSheetInit();
  }, []);
  return (
    <>
      <FaScaleBalanced className="mr-4" /> <h1>Balance Sheet ({ticker})</h1>
      {getBalanceSheetData ? (
        <>
          <RatioList config={tableConfig} data={getBalanceSheetData} />
        </>
      ) : (
        <>
          <IndicateLoading type="ratios" />
        </>
      )}
    </>
  );
}

export default BalanceSheet;
