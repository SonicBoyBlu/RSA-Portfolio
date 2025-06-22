import { CompanyKeyMetrics } from "../API/company";
import RatioList from "../Components/Controls/RatioList/RatioList";
import Table from "../Components/Controls/Table/Table";
import { formatLargeNonMonetaryNumber } from "../Helpers/formatNumbers";
import { testDataIncomeStatement } from "../API/_testData";

interface Props {}

const tableConfig = [
  {
    label: "Market Cap",
    render: (company: CompanyKeyMetrics) =>
      formatLargeNonMonetaryNumber(company.marketCapTTM),
    subTitle: "Total value of all a company's shares of stock",
  },
];

function PageDesignGuide({}: Props) {
  return (
    <>
      <h1>Design Guide</h1>
      <p>
        This is the design guide for Fin Shark. These are reusable components of
        the app with brief instructions on how to use them.
      </p>
      <hr />

      <h1>Ratio Lists</h1>
      <RatioList data={testDataIncomeStatement} config={tableConfig} />

      <h1>Tables</h1>
      <Table data={testDataIncomeStatement} config={tableConfig} />
      <em>
        *Table - Table takes in a configuration object and company data as
        params. Use the config to style your table.
      </em>
    </>
  );
}

export default PageDesignGuide;
