import { useEffect, useState } from "react";
import { CompanyTenK } from "../../../API/company";
import { fetchTenK } from "../../../API/API-FMP";
import TenKFinderItem from "./TenKFinderItem";
import IndicateLoading from "../../Controls/Loaders/indicateLoading";
type Props = {
  ticker: string;
};

function TenKFinder({ ticker }: Props) {
  const [getMetrics, setMetrics] = useState<CompanyTenK[]>();
  const [isLoading, setLoading] = useState(false);

  useEffect(() => {
    const getTenKInit = async () => {
      if (!isLoading) {
        setLoading(true);
        console.info("Fetching S.E.C. Filings List...");
        await fetchTenK(ticker)
          .then((result) => {
            console.log(result);
            setMetrics(result?.data[0]);
          })
          .finally(() => setLoading(false));
      }
    };
    getTenKInit();
  }, [ticker]);

  return (
    <div className="inline-flex rounded shadow m-4">
      {getMetrics ? (
        getMetrics?.slice(0, 5).map((getMetrics) => {
          return <TenKFinderItem metrics={getMetrics} />;
        })
      ) : (
        <IndicateLoading type="ratios" />
      )}
    </div>
  );
}

export default TenKFinder;
