import { useOutletContext } from "react-router-dom";
import { CompanyIncomeStatement } from "../../API/company";
import {
  formatLargeMonetaryNumber,
  formatRatio,
} from "../../Helpers/formatNumbers";
import { useEffect, useState } from "react";
import { fetchIncomeStatement } from "../../API/API-FMP";
import Table from "../Controls/Table/Table";
import IndicateLoading from "../Controls/Loaders/indicateLoading";
import { FaMoneyCheckDollar } from "react-icons/fa6";

interface Props {}

const tableConfig = [
  {
    label: "Date",
    render: (company: CompanyIncomeStatement) => company.date,
  },
  {
    label: "Revenue",
    render: (company: CompanyIncomeStatement) =>
      formatLargeMonetaryNumber(company.revenue),
  },
  {
    label: "Cost Of Revenue",
    render: (company: CompanyIncomeStatement) =>
      formatLargeMonetaryNumber(company.costOfRevenue),
  },
  {
    label: "Depreciation",
    render: (company: CompanyIncomeStatement) =>
      formatLargeMonetaryNumber(company.depreciationAndAmortization),
  },
  {
    label: "Operating Income",
    render: (company: CompanyIncomeStatement) =>
      formatLargeMonetaryNumber(company.operatingIncome),
  },
  {
    label: "Income Before Taxes",
    render: (company: CompanyIncomeStatement) =>
      formatLargeMonetaryNumber(company.incomeBeforeTax),
  },
  {
    label: "Net Income",
    render: (company: CompanyIncomeStatement) =>
      formatLargeMonetaryNumber(company.netIncome),
  },
  {
    label: "Net Income Ratio",
    render: (company: CompanyIncomeStatement) =>
      formatRatio(company.netIncomeRatio),
  },
  {
    label: "Earnings Per Share",
    render: (company: CompanyIncomeStatement) => formatRatio(company.eps),
  },
  {
    label: "Earnings Per Diluted",
    render: (company: CompanyIncomeStatement) =>
      formatRatio(company.epsdiluted),
  },
  {
    label: "Gross Profit Ratio",
    render: (company: CompanyIncomeStatement) =>
      formatRatio(company.grossProfitRatio),
  },
  {
    label: "Opearting Income Ratio",
    render: (company: CompanyIncomeStatement) =>
      formatRatio(company.operatingIncomeRatio),
  },
  {
    label: "Income Before Taxes Ratio",
    render: (company: CompanyIncomeStatement) =>
      formatRatio(company.incomeBeforeTaxRatio),
  },
];
const IncomeStatement = ({}: Props) => {
  const ticker = useOutletContext<string>();
  const [getIncomeStatementData, setIncomeStatementData] =
    useState<CompanyIncomeStatement[]>();
  const [isLoading, setLoading] = useState(false);
  useEffect(() => {
    const getIncomeStatementInit = async () => {
      if (!isLoading) {
        setLoading(true);
        await fetchIncomeStatement(ticker)
          .then((result) => {
            setIncomeStatementData(result!.data);
          })
          .finally(() => {
            setLoading(false);
          });
      }
    };
    getIncomeStatementInit();
  }, []);
  return (
    <>
      <FaMoneyCheckDollar className="mr-4" />{" "}
      <h1>Statement of Income ({ticker})</h1>
      {getIncomeStatementData ? (
        <>
          <Table config={tableConfig} data={getIncomeStatementData} />
        </>
      ) : (
        <>
          <IndicateLoading type="table" />
        </>
      )}
    </>
  );
};

export default IncomeStatement;
