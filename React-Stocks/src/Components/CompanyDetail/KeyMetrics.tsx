interface Props {}
import {
  formatLargeMonetaryNumber,
  formatRatio,
} from "../../Helpers/formatNumbers";
import { useOutletContext } from "react-router-dom";
import { CompanyKeyMetrics } from "../../API/company";
import { useEffect, useState } from "react";
import { fetchCompanyKeyMetrics } from "../../API/API-FMP";
import RatioList from "../Controls/RatioList/RatioList";
import IndicateLoading from "../Controls/Loaders/indicateLoading";
import { FaMagnifyingGlassChart } from "react-icons/fa6";

const tableConfig = [
  {
    label: "Market Cap",
    render: (company: CompanyKeyMetrics) =>
      formatLargeMonetaryNumber(company.marketCapTTM),
    subTitle: "Total value of all a company's shares of stock",
  },
  {
    label: "Current Ratio",
    render: (company: CompanyKeyMetrics) =>
      formatRatio(company.currentRatioTTM),
    subTitle:
      "Measures the companies ability to pay short term debt obligations",
  },
  {
    label: "Return On Equity",
    render: (company: CompanyKeyMetrics) => formatRatio(company.roeTTM),
    subTitle:
      "Return on equity is the measure of a company's net income divided by its shareholder's equity",
  },
  {
    label: "Return On Assets",
    render: (company: CompanyKeyMetrics) =>
      formatRatio(company.returnOnTangibleAssetsTTM),
    subTitle:
      "Return on assets is the measure of how effective a company is using its assets",
  },
  {
    label: "Free Cashflow Per Share",
    render: (company: CompanyKeyMetrics) =>
      formatRatio(company.freeCashFlowPerShareTTM),
    subTitle:
      "Return on assets is the measure of how effective a company is using its assets",
  },
  {
    label: "Book Value Per Share TTM",
    render: (company: CompanyKeyMetrics) =>
      formatRatio(company.bookValuePerShareTTM),
    subTitle:
      "Book value per share indicates a firm's net asset value (total assets - total liabilities) on per share basis",
  },
  {
    label: "Dividend Yield TTM",
    render: (company: CompanyKeyMetrics) =>
      formatRatio(company.dividendYieldTTM),
    subTitle: "Shows how much a company pays each year relative to stock price",
  },
  {
    label: "Capex Per Share TTM",
    render: (company: CompanyKeyMetrics) =>
      formatRatio(company.capexPerShareTTM),
    subTitle:
      "Capex is used by a company to acquire, upgrade, and maintain physical assets",
  },
  {
    label: "Graham Number",
    render: (company: CompanyKeyMetrics) =>
      formatRatio(company.grahamNumberTTM),
    subTitle:
      "This is the upper bound of the price range that a defensive investor should pay for a stock",
  },
  {
    label: "PE Ratio",
    render: (company: CompanyKeyMetrics) => formatRatio(company.peRatioTTM),
    subTitle:
      "This is the upper bound of the price range that a defensive investor should pay for a stock",
  },
];
const KeyMetrics = ({}: Props) => {
  const ticker = useOutletContext<string>();
  const [getCompanyData, setCompanyData] = useState<CompanyKeyMetrics>();
  const [isLoading, setLoading] = useState(false);

  useEffect(() => {
    const getCompanyKeyMetricsInit = async () => {
      if (!isLoading) {
        setLoading(true);
        console.log("Fetching KPIs...");
        await fetchCompanyKeyMetrics(ticker)
          .then((result) => {
            if (getCompanyData !== result!.data[0])
              setCompanyData(result!.data[0]);
          })
          .finally(() => {
            setLoading(false);
          });
      }
    };
    getCompanyKeyMetricsInit();
  }, []);

  return (
    <>
      <FaMagnifyingGlassChart className="mr-4" />{" "}
      <h1>Key Metrics ({ticker})</h1>
      {getCompanyData ? (
        <>
          <RatioList data={getCompanyData} config={tableConfig} />
        </>
      ) : (
        <>
          <IndicateLoading type="ratios" />
        </>
      )}
    </>
  );
};

export default KeyMetrics;
