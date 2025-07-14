import { FaMoneyBillTrendUp } from "react-icons/fa6";
import { CompanyCashFlow } from "../../API/company";
import { useState, useEffect } from "react";
import { useOutletContext } from "react-router";
import { formatLargeMonetaryNumber } from "../../Helpers/formatNumbers";
import IndicateLoading from "../Controls/Loaders/indicateLoading";
import Table from "../Controls/Table/Table";
import { fetchCashflowStatement } from "../../API/API-FMP";

interface Props {}

const tableConfig = [
  {
    label: "Date",
    render: (company: CompanyCashFlow) => company.date,
  },
  {
    label: "Operating Cashflow",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.operatingCashFlow),
  },
  {
    label: "Investing Cashflow",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.netCashUsedForInvestingActivites),
  },
  {
    label: "Financing Cashflow",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(
        company.netCashUsedProvidedByFinancingActivities
      ),
  },
  {
    label: "Cash At End of Period",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.cashAtEndOfPeriod),
  },
  {
    label: "CapEX",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.capitalExpenditure),
  },
  {
    label: "Issuance Of Stock",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.commonStockIssued),
  },
  {
    label: "Free Cash Flow",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.freeCashFlow),
  },
];

const CashFlow = ({}: Props) => {
  const ticker = useOutletContext<string>();
  const [getCashflowData, setCashflowData] = useState<CompanyCashFlow[]>();
  const [isLoading, setLoading] = useState(false);
  useEffect(() => {
    const getCashflowInit = async () => {
      if (!isLoading) {
        setLoading(true);
        await fetchCashflowStatement(ticker)
          .then((result) => {
            setCashflowData(result!.data);
          })
          .finally(() => setLoading(false));
      }
    };
    getCashflowInit();
  }, []);
  return (
    <>
      <FaMoneyBillTrendUp />
      <h1 className="ml-3">Cashflow Statement ({ticker})</h1>
      {getCashflowData ? (
        <>
          <Table config={tableConfig} data={getCashflowData} />
        </>
      ) : (
        <>
          <IndicateLoading type="table" />
        </>
      )}
    </>
  );
};

export default CashFlow;
